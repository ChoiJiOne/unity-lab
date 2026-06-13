using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityLab.EditorTools
{
    /// <summary>
    /// 메인 툴바의 플레이 버튼 왼쪽에 "씬 스위처" 드롭다운을 추가한다.
    /// 버튼을 누르면 프로젝트 내 모든 씬이 폴더별 폴드아웃(접기/펴기) 팝업으로 나열되고,
    /// 항목을 클릭하면 해당 씬을 연다(좌클릭=단독 열기, [+]=추가 로드).
    ///
    /// Unity 6.4의 공식 메인 툴바 확장 API(<see cref="MainToolbarElementAttribute"/>)를 사용한다.
    /// 드롭다운 본문은 폴드아웃 UI를 위해 GenericMenu 대신 커스텀 <see cref="SceneListPopup"/>을 띄운다.
    /// </summary>
    [InitializeOnLoad]
    public static class SceneSwitcherToolbar
    {
        // 툴바 요소 식별 경로(메뉴 경로 겸 Refresh 키 겸 오버레이 id)
        public const string ElementPath = "UnityLab/Scene Switcher";

        // 프로젝트당 "최초 1회 자동 표시 완료" 플래그 — 이후엔 사용자의 표시/숨김 선택을 존중한다.
        static string AutoShownKey => "UnityLab.SceneSwitcher.AutoShown.v1." + (Application.dataPath ?? "");
        static int _autoShowRetries;

        static SceneSwitcherToolbar()
        {
            // 활성 씬이 바뀌면 버튼 라벨을 갱신한다.
            EditorSceneManager.activeSceneChangedInEditMode += (_, __) => MainToolbar.Refresh(ElementPath);
            EditorSceneManager.sceneOpened += (_, __) => MainToolbar.Refresh(ElementPath);

            // 커스텀 메인 툴바 요소는 Unity 6.3+에서 기본 숨김이라, 최초 1회 강제로 표시한다.
            if (!EditorPrefs.GetBool(AutoShownKey, false))
                EditorApplication.delayCall += TryAutoShowOnce;
        }

        /// <summary>
        /// 메인 툴바 오버레이 캔버스에서 본 요소(<see cref="ElementPath"/>)의 표시를 강제로 켠다.
        /// 메인 툴바는 Overlay 시스템 기반이며, 캔버스 접근 경로가 internal이라 리플렉션으로 처리한다.
        /// 비공식 내부 API이므로 실패해도 무시한다(수동으로 켤 수 있음).
        /// </summary>
        static void TryAutoShowOnce()
        {
            try
            {
                var winType = typeof(EditorWindow).Assembly.GetType("UnityEditor.MainToolbarWindow");
                var win = winType?
                    .GetField("instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)?
                    .GetValue(null) as EditorWindow;
                if (win == null)
                {
                    // 메인 툴바 윈도우가 아직 생성되지 않음 → 다음 프레임 재시도(횟수 제한).
                    if (_autoShowRetries++ < 30)
                        EditorApplication.delayCall += TryAutoShowOnce;
                    return;
                }

                var canvas = typeof(EditorWindow)
                    .GetProperty("overlayCanvas", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                    .GetValue(win);
                if (canvas?.GetType()
                        .GetProperty("overlays", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                        .GetValue(canvas) is not IEnumerable overlays)
                    return;

                var overlayType = typeof(EditorWindow).Assembly.GetType("UnityEditor.Overlays.Overlay");
                var idProp = overlayType?.GetProperty("id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var displayedProp = overlayType?.GetProperty("displayed", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (idProp == null || displayedProp == null)
                    return;

                foreach (var overlay in overlays)
                {
                    if ((idProp.GetValue(overlay) as string) != ElementPath)
                        continue;

                    if (displayedProp.GetValue(overlay) is bool shown && !shown)
                        displayedProp.SetValue(overlay, true);

                    EditorPrefs.SetBool(AutoShownKey, true); // 1회 완료 표시
                    return;
                }
            }
            catch (Exception e)
            {
                // 내부 API 변경 등으로 실패 시 — 기능 자체엔 영향 없으니 안내만 남긴다.
                Debug.LogWarning($"[SceneSwitcher] 툴바 자동 표시에 실패했습니다. 툴바 빈 곳 우클릭 → '{ElementPath}'로 직접 켜주세요. ({e.Message})");
            }
        }

        // 플레이 버튼이 있는 가운데 영역의 가장 왼쪽(index 0)에 배치 → 플레이 버튼 바로 왼쪽
        [MainToolbarElement(ElementPath,
            defaultDockPosition = MainToolbarDockPosition.Middle,
            defaultDockIndex = 0)]
        static MainToolbarElement CreateSceneSwitcher()
        {
            var active = EditorSceneManager.GetActiveScene();
            string label = string.IsNullOrEmpty(active.name) ? "Scenes" : active.name;
            var icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
            var content = new MainToolbarContent(label, icon, "프로젝트 씬 열기");
            return new MainToolbarDropdown(content, ShowDropdown);
        }

        static void ShowDropdown(Rect dropdownRect)
        {
            UnityEditor.PopupWindow.Show(dropdownRect, new SceneListPopup(dropdownRect.width));
        }
    }

    /// <summary>
    /// 씬 목록 팝업. 등록 파일에 적힌 씬을 평평한 목록으로 표시한다.
    /// </summary>
    public class SceneListPopup : PopupWindowContent
    {
        const float RowHeight = 20f;

        readonly float _minWidth;
        readonly List<SceneInfo> _scenes = new();
        Vector2 _scroll;
        string _search = string.Empty;
        int _visibleCount;

        class SceneInfo
        {
            public string Name;
            public string Path;
            public bool Missing; // 등록돼 있으나 실제 .unity 파일이 없는 경우
        }

        public SceneListPopup(float minWidth)
        {
            _minWidth = Mathf.Max(240f, minWidth);
            BuildSceneList();
        }

        void BuildSceneList()
        {
            _scenes.Clear();

            // 프로젝트 전체가 아니라, 등록 파일(SceneSwitcherScenes.json)에 적힌 씬만 표시한다.
            foreach (var path in SceneRegistry.Load())
            {
                if (string.IsNullOrEmpty(path))
                    continue;

                _scenes.Add(new SceneInfo
                {
                    Name = Path.GetFileNameWithoutExtension(path),
                    Path = path,
                    Missing = string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)),
                });
            }

            _scenes.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
        }

        public override Vector2 GetWindowSize()
        {
            int rows = string.IsNullOrEmpty(_search)
                ? _scenes.Count
                : _scenes.Count(s => s.Name.IndexOf(_search, StringComparison.OrdinalIgnoreCase) >= 0);
            float height = 30f + rows * RowHeight + 8f; // 검색창 + 행들
            height = Mathf.Clamp(height, 90f, 520f);
            return new Vector2(Mathf.Max(_minWidth, 380f), height);
        }

        public override void OnGUI(Rect rect)
        {
            // 검색창
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                _search = EditorGUILayout.TextField(_search, EditorStyles.toolbarSearchField);
                if (GUILayout.Button("새로고침", EditorStyles.toolbarButton, GUILayout.Width(56)))
                    BuildSceneList();
            }

            string activePath = EditorSceneManager.GetActiveScene().path;
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            _visibleCount = 0;
            foreach (var scene in _scenes)
            {
                if (!string.IsNullOrEmpty(_search) &&
                    scene.Name.IndexOf(_search, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                _visibleCount++;
                DrawSceneRow(scene, scene.Path == activePath);
            }

            if (_visibleCount == 0)
                EditorGUILayout.LabelField($"등록된 씬이 없습니다. {SceneRegistry.ConfigPath} 에 추가하세요.",
                    EditorStyles.centeredGreyMiniLabel);

            EditorGUILayout.EndScrollView();
        }

        void DrawSceneRow(SceneInfo scene, bool isActive)
        {
            var style = new GUIStyle(EditorStyles.label)
            {
                fontStyle = isActive ? FontStyle.Bold : FontStyle.Normal,
                alignment = TextAnchor.MiddleLeft,
            };
            var icon = EditorGUIUtility.IconContent(scene.Missing ? "console.warnicon.sml" : "SceneAsset Icon");
            string suffix = scene.Missing ? "  (없음)" : (isActive ? "  ●" : "");
            var content = new GUIContent($" {scene.Name}{suffix}", icon.image, scene.Path);

            using (new EditorGUI.DisabledScope(scene.Missing))
            {
                if (GUILayout.Button(content, style, GUILayout.Height(RowHeight)))
                    OpenScene(scene.Path);
            }
        }

        void OpenScene(string path)
        {
            if (EditorApplication.isPlaying)
            {
                EditorUtility.DisplayDialog("씬 전환", "플레이 모드에서는 씬을 전환할 수 없습니다.", "확인");
                return;
            }

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return; // 사용자가 저장을 취소함

            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            editorWindow?.Close();
        }
    }

    /// <summary>
    /// 씬 스위처에 표시할 씬 목록과 제외(블랙리스트) 목록을 관리한다.
    /// 설정은 프로젝트 내 JSON 파일(<see cref="ConfigPath"/>)에 보존된다.
    ///   scenes  : 드롭다운에 표시할 씬 경로
    ///   exclude : 등록 후보에서 빼는 폴더(하위 전체) 또는 씬 파일 경로
    /// 파일이 없으면 기본 씬(DefaultScene, TappyPlane) + 기본 제외(Assets/Packages)로 자동 생성한다.
    /// </summary>
    public static class SceneRegistry
    {
        public const string ConfigPath = "Assets/Editor/SceneSwitcherScenes.json";

        // 파일이 없을 때의 기본값
        static readonly string[] DefaultSceneNames = { "DefaultScene", "TappyPlane" };
        static readonly string[] DefaultExcludes = { "Assets/Packages" };

        [Serializable]
        class Data
        {
            public List<string> scenes = new();
            public List<string> exclude = new();
        }

        public static List<string> Load() => LoadData().scenes;

        public static List<string> LoadExcludes() => LoadData().exclude;

        /// <summary>경로가 제외 목록에 걸리는지 검사한다(폴더 하위 전체 또는 정확 일치).</summary>
        public static bool IsExcluded(string scenePath, IEnumerable<string> excludes)
        {
            if (string.IsNullOrEmpty(scenePath))
                return false;

            foreach (var e in excludes)
            {
                if (string.IsNullOrEmpty(e))
                    continue;

                var norm = e.Replace('\\', '/').TrimEnd('/');
                if (string.Equals(scenePath, norm, StringComparison.OrdinalIgnoreCase))
                    return true; // 특정 파일/정확 일치
                if (scenePath.StartsWith(norm + "/", StringComparison.OrdinalIgnoreCase))
                    return true; // 폴더 하위 전체
            }
            return false;
        }

        static Data LoadData()
        {
            if (!File.Exists(ConfigPath))
            {
                var fresh = new Data
                {
                    scenes = ResolveDefaults(),
                    exclude = new List<string>(DefaultExcludes),
                };
                Write(fresh);
                return fresh;
            }

            try
            {
                var data = JsonUtility.FromJson<Data>(File.ReadAllText(ConfigPath)) ?? new Data();
                data.scenes ??= new List<string>();
                data.exclude ??= new List<string>();
                return data;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[SceneSwitcher] 설정 파일을 읽지 못했습니다({ConfigPath}): {e.Message}");
                return new Data();
            }
        }

        static List<string> ResolveDefaults()
        {
            var list = new List<string>();
            foreach (var name in DefaultSceneNames)
            {
                foreach (var guid in AssetDatabase.FindAssets($"t:Scene {name}"))
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (Path.GetFileNameWithoutExtension(path) == name && !list.Contains(path))
                    {
                        list.Add(path);
                        break;
                    }
                }
            }
            return list;
        }

        static void Write(Data data)
        {
            var dir = Path.GetDirectoryName(ConfigPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(ConfigPath, JsonUtility.ToJson(data, true));
            AssetDatabase.ImportAsset(ConfigPath);
        }
    }
}
