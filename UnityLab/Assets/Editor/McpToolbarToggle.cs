using System;
using System.Reflection;
using System.Threading.Tasks;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Services;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityLab.EditorTools
{
    /// <summary>
    /// 메인 툴바의 Play/Stop(게임 시작/중단) 버튼 영역에 Unity MCP를
    /// 통째로 켜고 끄는 버튼을 추가한다.
    ///
    /// Unity 6의 공식 메인 툴바 확장 API(<see cref="MainToolbarElementAttribute"/>)를 사용하며,
    /// <see cref="MainToolbarDockPosition.Middle"/> 도크(=Play/Pause/Step 컨트롤 영역) 옆에 배치된다.
    ///
    /// [상태 표시] 색상 점(dot)으로 "현재 상태"를 나타낸다.
    ///   초록 = 실행 중, 빨강 = 중단, 노랑 = 전환 중, 회색 = 조회 불가.
    ///   클릭 시: 초록(실행 중)→중단, 빨강(중단)→실행.
    ///
    ///   주의: <see cref="MainToolbarButton.content"/> 를 생성 후 재할당해도 뷰에 반영되지 않는다
    ///   (오버레이가 생성 시점에 한 번만 VisualElement를 빌드하고 content 변경 이벤트가 없음).
    ///   그래서 색/툴팁/활성화는 실제 Image·Button VisualElement를 찾아 직접 갱신한다.
    ///   흰색 점 텍스처에 상태색 tint를 곱해 색을 표현한다.
    ///
    /// [동작] HTTP 전송 모드의 MCP는 (1)로컬 HTTP 서버 프로세스(기본 127.0.0.1:8080)와
    ///   (2)거기에 붙는 Unity 브리지(WebSocket 클라이언트)로 나뉜다. 단순히 Bridge.StartAsync만
    ///   부르면 (2)만 연결하므로 서버가 꺼져 있으면 실패한다. 그래서 UI의 *Start Server* 및
    ///   <c>HttpAutoStartHandler</c> 와 동일하게 서버 기동 → reachable 대기 → 브리지 접속을 수행하고,
    ///   중단 시 Unity가 띄운 로컬 서버까지 정리한다.
    /// </summary>
    public static class McpToolbarToggle
    {
        const string ElementId = "UnityLab/MCP Bridge";

        static MainToolbarButton button;
        static bool busy;        // Start/Stop 전환 중 중복 클릭 방지
        static bool initialized;

        // 직접 갱신할 라이브 VisualElement 캐시
        static Image iconImage;          // 색상 점 아이콘
        static TextElement labelText;    // 버튼 라벨(상태 텍스트)
        static VisualElement buttonElement; // 클릭/툴팁/활성화 대상
        static Texture2D whiteDot;       // tint 곱셈용 흰색 점

        // 직전 적용값(불필요한 재적용·리페인트 방지)
        static Color appliedColor = new Color(-1f, -1f, -1f, -1f);
        static string appliedText;
        static string appliedTooltip;
        static bool appliedEnabled;

        static readonly Color RunningColor = new Color(0.30f, 0.82f, 0.40f); // 초록
        static readonly Color StoppedColor = new Color(0.90f, 0.35f, 0.30f); // 빨강
        static readonly Color BusyColor    = new Color(0.95f, 0.78f, 0.25f); // 노랑
        static readonly Color UnknownColor = new Color(0.60f, 0.60f, 0.60f); // 회색

        // Unity가 툴바를 구성할 때 호출하는 팩토리 메서드.
        [MainToolbarElement(ElementId,
            defaultDockPosition = MainToolbarDockPosition.Middle,
            defaultDockIndex = 100)]
        static MainToolbarElement CreateElement()
        {
            // 새 VisualElement가 빌드되므로 이전 캐시를 버리고 다시 찾도록 한다.
            iconImage = null;
            labelText = null;
            buttonElement = null;
            appliedColor = new Color(-1f, -1f, -1f, -1f);
            appliedText = null;
            appliedTooltip = null;

            button = new MainToolbarButton(new MainToolbarContent("MCP", WhiteDot(), null), OnClick);

            if (!initialized)
            {
                EditorApplication.update += PollState;
                initialized = true;
            }
            return button;
        }

        static void OnClick()
        {
            if (busy)
                return;
            Toggle();
        }

        static async void Toggle()
        {
            busy = true;
            ApplyVisual();
            try
            {
                if (TryGetRunning(out bool running) && running)
                    await StopMcpAsync();
                else
                    await StartMcpAsync();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[MCP Toolbar] 토글 실패: {ex}");
            }
            finally
            {
                busy = false;
                ApplyVisual();
            }
        }

        // 매 프레임 호출되지만, 요소 캐시가 살아있고 적용값이 같으면 사실상 비용이 없다.
        static void PollState() => ApplyVisual();

        /// <summary>현재 상태에 맞춰 라이브 Image의 색·툴팁·활성화를 직접 갱신한다.</summary>
        static void ApplyVisual()
        {
            AcquireElements();

            // 상태 → 색상/텍스트/툴팁 결정. 텍스트는 "MCP (현재상태 → 클릭시동작)" 형식.
            Color color;
            string text;
            string tooltip;
            if (busy)
            {
                color = BusyColor;
                text = "MCP (전환 중…)";
                tooltip = "Unity MCP 서버/브리지 전환 중…";
            }
            else if (!TryGetRunning(out bool running))
            {
                color = UnknownColor;
                text = "MCP (?)";
                tooltip = "Unity MCP 상태를 조회할 수 없습니다";
            }
            else if (running)
            {
                color = RunningColor;
                text = "MCP (켜짐 → 끄기)";
                int port = 0;
                try { port = MCPServiceLocator.Bridge.CurrentPort; } catch { }
                tooltip = port > 0
                    ? $"Unity MCP 켜짐 (포트 {port}) — 클릭하면 끄기"
                    : "Unity MCP 켜짐 — 클릭하면 끄기";
            }
            else
            {
                color = StoppedColor;
                text = "MCP (꺼짐 → 켜기)";
                tooltip = "Unity MCP 꺼짐 — 클릭하면 켜기 (필요 시 로컬 서버 자동 기동)";
            }

            if (iconImage != null && color != appliedColor)
            {
                if (iconImage.image != whiteDot)
                    iconImage.image = WhiteDot();
                iconImage.tintColor = color;
                iconImage.MarkDirtyRepaint();
                appliedColor = color;
            }

            if (labelText != null && text != appliedText)
            {
                labelText.text = text;
                appliedText = text;
            }

            if (buttonElement != null)
            {
                if (tooltip != appliedTooltip)
                {
                    buttonElement.tooltip = tooltip;
                    appliedTooltip = tooltip;
                }
                bool enabled = !busy;
                if (enabled != appliedEnabled)
                {
                    buttonElement.SetEnabled(enabled);
                    appliedEnabled = enabled;
                }
            }
        }

        /// <summary>메인 툴바 오버레이에서 우리 버튼의 Image·Button VisualElement를 찾아 캐시한다.</summary>
        static void AcquireElements()
        {
            if (iconImage != null && iconImage.panel != null)
                return; // 살아있는 캐시

            iconImage = null;
            buttonElement = null;
            try
            {
                var winType = typeof(MainToolbar).Assembly.GetType("UnityEditor.MainToolbarWindow");
                if (winType == null)
                    return;
                var wins = Resources.FindObjectsOfTypeAll(winType);
                if (wins.Length == 0)
                    return;

                var ocField = typeof(EditorWindow).GetField("m_OverlayCanvas",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                var oc = ocField?.GetValue(wins[0]);
                if (oc == null)
                    return;

                var overlays = oc.GetType()
                    .GetProperty("overlays", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(oc) as System.Collections.IEnumerable;
                if (overlays == null)
                    return;

                var overlayType = typeof(UnityEditor.Overlays.Overlay);
                var pId = overlayType.GetProperty("id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var pRoot = overlayType.GetProperty("rootVisualElement", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var o in overlays)
                {
                    if ((string)pId.GetValue(o) != ElementId)
                        continue;
                    if (!(pRoot.GetValue(o) is VisualElement root))
                        break;

                    iconImage = root.Q<Image>();
                    labelText = root.Q<TextElement>("EditorToolbarButtonText");
                    // 클릭/툴팁 대상: EditorToolbarButton(내부 타입)을 이름이 아닌 타입명으로 식별.
                    buttonElement = FindByTypeName(root, "EditorToolbarButton") ?? root;
                    break;
                }
            }
            catch
            {
                iconImage = null;
                buttonElement = null;
            }
        }

        static VisualElement FindByTypeName(VisualElement root, string typeName)
        {
            var stack = new System.Collections.Generic.Stack<VisualElement>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var e = stack.Pop();
                if (e.GetType().Name == typeName)
                    return e;
                foreach (var c in e.Children())
                    stack.Push(c);
            }
            return null;
        }

        static bool TryGetRunning(out bool running)
        {
            running = false;
            try
            {
                running = MCPServiceLocator.Bridge.IsRunning;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ───────────────────────────── MCP 기동/중단 ─────────────────────────────

        /// <summary>
        /// MCP 전체 기동. HTTP 로컬이면 서버 프로세스를 보장한 뒤 reachable을 기다렸다가 브리지를 연결한다.
        /// </summary>
        static async Task StartMcpAsync()
        {
            bool useHttp = true;
            try { useHttp = EditorConfigurationCache.Instance.UseHttpTransport; } catch { }
            bool remote = false;
            try { remote = HttpEndpointUtility.IsRemoteScope(); } catch { }

            if (!useHttp || remote)
            {
                await MCPServiceLocator.Bridge.StartAsync();
                return;
            }

            var server = MCPServiceLocator.Server;
            if (!server.IsLocalHttpServerReachable())
            {
                if (!server.CanStartLocalServer())
                {
                    Debug.LogError("[MCP Toolbar] 로컬 HTTP 서버를 시작할 수 없습니다(전송 설정 또는 URL 보안 정책 확인).");
                    return;
                }
                if (!server.StartLocalHttpServer(quiet: true))
                {
                    Debug.LogError("[MCP Toolbar] 로컬 HTTP 서버 시작에 실패했습니다.");
                    return;
                }
            }

            await WaitForServerAndConnectAsync(server);
        }

        /// <summary>서버가 응답할 때까지 대기하다가 브리지를 연결한다(HttpAutoStartHandler 로직 모사).</summary>
        static async Task WaitForServerAndConnectAsync(IServerManagementService server)
        {
            const int maxAttempts = 30;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                bool reachable = false;
                try { reachable = server.IsLocalHttpServerReachable(); } catch { }

                if (reachable)
                {
                    if (await MCPServiceLocator.Bridge.StartAsync())
                        return;
                }
                else if (attempt >= 20 && (attempt - 20) % 3 == 0)
                {
                    if (await MCPServiceLocator.Bridge.StartAsync())
                        return;
                }

                try { await Task.Delay(attempt < 6 ? 500 : 3000); }
                catch { return; }
            }
            Debug.LogWarning("[MCP Toolbar] 서버가 제때 응답하지 않아 브리지 연결에 실패했습니다.");
        }

        /// <summary>브리지를 끊고, Unity가 띄운 로컬 HTTP 서버가 있으면 함께 정리한다.</summary>
        static async Task StopMcpAsync()
        {
            await MCPServiceLocator.Bridge.StopAsync();
            try
            {
                bool useHttp = EditorConfigurationCache.Instance.UseHttpTransport;
                bool remote = HttpEndpointUtility.IsRemoteScope();
                if (useHttp && !remote)
                    MCPServiceLocator.Server.StopManagedLocalHttpServer();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[MCP Toolbar] 서버 중단 중 경고: {ex.Message}");
            }
        }

        /// <summary>tint 곱셈으로 색을 입히기 위한 흰색 원형 점 텍스처(안티에일리어싱).</summary>
        static Texture2D WhiteDot()
        {
            if (whiteDot != null)
                return whiteDot;

            const int S = 16;
            whiteDot = new Texture2D(S, S, TextureFormat.RGBA32, false)
            {
                hideFlags = HideFlags.HideAndDontSave,
                filterMode = FilterMode.Bilinear,
            };

            float center = (S - 1) / 2f;
            float radius = S / 2f - 1.5f;
            var pixels = new Color[S * S];
            for (int y = 0; y < S; y++)
            {
                for (int x = 0; x < S; x++)
                {
                    float dx = x - center, dy = y - center;
                    float dist = Mathf.Sqrt(dx * dx + dy * dy);
                    float alpha = Mathf.Clamp01(radius - dist + 0.5f);
                    pixels[y * S + x] = new Color(1f, 1f, 1f, alpha);
                }
            }
            whiteDot.SetPixels(pixels);
            whiteDot.Apply();
            return whiteDot;
        }
    }
}
