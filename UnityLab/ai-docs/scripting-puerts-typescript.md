# PuerTS(TypeScript) 스크립팅 세팅 가이드 (주제 37)

PuerTS를 Unity 6 프로젝트에 설치하고 C#↔TypeScript 상호 호출을 동작시키는 절차와
이 저장소에서 채택한 구성·재현 방법을 정리한 문서다. 리서치 주제 정의는
[unity-research-topics.md](../../Docs/unity-research-topics.md) 37번 참고.

- **대상 Unity:** 6000.4.9f1 (UPM 임베디드 패키지 방식)
- **PuerTS 버전:** v3.0.2 (Unity_v3.0.2 릴리스)
- **백엔드:** V8 (`PapiV8.dll`, 데스크톱 에디터 리서치 표준)
- **Node.js:** v24.16.0 (TypeScript 컴파일용, 18+ 필요)
- **git 방침:** **자체 포함(self-contained)** — 임베디드 패키지·릴리스 타르볼·스캐폴드를 모두
  추적한다. clone 즉시 동작(재현 단계 불필요). 재생성 가능한 `node_modules/`·`Assets/Gen/` 만 무시.

---

## 1. 설치 방식 결정 배경

PuerTS는 OpenUPM에 **core 일부만** 있고(`com.tencent.puerts.core` 등 2.x), **네이티브 백엔드
(v8/nodejs/quickjs)는 OpenUPM에 없다**. 백엔드 바이너리는 용량이 커서 GitHub Releases로만 배포된다
(v8 타르볼 74MB, 추출 시 전 플랫폼 플러그인 241MB).

| 방식 | 채택 여부 | 이유 |
|---|---|---|
| OpenUPM scoped registry | ✗ | 백엔드(v8)가 레지스트리에 없음 → core/backend 버전 불일치 위험 |
| `file:` 타르볼 참조 | ✗ | 릴리스 타르볼 루트가 `core/`·`v8/` 구조라 Unity `file:` 타르볼 규격(`package/`)과 불일치 |
| **`Packages/` 임베디드 패키지** | ✓ | 타르볼을 `Packages/<패키지명>/` 으로 추출하면 Unity가 자동 인식, manifest 수정 불필요 |

> 네이티브 바이너리(추출 후 ~241MB, OpenHarmony 제거 후 더 작음)를 **git에 그대로 추적**한다(자체 포함).
> LFS 미사용 — 기존 아트 에셋(png/fbx)을 직접 커밋해 온 저장소 관례와 동일. 따라서 clone 만 하면
> 바로 열린다. 아래 2장은 "처음 어떻게 설치했는지" 기록이며, 일반적으로 재실행할 필요는 없다.

---

## 2. 설치 절차 기록 (최초 1회, 이미 저장소에 포함됨)

### 2-1. 타르볼 다운로드 + 임베디드 패키지로 추출

프로젝트 루트(`UnityLab/`)에서:

```bash
# 1) 릴리스 타르볼 다운로드 (LocalPackages/ 에 보관 — 저장소에 함께 추적됨)
mkdir -p LocalPackages
curl -sL -o LocalPackages/PuerTS_Core_3.0.2.tgz \
  "https://github.com/Tencent/puerts/releases/download/Unity_v3.0.2/PuerTS_Core_3.0.2.tar.gz"
curl -sL -o LocalPackages/PuerTS_V8_3.0.2.tgz \
  "https://github.com/Tencent/puerts/releases/download/Unity_v3.0.2/PuerTS_V8_3.0.2.tar.gz"

# 2) Packages/ 하위에 임베디드 패키지로 추출 (--strip-components=1 로 core/·v8/ 접두 제거)
mkdir -p Packages/com.tencent.puerts.core Packages/com.tencent.puerts.v8
tar -xzf LocalPackages/PuerTS_Core_3.0.2.tgz -C Packages/com.tencent.puerts.core --strip-components=1
tar -xzf LocalPackages/PuerTS_V8_3.0.2.tgz   -C Packages/com.tencent.puerts.v8   --strip-components=1

# 3) OpenHarmony 플러그인 제거 (.so.meta GUID 가 base64 로 깨져 있어 Unity 가 경고를 띄움.
#    HarmonyOS 는 이 리서치 대상이 아니므로 폴더째 삭제 → 경고 제거 + 용량 절감)
rm -rf Packages/com.tencent.puerts.v8/Plugins/OpenHarmony \
       Packages/com.tencent.puerts.v8/Plugins/OpenHarmony.meta
```

> **알려진 경고:** 위 3)을 생략하면 Unity 가 OpenHarmony `libPapiV8.so.meta` 두 개에 대해
> "does not have a valid GUID ... will be ignored" 경고를 띄운다. 동작에는 무해하지만 폴더 삭제로 제거한다.

- 패키지 이름: `com.tencent.puerts.core` 3.0.2, `com.tencent.puerts.v8` 3.0.2 (core 3.0.2 의존)
- Windows 에디터 백엔드: `Packages/com.tencent.puerts.v8/Plugins/x86_64/PapiV8.dll`
- 백엔드 교체 시: V8 대신 `PuerTS_Quickjs_3.0.2.tar.gz`(9MB, 모바일 용량 최적) / `PuerTS_Nodejs_3.0.2.tar.gz`(169MB, 파일·네트워크 API) 추출. 단, **백엔드는 한 번에 하나만**.

### 2-2. Unity 에디터에서

1. Unity 6000.4.9f1 로 프로젝트 열기 → UPM이 임베디드 패키지를 자동 컴파일. 상단 메뉴에 **`Tools → PuerTS`** 생김.
2. (선택) **`Tools → PuerTS → Generate il2cpp → Reflection Mode`** — IL2CPP 빌드용 바인딩. 에디터(Mono) 리서치만 할 땐 생략 가능.
3. 씬에 빈 GameObject 생성 → `PuerTSBootstrap` 컴포넌트 추가 → Play.
4. 콘솔에 다음이 찍히면 성공:
   - `[PuerTS] main 모듈이 TypeScript 에서 로드됨` (TS→C#)
   - `[PuerTS] 안녕하세요, Unity 6! (C# 에서 호출됨)` (C#→TS)

---

## 3. 리서치 스캐폴드 (정본 코드)

라이브 파일은 `Assets/Packages/PuerTSResearch/` 에 있으며 **저장소에 함께 추적된다**(PuerTS 패키지도 추적되므로
clone 시 그대로 컴파일·실행됨). 아래는 참고용 본문이다.

### `Assets/Packages/PuerTSResearch/PuerTSBootstrap.cs`

```csharp
using System;
using Puerts;
using UnityEngine;

public class PuerTSBootstrap : MonoBehaviour
{
    private JsEnv _env;

    private void Start()
    {
        _env = new JsEnv();                                  // DefaultLoader → Resources, 백엔드 Auto(V8)
        ScriptObject main = _env.ExecuteModule("main.mjs");  // 모듈 로드 = TS→C#
        var greet = main.Get<Action<string>>("greet");       // export 가져오기
        greet?.Invoke("Unity 6");                            // C#→TS
    }

    private void Update()   => _env?.Tick();                 // V8 마이크로태스크/타이머 펌프
    private void OnDestroy() { _env?.Dispose(); _env = null; }
}
```

### `Assets/Packages/PuerTSResearch/Resources/main.mjs` (즉시 실행용 컴파일본)

```js
import { UnityEngine } from 'csharp';
UnityEngine.Debug.Log('[PuerTS] main 모듈이 TypeScript 에서 로드됨');
export function greet(name) {
    UnityEngine.Debug.Log(`[PuerTS] 안녕하세요, ${name}! (C# 에서 호출됨)`);
}
```

### `Assets/Packages/PuerTSResearch/TsProject/src/main.ts` (TypeScript 원본)

```ts
import { UnityEngine } from 'csharp'
UnityEngine.Debug.Log('[PuerTS] main 모듈이 TypeScript 에서 로드됨')
export function greet(name: string): void {
    UnityEngine.Debug.Log(`[PuerTS] 안녕하세요, ${name}! (C# 에서 호출됨)`)
}
```

---

## 4. 핵심 동작 원리 (확인된 사실)

- **로더:** `DefaultLoader` 는 `UnityEngine.Resources.Load` 로 스크립트를 찾는다 → 컴파일된 JS는
  반드시 `Resources/` 폴더 안에 있어야 한다.
- **확장자:** `.js` 는 비권장(경고 발생). `.mjs`(ESM) / `.cjs`(CommonJS) 를 쓴다. core 패키지가
  `MJSImporter`/`CJSImporter`(ScriptedImporter)를 동봉해 이 확장자를 TextAsset으로 임포트한다.
- **내장 모듈:** `'csharp'`(C# 타입 접근), `'puerts'`(PuerTS 런타임 API)는 PuerTS가 제공하는 내장 모듈.
- **타입 선언:** core 패키지 `Typing/puerts/index.d.ts` 는 **`"puerts"` 모듈만** 선언한다.
  `import { UnityEngine } from 'csharp'` 의 **`"csharp"` 모듈 타입은 동봉돼 있지 않고**,
  `Tools/PuerTS` 메뉴의 d.ts 생성으로 만들어야 한다(정확한 메뉴 항목은 에디터에서 확인 — v3에서
  명칭/위치가 v2와 다를 수 있음). **생성 전에도 런타임은 정상**이며(손으로 쓴 `main.mjs` 가 'csharp'를
  런타임 내장 모듈로 해석), 타입 체크(tsc)만 'csharp' 모듈을 못 찾는다.

### TypeScript 반복 워크플로 두 갈래

1. **tsc + 수동 .mjs**: `TsProject/tsconfig.json`(outDir `../Resources`)로 컴파일.
   단, **tsc는 `.js`로 출력**하므로 `main.js → main.mjs` 리네임 필요(혹은 빌드 스크립트로 자동화).
2. **ts-loader(권장, 선택)**: `com.tencent.puerts.ts-loader`(OpenUPM) 추가 시 `.ts`를 직접 로드 →
   리네임 불필요. 도입하려면 manifest에 OpenUPM scoped registry 추가:
   ```json
   "scopedRegistries": [
     { "name": "OpenUPM", "url": "https://package.openupm.com", "scopes": ["com.tencent.puerts"] }
   ]
   ```

---

## 5. 다음 리서치 단계 (주제 37 목표 매핑)

- [ ] **목표 1** — 설치 + 상호 호출 + `.d.ts` 생성: 위 스캐폴드로 양방향 호출 확인 완료. UnityEngine `.d.ts` 생성해 TS에서 타입 안전 호출 검증.
- [ ] **목표 2** — 간단한 게임 로직을 TS로 작성해 C# 호스트와 연결 (예: 데미지 계산/상태머신).
- [ ] **목표 3** — xLua(주제 36)와 비교: 타입 안정성 / 성능(호출 오버헤드) / 코드 용량.
- [ ] IL2CPP Reflection vs Static Wrapper 모드 빌드 비용·성능 차이 측정.

> 다음은 **주제 36 (xLua)** 세팅. main에 한 번에 하나씩 진행하는 정책이므로,
> PuerTS 실험·기록을 마치면 `Packages/com.tencent.puerts.*` 와 `Assets/Packages/PuerTSResearch/`,
> `LocalPackages/` 를 제거한 뒤 xLua를 올린다.

---

## 출처

- [Tencent/puerts GitHub](https://github.com/Tencent/puerts) · [Releases (Unity_v3.0.2)](https://github.com/Tencent/puerts/releases/tag/Unity_v3.0.2)
- [PuerTS 공식 설치 문서](https://puerts.github.io/en/docs/puerts/unity/install/)
- [com.tencent.puerts.core (OpenUPM)](https://openupm.com/packages/com.tencent.puerts.core/)
