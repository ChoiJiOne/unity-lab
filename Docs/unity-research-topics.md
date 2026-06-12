---
title: Unity 리서칭 주제 상세
type: note
status: draft
tags: [unity, research, detail]
---

# Unity 리서칭 주제 상세

Unity 클라이언트 개발 역량을 주제별로 정리한 리서칭 문서다. 각 주제는 **목적·목표**, **핵심 용어**, **선행 학습 자료(근거 출처)**, **리서치 범위**, **산출물**을 담는다.

번호는 작성 순서일 뿐 학습 순서가 아니다. 분류로 골라 보면 된다. 주제 한 줄 요약과 분류별 목차는 [unity-research-index.md](unity-research-index.md)를 참고한다. 주제 간 참조는 `→ NN. 제목` 형태로 이 문서 안의 다른 절을 가리킨다(`Ctrl+F`로 이동).

## 목차

- [성능·메모리](#성능메모리) — 01, 02, 05, 29, 30, 31
- [UI·입력·연출](#ui입력연출) — 03, 04, 12, 15, 16, 24
- [그래픽스·애니메이션·사운드](#그래픽스애니메이션사운드) — 10, 14, 17, 19, 21, 25
- [게임플레이·데이터](#게임플레이데이터) — 13, 18, 26, 27, 38
- [비동기·리소스 수명](#비동기리소스-수명) — 06, 07, 09
- [아키텍처·테스트·툴링](#아키텍처테스트툴링) — 00, 08, 11, 20, 28, 32
- [출시·운영·네트워크](#출시운영네트워크) — 22, 23, 33, 34, 35, 39, 40
- [스크립팅·핫업데이트](#스크립팅핫업데이트) — 36, 37
- [1인 개발·사업·운영](#1인-개발사업운영) — 41, 42, 43, 44, 45
- [추가 주제(모바일 플랫폼·AI·파이프라인 보강)](#추가-주제46-61) — 46~61

---

## 성능·메모리

### 01. C# 런타임 비용과 GC Allocation

**목적·목표**

- **목적**: AI가 만든 "깔끔해 보이는" C# 코드 안의 할당·박싱·LINQ 비용을 찾아내고 개선할 수 있게 된다.
- **목표**
  1. `Update()` 안의 `Where().ToList()`, `new List<T>()`, 문자열 결합, 박싱 샘플을 만들어 GC 할당을 Profiler로 측정한다.
  2. dirty flag·캐싱·이벤트 기반 갱신으로 같은 시나리오의 GC 할당을 줄인다.
  3. 개선 전후를 수치로 비교해 코드 규칙으로 정리한다.

**핵심 용어**: GC 할당(GC Alloc, 힙에 새로 만들어 회수해야 하는 메모리 — 프레임마다 쌓이면 끊김), 박싱(Boxing, 값 형식을 object로 변환하며 힙에 복사 — 의도치 않은 할당 원인).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| C# value types | https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types |
| Boxing and Unboxing | https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing |
| LINQ deferred execution | https://learn.microsoft.com/en-us/dotnet/standard/linq/deferred-execution-lazy-evaluation |
| Unity Garbage Collector overview | https://docs.unity3d.com/6000.1/Documentation/Manual/performance-garbage-collector.html |

**리서치 범위**

- **다룰 내용**: GC 할당 측정, 박싱·LINQ·문자열 비용, dirty flag·캐싱 개선.
- **다루지 않을 내용**: Object Pooling 실습(→ 02), 에셋 메모리(→ 05).

**산출물**: `AllocationSamples.cs`, `RuntimeCostOptimized.cs`, `Docs/csharp-runtime-cost.md`, 개선 전후 Profiler 캡처.

### 02. Profiler 기본과 Object Pooling

**목적·목표**

- **목적**: Profiler에서 어디를 봐야 하는지 익히고, 표준 `ObjectPool` API를 작은 샘플로 확인한다.
- **목표**
  1. Profiler의 CPU Usage·Timeline·GC Alloc 위치를 찾아 읽는다.
  2. 데미지 텍스트 30~50개를 생성·반환하는 Object Pool 샘플을 만든다.
  3. 풀 초기화·반환·중복 반환 정책을 정리한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Profiler Manual | https://docs.unity3d.com/Manual/Profiler.html |
| Collect performance data on a target platform | https://docs.unity3d.com/6000.3/Documentation/Manual/profiling-target-device.html |
| UnityEngine.Pool.ObjectPool API | https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Pool.ObjectPool_1.html |

**리서치 범위**

- **다룰 내용**: Profiler 읽는 법, `ObjectPool` 사용, 풀 생명주기 정책.
- **다루지 않을 내용**: GC 비용 심화(→ 01), UI 셀 풀링(→ 03).

**산출물**: `DamageTextPool_Basic.cs`, `PoolUsageScene`, `Docs/objectpool-basic-note.md`, Profiler 캡처 1~2장.

### 05. Memory Profiler와 에셋 수명

**목적·목표**

- **목적**: GameObject 파괴와 에셋 메모리 해제가 같은 의미가 아님을 직접 확인하고, 참조와 해제를 다룰 수 있게 된다.
- **목표**
  1. 큰 텍스처·스프라이트를 로드해 UI에 표시하고 Memory Profiler로 메모리를 본다.
  2. UI 닫기·씬 전환·참조 제거 전후 스냅샷을 비교한다.
  3. `Resources.UnloadUnusedAssets`·GC·참조 차이가 해제에 미치는 영향을 정리한다.

**핵심 용어**: 스냅샷(특정 시점 메모리 상태 캡처 — 비교로 누수 추적), 에셋 수명(텍스처 등이 메모리에 올라가고 내려가는 시점 — 오브젝트 파괴와 별개).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Memory Profiler package | https://docs.unity3d.com/Packages/com.unity.memoryprofiler%40latest/ |
| Unity Memory Profiler 1.0 manual | https://docs.unity3d.com/Packages/com.unity.memoryprofiler%401.0/ |
| Unity memory profiling guide | https://unity.com/how-to/use-memory-profiling-unity |

**리서치 범위**

- **다룰 내용**: 스냅샷 비교, 텍스처·스프라이트 메모리, 참조 해제, UnloadUnusedAssets.
- **다루지 않을 내용**: Addressables 핸들 수명(→ 07), GC 할당 비용(→ 01).

**산출물**: `AssetMemoryScene`, `LargeTextureLoader.cs`, 메모리 스냅샷, `Docs/memory-profiler-asset-lab.md`.

### 29. 렌더링 배칭과 드로우콜 최적화

**목적·목표**

- **목적**: SRP Batcher·GPU 인스턴싱·정적/동적 배칭으로 드로우콜을 줄이고, 모바일 GPU 비용을 다룰 수 있게 된다.
- **목표**
  1. Frame Debugger로 배칭이 깨지는 지점(머티리얼·셰이더 변형)을 찾는다.
  2. SRP Batcher·GPU 인스턴싱을 적용해 드로우콜 변화를 측정한다.
  3. 정적 배칭·아틀라스·머티리얼 통합으로 배칭률을 올린다.

**핵심 용어**: 드로우콜(CPU가 GPU에 그리기를 지시하는 호출 — 많으면 CPU 병목), SRP Batcher(같은 셰이더 변형의 드로우 준비 비용 절감 — URP 기본), GPU 인스턴싱(같은 메시·머티리얼을 한 번에 다량 그리기).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| SRP Batcher in URP | https://docs.unity3d.com/Manual/SRPBatcher.html |
| GPU instancing | https://docs.unity3d.com/Manual/GPUInstancing.html |
| Draw call batching | https://docs.unity3d.com/Manual/DrawCallBatching.html |
| Configure for better performance in URP | https://docs.unity3d.com/6000.3/Documentation/Manual/urp/configure-for-better-performance.html |

**리서치 범위**

- **다룰 내용**: 드로우콜 측정, SRP Batcher·GPU 인스턴싱·정적 배칭, 아틀라스·머티리얼 통합, 모바일 URP 설정.
- **다루지 않을 내용**: 셰이더 작성 자체(→ 14, → 19), UI 배칭(→ 03).

**산출물**: 배칭 적용 전후 드로우콜 비교(Frame Debugger 캡처), 배칭률 개선 씬·설정 노트, 모바일 렌더링 최적화 체크리스트.

### 30. C# Job System·Burst·DOTS 기초

**목적·목표**

- **목적**: C# Job System과 Burst로 작업을 멀티스레드·SIMD로 가속하고, DOTS/ECS의 데이터 중심 접근이 언제 유리한지 판단할 수 있게 된다.
- **목표**
  1. 무거운 반복 연산을 IJob/IJobParallelFor로 옮기고 메인 스레드 부담 감소를 측정한다.
  2. `[BurstCompile]`을 적용해 Burst 전후 성능을 비교한다.
  3. ECS의 엔티티·컴포넌트·시스템 개념을 작은 예제로 확인하고 도입 기준을 정리한다.

**핵심 용어**: Job System(작업을 여러 스레드로 나눠 실행 — 안전한 병렬 처리), Burst(C# 작업을 SIMD 네이티브 코드로 컴파일), ECS(데이터(컴포넌트)와 로직(시스템) 분리 — 대량 객체에 유리).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| C# Job System | https://docs.unity3d.com/Manual/JobSystem.html |
| Burst User Guide | https://docs.unity3d.com/Packages/com.unity.burst@latest |
| Entity Component System (ECS) | https://unity.com/ecs |
| DOTS in Unity 6 (features & samples) | https://unity.com/resources/dots-concepts-features-samples-resources-unity-6 |

**리서치 범위**

- **다룰 내용**: IJob/IJobParallelFor, NativeArray, Burst 적용·비교, ECS 기본 개념·도입 기준.
- **다루지 않을 내용**: 전체 게임의 ECS 전환(대규모), 물리 충돌 기본(→ 26).

**산출물**: 반복 연산의 Job/Burst 버전과 메인 스레드 부담·시간 비교, ECS 최소 예제와 "언제 DOTS를 쓰나" 판단 노트.

### 31. IL2CPP·빌드·앱 용량 최적화

**목적·목표**

- **목적**: 모바일 빌드의 IL2CPP·코드 스트리핑·텍스처 압축으로 앱 용량과 런타임 성능을 다룰 수 있게 된다.
- **목표**
  1. Mono와 IL2CPP 빌드의 차이(성능·용량·플랫폼 요건)를 정리한다.
  2. 코드 스트리핑·`link.xml`로 제거되는 코드와 리플렉션 예외를 다룬다.
  3. 텍스처 압축(ASTC 등)·빌드 리포트로 앱 용량 구성 요소를 분석·축소한다.

**핵심 용어**: IL2CPP(C#을 C++로 AOT 변환 — iOS 필수·Android 권장), 코드 스트리핑(쓰지 않는 코드 제거 — 리플렉션 주의), ASTC(모바일용 텍스처 압축 포맷).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| IL2CPP overview | https://docs.unity3d.com/Manual/IL2CPP.html |
| Managed code stripping | https://docs.unity3d.com/Manual/ManagedCodeStripping.html |
| Reducing the file size of your build | https://docs.unity3d.com/Manual/ReducingFilesize.html |
| Texture compression formats for platforms | https://docs.unity3d.com/Manual/texture-compression-formats.html |

**리서치 범위**

- **다룰 내용**: Mono vs IL2CPP, 코드 스트리핑·link.xml, 텍스처 압축, 빌드 리포트 기반 용량 분석.
- **다루지 않을 내용**: CI 자동화 흐름(→ 20), 스토어 심사·배포 절차.

**산출물**: Mono/IL2CPP 빌드 비교 표(용량·체감 성능), 앱 용량 구성 분석과 축소 전후 비교, 스트리핑 예외(link.xml) 처리 노트.

---

## UI·입력·연출

### 03. 대량 UI 리스트 성능

**목적·목표**

- **목적**: 인벤토리·상점·우편함·랭킹처럼 셀이 많은 UI 리스트의 성능 문제를 재현하고 해결할 수 있게 된다.
- **목표**
  1. 셀 1,000개를 한 번에 생성하는 나쁜 리스트를 만들어 생성 비용과 끊김을 측정한다.
  2. 보이는 셀만 갱신하는 셀 풀링 구조로 바꿔 측정값을 개선한다.
  3. Canvas 분리·Layout Group·Raycaster 비용을 비교해 규칙을 정리한다.

**핵심 용어**: 셀 풀링(Cell Pooling, 보이는 만큼의 셀만 만들어 재사용 — 가상화라고도 함).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity UI optimization tips | https://unity.com/how-to/unity-ui-optimization-tips |
| Unity Scroll Rect Manual | https://docs.unity3d.com/Manual/script-ScrollRect.html |
| Unity Frame Debugger | https://docs.unity3d.com/6000.4/Documentation/Manual/FrameDebugger.html |

**리서치 범위**

- **다룰 내용**: 대량 셀 생성 비용, 셀 풀링, Canvas 분리, Layout/Raycaster 비용, 드로우 콜.
- **다루지 않을 내용**: 일반 Object Pooling 기초(→ 02), 셰이더·overdraw 심화(→ 21).

**산출물**: `InventoryList_Bad.cs`, `InventoryList_Pooled.cs`, `InventoryCell.cs`, `Docs/ui-large-list-performance.md`, Profiler·Frame Debugger 캡처.

### 04. Input System 기본

**목적·목표**

- **목적**: 모바일·PC 입력을 액션 단위로 추상화하고, UI 입력과 게임플레이 입력의 충돌을 다룰 수 있게 된다.
- **목표**
  1. 이동·상호작용·취소·스킬 입력을 Input Action으로 정의하고, 키보드·마우스·터치에서 같은 액션으로 동작시킨다.
  2. UI 팝업이 열린 상태에서 게임플레이 입력을 차단한다.
  3. 입력 액션 네이밍과 맵 분리 정책을 정리한다.

**핵심 용어**: Input Action(입력 장치와 독립적으로 정의하는 행동 단위 — 예: Move, Interact), Action Map(상황별 입력 묶음 — 맵 전환으로 충돌 제어).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Input System Manual | https://docs.unity3d.com/6000.4/Documentation/Manual/com.unity.inputsystem.html |
| Input System package documentation | https://docs.unity3d.com/Packages/com.unity.inputsystem%40latest/ |
| Unity Input System GitHub | https://github.com/Unity-Technologies/InputSystem |

**리서치 범위**

- **다룰 내용**: Input Action 정의, 다중 장치 매핑, Action Map 전환, UI 입력 차단, 네이밍 정책.
- **다루지 않을 내용**: 입력 기반 연출(→ 10), 네트워크 입력 동기화.

**산출물**: `InputActions.inputactions`, `PlayerInputController.cs`, `InputBlocker.cs`, `Docs/input-system-policy.md`.

### 12. DOTween과 UI Animation

**목적·목표**

- **목적**: 트윈 기반 UI 연출을 생명주기와 취소 관점으로 다룰 수 있게 된다.
- **목표**
  1. Sequence·SetTarget·Kill·OnComplete·SetLink 개념을 확인한다.
  2. 팝업 open/close 연출을 만들고, 빠른 연타·닫는 중 재오픈을 재현한다.
  3. 오브젝트 파괴 후 OnComplete 접근 문제를 막는 트윈 수명 정책을 정리한다.

**핵심 용어**: 트윈(Tween, 값을 시간에 따라 보간해 움직이는 애니메이션), SetLink(트윈 수명을 대상 오브젝트에 묶기 — 파괴 시 자동 정리).

> DOTween은 Unity 공식 패키지가 아니다. 도입 시 라이선스·버전 고정·초기화 정책을 별도로 확인한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| DOTween Documentation | https://dotween.demigiant.com/documentation.php |
| DOTween Get Started | https://dotween.demigiant.com/getstarted.php |
| DOTween GitHub | https://github.com/demigiant/dotween |

**리서치 범위**

- **다룰 내용**: 트윈 수명 관리, 팝업 연출, 파괴 후 콜백 안전 처리.
- **다루지 않을 내용**: Timeline 컷신(→ 15), 캐릭터 애니메이션(→ 10).

**산출물**: `PopupTweenController_Bad.cs`, `PopupTweenController_Safe.cs`, `Docs/dotween-ui-animation-policy.md`.

### 15. Timeline과 Playables

**목적·목표**

- **목적**: 스킬 연출·컷신·튜토리얼 시퀀스를 Timeline으로 만들고 코드 이벤트와 연결할 수 있게 된다.
- **목표**
  1. Timeline Asset/Instance·PlayableDirector·Signal·마커 개념을 확인한다.
  2. 카메라·애니메이션·이펙트·사운드·데미지 Signal을 포함한 스킬 컷신을 만든다.
  3. Timeline 중간 오브젝트 파괴·씬 전환을 재현하고 연출/게임 로직 책임을 분리한다.

**핵심 용어**: Timeline Signal(타임라인 특정 시점에 게임 코드를 호출하는 마커), PlayableDirector(타임라인 재생 제어 컴포넌트).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Timeline Manual | https://docs.unity3d.com/6000.4/Documentation/Manual/com.unity.timeline.html |
| Timeline package documentation | https://docs.unity3d.com/Packages/com.unity.timeline%401.8/ |
| Timeline Signals workflow | https://docs.unity3d.com/Packages/com.unity.timeline%401.8/manual/wf-signals.html |
| Playables API Manual | https://docs.unity3d.com/Manual/Playables.html |

**리서치 범위**

- **다룰 내용**: Timeline 컷신, Signal로 게임 이벤트 연결, 연출/게임 로직 책임 분리.
- **다루지 않을 내용**: 트윈 연출(→ 12), 카메라 구성(→ 16).

**산출물**: `SkillCutscene.timeline`, `TimelineSignalReceiver.cs`, `PlayableDirectorController.cs`, `Docs/timeline-playable-policy.md`.

### 16. Cinemachine과 Camera

**목적·목표**

- **목적**: 카메라 follow·shake·blend·컷신 카메라를 코드와 Timeline에 연결할 수 있게 된다.
- **목표**
  1. Cinemachine Brain·Virtual Camera·blend 개념을 확인한다.
  2. 플레이어 follow 카메라와 공격 시 카메라 shake를 만든다.
  3. Timeline 컷신 중 Virtual Camera 전환을 구현한다.

**핵심 용어**: Virtual Camera(실제 카메라가 따라가는 가상 시점 — 여러 개 두고 전환), Blend(카메라 전환을 부드럽게 섞는 처리).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Cinemachine Manual | https://docs.unity3d.com/Manual/com.unity.cinemachine.html |
| Cinemachine package documentation | https://docs.unity3d.com/Packages/com.unity.cinemachine%402.3/ |
| Cinemachine GitHub | https://github.com/Unity-Technologies/com.unity.cinemachine |

**리서치 범위**

- **다룰 내용**: follow·shake·blend, Timeline 카메라 전환, 카메라 연출 기준.
- **다루지 않을 내용**: 컷신 시퀀스 구성(→ 15).

**산출물**: `CameraController.cs`, `CameraShakeService.cs`, `TimelineCameraSequence`, `Docs/cinemachine-camera-note.md`.

### 24. UI Toolkit 런타임 UI

**목적·목표**

- **목적**: uGUI 외에 Unity가 미래로 미는 UI Toolkit으로 런타임 UI를 만들고, uGUI와 언제 무엇을 쓸지 판단할 수 있게 된다.
- **목표**
  1. UXML(구조)·USS(스타일)·C#(로직)로 데이터 중심 화면(인벤토리·랭킹 등) 1종을 만든다.
  2. 같은 화면을 uGUI로도 만들어 드로우콜·갱신 비용·작성 편의를 비교한다.
  3. UI Toolkit와 uGUI의 선택 기준(애니메이션 HUD vs 데이터 화면)을 표로 정리한다.

**핵심 용어**: UXML(UI 구조를 정의하는 XML — HTML과 유사), USS(UI 스타일 시트 — CSS와 유사), VisualElement(UI Toolkit 기본 노드 — 트리 구성).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| UI Toolkit Manual | https://docs.unity3d.com/Manual/UIElements.html |
| Comparison of UI systems in Unity | https://docs.unity3d.com/Manual/UI-system-compare.html |
| Migrate from uGUI to UI Toolkit | https://docs.unity3d.com/Manual/UIE-Transitioning-From-UGUI.html |
| Get started with runtime UI | https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html |

**리서치 범위**

- **다룰 내용**: UXML·USS 구성, 데이터 바인딩, 런타임 UI 1종, uGUI와 비용·작성 비교, 선택 기준.
- **다루지 않을 내용**: 에디터 확장용 UI Toolkit(→ 28), uGUI 대량 리스트 최적화(→ 03).

**산출물**: UXML/USS와 컨트롤러로 만든 런타임 UI 화면 1종, 동일 화면의 uGUI 버전과 비교 표, 선택 기준 노트.

---

## 그래픽스·애니메이션·사운드

### 10. Animator와 State Machine

**목적·목표**

- **목적**: 캐릭터 상태 전이와 애니메이션 이벤트를 코드·테스트·디버깅 관점으로 다룰 수 있게 된다.
- **목표**
  1. Animator Controller·파라미터·레이어·블렌드 트리·Animation Event 개념을 확인한다.
  2. Idle/Move/Attack/Hit/Dead 상태와 공격 타이밍 이벤트를 만든다.
  3. 코드 FSM과 Animator Controller의 책임을 분리하고 `Animator.StringToHash`를 적용한다.

**핵심 용어**: FSM(유한 상태 머신 — 상태와 전이 규칙으로 동작 표현), Animation Event(애니메이션 특정 프레임에서 함수 호출 — 공격 판정 타이밍 등).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Animator Controller Manual | https://docs.unity3d.com/6000.4/Documentation/Manual/class-AnimatorController.html |
| Animator component Manual | https://docs.unity3d.com/6000.4/Documentation/Manual/class-Animator.html |
| Animator Scripting API | https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Animator.html |

**리서치 범위**

- **다룰 내용**: Animator 상태 전이, Animation Event, 코드 FSM 분리, 상태 키 캐싱.
- **다루지 않을 내용**: 컷신·시퀀스 연출(→ 15), Spine 애니메이션(→ 17).

**산출물**: `CharacterStateMachine.cs`, `AnimatorBridge.cs`, `AttackTimingEvent.cs`, `Docs/animator-state-policy.md`.

### 14. Shader Graph와 URP 기초

**목적·목표**

- **목적**: 노드 기반 셰이더로 dissolve·hit flash·outline 같은 실무형 효과를 만들 수 있게 된다.
- **목표**
  1. URP·머티리얼·셰이더·프로퍼티·키워드 개념을 확인한다.
  2. Shader Graph로 Hit Flash·Dissolve·Simple Outline 중 1~2개를 만든다.
  3. 머티리얼 프로퍼티를 코드에서 제어하고 Frame Debugger로 확인한다.

**핵심 용어**: URP(범용 렌더 파이프라인), Shader Graph(노드 기반 셰이더 제작 도구 — 코드 없이 효과 제작).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Shader Graph Manual | https://docs.unity3d.com/Manual/com.unity.shadergraph.html |
| Shader Graph package documentation | https://docs.unity3d.com/Packages/com.unity.shadergraph%4017.0/manual/index.html |
| URP introduction | https://docs.unity3d.com/6000.4/Documentation/Manual/urp/urp-introduction.html |
| Unity Shader/VFX URP eBook | https://unity.com/resources/create-shaders-visual-effects-urp-unity-6 |

**리서치 범위**

- **다룰 내용**: Shader Graph 효과, 머티리얼 프로퍼티 코드 제어, Frame Debugger 확인.
- **다루지 않을 내용**: 코드 기반 셰이더(→ 19), 파티클·VFX(→ 21).

**산출물**: `HitFlash.shadergraph`, `Dissolve.shadergraph`, `MaterialPropertyController.cs`, `Docs/shader-graph-urp-note.md`.

### 17. Spine Runtime

**목적·목표**

- **목적**: Spine 애니메이션을 Unity에서 재생·전환·스킨 교체하고, 런타임 비용과 수명 문제를 확인할 수 있게 된다.
- **목표**
  1. SkeletonAnimation·SkeletonGraphic·애니메이션 믹싱·스킨·슬롯·어태치먼트 개념을 확인한다.
  2. Spine 캐릭터를 임포트해 Idle/Attack/Hit 전환을 만든다.
  3. UI용 SkeletonGraphic과 월드용 SkeletonAnimation의 비용·용도를 비교한다.

**핵심 용어**: Spine(2D 스켈레탈 애니메이션 도구·런타임 — Unity 공식 패키지 아님), 스킨(Skin, 슬롯에 붙는 이미지 묶음 교체 단위).

> Spine 에디터·에셋은 라이선스 이슈가 있을 수 있다. 공개 Lab에는 공식 샘플이나 직접 만든 최소 에셋만 쓴다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| spine-unity Runtime Documentation | https://en.esotericsoftware.com/spine-unity |
| Spine Documentation | https://esotericsoftware.com/spine-documentation |
| Spine Runtimes | https://esotericsoftware.com/spine-runtimes |
| spine-unity Getting Started video | https://www.youtube.com/watch?v=DxDZtTK2nlE |

**리서치 범위**

- **다룰 내용**: Spine 애니메이션 전환·스킨 교체, UI/월드 렌더링 비용 비교.
- **다루지 않을 내용**: Spine 에셋의 Addressables 수명 심화(→ 07), Animator 기반 애니메이션(→ 10).

**산출물**: `SpineCharacterController.cs`, `SpineSkinChanger.cs`, `Docs/spine-runtime-note.md`.

### 19. ShaderLab / HLSL 기초

**목적·목표**

- **목적**: Shader Graph만 쓰지 않고, 최소한의 HLSL/ShaderLab 구조를 읽고 수정할 수 있게 된다.
- **목표**
  1. Shader/SubShader/Pass/Properties와 vertex/fragment 흐름을 확인한다.
  2. Unlit color shader와 texture sample shader를 작성한다.
  3. dissolve threshold 또는 UV scroll 효과를 작성하고 Shader Graph 구현과 비교한다.

**핵심 용어**: ShaderLab(Unity 셰이더를 감싸는 선언 언어 — Pass·Properties 정의), HLSL(셰이더 작성 언어), 셰이더 베리언트(키워드 조합마다 만들어지는 변형 — 과다 시 빌드·메모리 부담).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Introduction to writing shaders in code | https://docs.unity3d.com/6000.4/Documentation/Manual/SL-ShadingLanguage.html |
| ShaderLab language reference | https://docs.unity3d.com/6000.4/Documentation/Manual/SL-Reference.html |
| Writing HLSL shader programs | https://docs.unity3d.com/6000.4/Documentation/Manual/writing-shader-writing-shader-programs-hlsl.html |
| HLSL shader examples | https://docs.unity3d.com/6000.4/Documentation/Manual/built-in-shader-examples.html |

**리서치 범위**

- **다룰 내용**: ShaderLab 구조, HLSL vertex/fragment, dissolve·UV scroll, 노드 구현과 비교, 베리언트 위험.
- **다루지 않을 내용**: 노드 기반 효과 제작(→ 14).

**산출물**: `UnlitColor.shader`, `TextureScroll.shader`, `DissolveHLSL.shader`, `Docs/shaderlab-hlsl-basic-note.md`.

### 21. Particle System / VFX와 이펙트 풀링

**목적·목표**

- **목적**: 전투 이펙트의 생성·파괴·재생 완료·풀링·sorting·overdraw 문제를 다룰 수 있게 된다.
- **목표**
  1. ParticleSystem의 play/stop/clear/stop action과 VFX Graph 도입 기준을 확인한다.
  2. 히트 이펙트 100개를 생성·파괴하는 샘플을 만들어 비용을 측정한다.
  3. EffectPool을 적용하고 sorting order·투명 overdraw를 Frame Debugger로 관찰·개선한다.

**핵심 용어**: Overdraw(같은 픽셀을 여러 번 그리는 낭비 — 투명 이펙트 겹침 시 증가), Stop Action(파티클 정지 시 동작 — 풀 반환과 연결).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Particle Systems Manual | https://docs.unity3d.com/Manual/ParticleSystems.html |
| Visual Effect Graph package documentation | https://docs.unity3d.com/Packages/com.unity.visualeffectgraph%40latest/ |
| Unity Frame Debugger | https://docs.unity3d.com/6000.4/Documentation/Manual/FrameDebugger.html |

**리서치 범위**

- **다룰 내용**: 파티클 생성·파괴·재생 완료, 이펙트 풀링, sorting, overdraw 관찰.
- **다루지 않을 내용**: 셰이더 효과 제작(→ 14, → 19).

**산출물**: `HitEffectSpawner_Bad.cs`, `EffectPool.cs`, `EffectAutoReturn.cs`, `Docs/particle-vfx-pooling-note.md`.

### 25. 오디오 시스템과 AudioMixer

**목적·목표**

- **목적**: BGM·효과음·음성을 AudioMixer로 분리·제어하고, 모바일에서 오디오 메모리·로딩·끊김 문제를 다룰 수 있게 된다.
- **목표**
  1. AudioMixer 그룹(BGM/SFX/Voice)과 볼륨·스냅샷을 구성한다.
  2. 효과음 동시 재생 시 보이스 한도·우선순위·풀링을 적용한다.
  3. 오디오 클립의 로드 타입(압축·스트리밍)에 따른 메모리·지연 차이를 측정한다.

**핵심 용어**: AudioMixer(여러 오디오 소스를 묶어 효과·볼륨을 거는 믹싱 구조 — 그룹(버스) 구성), 스냅샷(믹서 설정의 한 상태 — 상황별 전환), 로드 타입(클립을 메모리에 올리는 방식 — 메모리·지연 절충).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Audio overview | https://docs.unity3d.com/Manual/Audio.html |
| Audio Mixer | https://docs.unity3d.com/Manual/AudioMixer.html |
| AudioClip import settings | https://docs.unity3d.com/Manual/class-AudioClip.html |
| Audio optimization (best practices) | https://unity.com/how-to/optimize-game-audio |

**리서치 범위**

- **다룰 내용**: AudioMixer 그룹·스냅샷, 보이스 한도·풀링, 클립 로드 타입별 메모리·지연 측정.
- **다루지 않을 내용**: 오디오 에셋의 Addressables 수명(→ 07), DSP 수준의 커스텀 사운드 합성.

**산출물**: BGM/SFX/Voice 믹서와 스냅샷 전환 샘플, 효과음 풀링·보이스 한도 적용 코드, 클립 로드 타입별 메모리·지연 비교 표.

---

## 게임플레이·데이터

### 13. 인벤토리 설계 변경과 ScriptableObject

**목적·목표**

- **목적**: 패턴 암기가 아니라 요구사항 변경으로 구조가 어디서 무너지는지 경험하고, 설정/상태 분리 설계를 익힌다.
- **목표**
  1. `ItemData` 하나로 처리하는 v1에 스택·장비·기간제 요구사항을 추가해 한계를 경험한다.
  2. `ItemDefinition`(설정)과 `ItemInstance`(상태)를 분리한 v2로 정렬·필터·저장 요구사항을 반영한다.
  3. 변경 전후 수정 파일 수를 기록해 설계 차이의 효과를 비교한다.

**핵심 용어**: ScriptableObject(인스턴스 없이 에셋으로 데이터를 담는 객체 — 설정 데이터 보관에 적합), 정의/인스턴스 분리(공유 설정과 개별 상태를 나누는 설계 — 요구사항 변경에 강함).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| ScriptableObject Manual | https://docs.unity3d.com/Manual/class-ScriptableObject.html |
| ScriptableObject Scripting API | https://docs.unity3d.com/6000.1/Documentation/ScriptReference/ScriptableObject.html |
| Unity ScriptableObject architecture eBook | https://unity.com/resources/create-modular-game-architecture-with-scriptable-objects-ebook |
| 『게임 프로그래밍 패턴』 | https://gameprogrammingpatterns.com/ |

**리서치 범위**

- **다룰 내용**: SO 설정/상태 분리, 요구사항 변경 대응, 정렬·필터·저장, 변경 영향 측정.
- **다루지 않을 내용**: 세이브 파일 포맷·직렬화 심화(→ 27), 현지화(→ 18).

**산출물**: `InventoryV1`, `InventoryV2`, `ItemDefinition.cs`, `ItemInstance.cs`, `Docs/requirement-change-log.md`.

### 18. Localization 기본

**목적·목표**

- **목적**: 텍스트·폰트·숫자/날짜·동적 문자열을 포함한 기본 현지화 흐름을 만들 수 있게 된다.
- **목표**
  1. String Table·Asset Table·Locale·Smart String 개념을 확인한다.
  2. 로비 UI 텍스트·아이템 이름/설명·동적 값 문장을 현지화한다.
  3. 키 네이밍·누락 키 대응·폰트 폴백 정책을 정리한다.

**핵심 용어**: Locale(언어·지역 단위 예: ko-KR), Smart String(변수·복수형을 끼워 넣는 현지화 문자열), 폰트 폴백(글리프가 없을 때 대체 폰트 사용 — 깨짐 방지).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Localization package manual | https://docs.unity3d.com/6000.2/Documentation/Manual/com.unity.localization.html |
| Localization package documentation | https://docs.unity3d.com/Packages/com.unity.localization%401.0/ |

**리서치 범위**

- **다룰 내용**: String/Asset Table, Smart String, 키 네이밍, 누락 키 대응, 폰트 폴백.
- **다루지 않을 내용**: 데이터 구조 설계(→ 13), 음성·더빙 현지화.

**산출물**: Localization 테이블, `LocalizedInventoryView.cs`, `Docs/localization-policy.md`.

### 26. 물리(Physics)와 충돌

**목적·목표**

- **목적**: Rigidbody·Collider·레이어 충돌 매트릭스·레이캐스트를 이해하고, 물리 비용과 흔한 함정을 다룰 수 있게 된다.
- **목표**
  1. Collider·Trigger·Rigidbody 조합별 충돌/트리거 콜백 발생 조건을 정리한다.
  2. 레이어 충돌 매트릭스로 불필요한 충돌 검사를 줄인다.
  3. 레이캐스트·OverlapSphere의 할당 없는 버전(NonAlloc 등)을 적용하고 비용을 측정한다.

**핵심 용어**: Rigidbody(물리 시뮬레이션을 받는 본체), Trigger(물리 반응 없이 겹침만 감지), 충돌 매트릭스(레이어 간 충돌 검사 여부 표 — 검사량 절감).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Physics overview | https://docs.unity3d.com/Manual/PhysicsOverview.html |
| Collider interactions / collision action matrix | https://docs.unity3d.com/Manual/CollidersOverview.html |
| Layer-based collision detection | https://docs.unity3d.com/Manual/LayerBasedCollision.html |
| Physics.RaycastNonAlloc API | https://docs.unity3d.com/ScriptReference/Physics.RaycastNonAlloc.html |

**리서치 범위**

- **다룰 내용**: 충돌/트리거 콜백 조건, 충돌 매트릭스, NonAlloc 쿼리, FixedUpdate·고정 타임스텝, 물리 비용 측정.
- **다루지 않을 내용**: 물리 기반 캐릭터 컨트롤러 심화, DOTS Physics(→ 30).

**산출물**: 충돌/트리거 콜백 조건 정리 표와 검증 씬, 충돌 매트릭스 적용 전후 비용 비교, NonAlloc 레이캐스트 적용 코드.

### 27. 세이브 시스템과 직렬화

**목적·목표**

- **목적**: 게임 데이터를 안전하게 저장·복원하는 직렬화 방식을 비교하고, 버전 변경·손상·치트에 견디는 세이브 구조를 만들 수 있게 된다.
- **목표**
  1. JSON·바이너리·PlayerPrefs의 용도와 한계를 비교한다.
  2. 세이브 데이터 스키마에 버전 필드를 두고, 구버전 세이브를 신버전으로 마이그레이션한다.
  3. 저장 중 종료·파일 손상에 대비한 원자적 쓰기(임시 파일 후 교체)를 적용한다.

**핵심 용어**: 직렬화(객체를 저장 가능한 형태로 바꾸기 — 복원은 역직렬화), PlayerPrefs(키-값 설정 저장소 — 대량·민감 데이터엔 부적합), 원자적 쓰기(임시 파일에 쓰고 마지막에 교체 — 중간 종료 대비).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity JsonUtility | https://docs.unity3d.com/ScriptReference/JsonUtility.html |
| Script serialization | https://docs.unity3d.com/Manual/script-Serialization.html |
| PlayerPrefs | https://docs.unity3d.com/ScriptReference/PlayerPrefs.html |
| Application.persistentDataPath | https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html |

**리서치 범위**

- **다룰 내용**: JSON/바이너리/PlayerPrefs 비교, 스키마 버전·마이그레이션, 원자적 쓰기, 무결성 검증.
- **다루지 않을 내용**: 데이터 구조 설계(→ 13), 서버 동기화·클라우드 세이브 인프라.

**산출물**: 직렬화 방식 비교 표와 샘플 세이브/로드 코드, 버전 마이그레이션·원자적 쓰기 구현, 세이브 손상·구버전 대응 정책 노트.

### 38. NavMesh와 AI 내비게이션

**목적·목표**

- **목적**: NavMesh로 캐릭터의 길 찾기·장애물 회피를 구현하고, 런타임 베이크·동적 장애물·성능 비용을 다룰 수 있게 된다.
- **목표**
  1. AI Navigation 패키지로 NavMesh를 베이크하고 NavMeshAgent를 목적지로 이동시킨다(`SetDestination`).
  2. NavMeshObstacle·Off-Mesh Link·여러 에이전트 타입으로 회피·점프·다중 크기 이동을 구현한다.
  3. NavMeshSurface로 런타임 베이크를 수행하고, 경로 계산 비용과 에이전트 수에 따른 부하를 측정한다.

**핵심 용어**: NavMesh(걸을 수 있는 영역을 나타낸 메시 — 베이크로 생성), NavMeshAgent(NavMesh 위 이동·회피 행위자), Off-Mesh Link(점프·낙하 같은 끊긴 영역 연결), 런타임 베이크(실행 중 NavMesh 생성·갱신).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| AI Navigation (패키지 매뉴얼) | https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/ |
| NavMesh Agent component reference | https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshAgent.html |
| NavMesh Surface (런타임 베이크) | https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshSurface.html |
| NavMesh Scripting API | https://docs.unity3d.com/ScriptReference/AI.NavMesh.html |
| Unity Learn - Unity NavMesh | https://learn.unity.com/tutorial/unity-navmesh |

**리서치 범위**

- **다룰 내용**: NavMesh 베이크, NavMeshAgent 이동·회피, NavMeshObstacle·Off-Mesh Link, 멀티 에이전트, 런타임 베이크, 경로 비용 측정.
- **다루지 않을 내용**: 행동 트리·의사결정(→ 10과 별개의 AI 로직), 물리 기반 이동(→ 26), DOTS 기반 대규모 군중 시뮬레이션.

**산출물**: NavMesh 베이크 + 에이전트 이동·회피 데모 씬, 런타임 베이크와 Off-Mesh Link 적용 예제, 에이전트 수별 경로 계산 비용 측정 노트.

---

## 비동기·리소스 수명

### 06. Async/Await, Cancellation, UniTask

**목적·목표**

- **목적**: 비동기 프로그래밍의 기본 개념과 취소 정책을 작은 샘플로 이해한다.
- **목표**
  1. Task·async/await·CancellationToken·Unity Awaitable·UniTask의 차이를 확인한다.
  2. 취소 가능한 작업과 불가능한 작업을 만들어 차이를 비교한다.
  3. `try/catch/finally`·dispose·await 이후 유효성 확인 규칙을 정리한다.

**핵심 용어**: CancellationToken(진행 중인 비동기 작업에 취소를 알리는 신호 — 누락 시 파괴된 객체 접근 위험), UniTask(할당 없는 비동기 제공 라이브러리 — Unity 공식 패키지 아님).

> UniTask는 Unity 공식 패키지가 아니다. 도입 시 라이선스·버전 고정을 별도로 확인한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| C# asynchronous programming | https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/ |
| Cancellation in managed threads | https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads |
| Unity Awaitable / async-await support | https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html |
| UniTask documentation | https://cysharp.github.io/UniTask/ |

**리서치 범위**

- **다룰 내용**: async/await, CancellationToken, UniTask, 예외·dispose 규칙.
- **다루지 않을 내용**: Addressables·씬 전환 적용(→ 07, → 09).

**산출물**: `AsyncBasics.cs`, `CancellationSample.cs`, `Docs/async-cancellation-basic-note.md`.

### 07. Addressables Load/Release 수명 관리

**목적·목표**

- **목적**: Addressables의 load/release 균형과 핸들 소유권을 설명하고 관리할 수 있게 된다.
- **목표**
  1. 참조 카운트·`AsyncOperationHandle`·load/release 흐름을 확인한다.
  2. 아이콘·프리팹을 로드하며 release 누락·중복 로드를 재현한다.
  3. 핸들 추적기로 참조 균형을 맞추고 Memory Profiler로 관찰한다.

**핵심 용어**: Addressables(주소 기반으로 에셋을 비동기 로드/해제하는 패키지 — load/release 균형이 핵심), 참조 카운트(같은 에셋을 몇 번 로드했는지 세는 값 — 0이 되어야 해제).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Addressables package manual | https://docs.unity3d.com/Packages/com.unity.addressables%40latest/ |
| Addressables Memory Management | https://docs.unity3d.com/Packages/com.unity.addressables%401.20/manual/MemoryManagement.html |
| Unity Addressables memory optimization blog | https://unity.com/blog/technology/tales-from-the-optimization-trenches-saving-memory-with-addressables |

**리서치 범위**

- **다룰 내용**: 참조 카운트, 핸들 추적, release 누락·중복 로드 재현, 수명 정책.
- **다루지 않을 내용**: 비동기·취소 기초(→ 06), 일반 에셋 메모리(→ 05).

**산출물**: `AddressableIconLoader_Bad.cs`, `AddressableAssetLoader.cs`, `AddressableHandleTracker.cs`, `Docs/addressables-lifecycle-policy.md`, 메모리 스냅샷 비교.

### 09. 씬 로딩과 팝업 수명

**목적·목표**

- **목적**: `LoadSceneAsync`·Additive Scene·Loading UI·비동기 취소를 하나의 흐름으로 안전하게 연결할 수 있게 된다.
- **목표**
  1. Single/Additive 로딩과 진행도·활성화 제어를 확인한다.
  2. Boot → Loading → Lobby → BattleMock 전환 흐름을 만든다.
  3. 로딩 중 취소·중복 로딩·빠른 뒤로가기 상황을 재현하고 전환 서비스를 개선한다.

**핵심 용어**: Additive Scene(기존 씬 위에 더해 로드하는 씬 — UI·로딩 흐름에 사용), 활성화(Activation, 로드 완료된 씬을 실제로 켜는 시점 제어).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| SceneManager.LoadSceneAsync API | https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html |
| AsyncOperation API | https://docs.unity3d.com/6000.2/Documentation/ScriptReference/AsyncOperation.html |
| Unity Awaitable / async-await support | https://docs.unity3d.com/6000.0/Documentation/Manual/async-await-support.html |

**리서치 범위**

- **다룰 내용**: 비동기 씬 로딩, Additive, Loading UI, 로딩 중 취소·중복 처리.
- **다루지 않을 내용**: 비동기·취소 기초(→ 06), 팝업 트윈 연출(→ 12).

**산출물**: `SceneTransitionService.cs`, `LoadingView.cs`, Boot/Lobby/BattleMock 씬, `Docs/scene-loading-lifecycle.md`.

---

## 아키텍처·테스트·툴링

### 00. Lab 템플릿과 AI 코드 리뷰 기준

**목적·목표**

- **목적**: 이후 모든 Lab에 재사용할 프로젝트 기본 구조와, AI가 만든 코드를 PR처럼 검토할 체크리스트를 만든다.
- **목표**
  1. `Runtime`/`Editor`/`Tests`/`Docs`로 나뉜 Lab 템플릿과 Assembly Definition 구성을 만든다.
  2. AI 코드 리뷰 체크리스트(수명·할당·테스트 가능성 등)를 문서로 정리한다.
  3. 템플릿 복사만으로 새 Lab을 시작할 수 있는지 확인한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity C# Style Guide eBook | https://unity.com/resources/create-code-c-sharp-style-guide-e-book |
| Unity Version Control Best Practices | https://unity.com/resources/best-practices-version-control-unity-6 |
| Unity Assembly Definition 문서 | https://docs.unity3d.com/Manual/cus-asmdef.html |

**리서치 범위**

- **다룰 내용**: Lab 폴더 구조, asmdef 분리, README·문서 템플릿, AI 리뷰 체크리스트.
- **다루지 않을 내용**: 개별 기술 주제(이후 Lab에서 다룸), CI 자동화(→ 20).

**산출물**: `00_UnityLabTemplate` 프로젝트, `Docs/ai-review-checklist.md`, `Docs/lab-note-template.md`, `README.md`.

### 08. EditMode Test와 순수 C# 로직 분리

**목적·목표**

- **목적**: 테스트 가능한 구조와 테스트하기 어려운 구조를 구분하고, 도메인 로직을 분리해 EditMode 테스트를 작성할 수 있게 된다.
- **목표**
  1. EditMode/PlayMode 차이와 NUnit 기본 assertion을 확인한다.
  2. 보상·쿨타임·인벤토리 정렬 로직을 MonoBehaviour에서 분리한다.
  3. EditMode 테스트를 작성하고, AI에게 누락된 엣지 케이스를 요청해 보강한다.

**핵심 용어**: EditMode 테스트(에디터에서 게임 실행 없이 도는 테스트 — 순수 로직 검증에 적합), 도메인 로직(게임 규칙 계산처럼 엔진과 무관한 로직 — 분리하면 테스트 쉬움).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Test Framework overview | https://docs.unity3d.com/Packages/com.unity.test-framework%402.0/manual/index.html |
| Unity automated tests guide | https://unity.com/how-to/automated-tests-unity-test-framework |
| Unity Test Framework learning materials | https://docs.unity3d.com/Packages/com.unity.test-framework%401.3/manual/course/overview.html |

**리서치 범위**

- **다룰 내용**: 테스트 가능한 구조 분리, EditMode 테스트, 엣지 케이스 보강.
- **다루지 않을 내용**: 런타임 회귀 테스트(→ 11), CI 자동화(→ 20).

**산출물**: `RewardCalculator.cs`, `CooldownCalculator.cs`, `InventorySorter.cs`, EditMode 테스트, `Docs/editmode-test-strategy.md`.

### 11. PlayMode 회귀 테스트

**목적·목표**

- **목적**: 버그를 고친 뒤 재발 방지 테스트로 남기는 흐름을 익히고, 런타임에서만 드러나는 문제를 검증할 수 있게 된다.
- **목표**
  1. PlayMode 테스트가 필요한 경우와 EditMode로 충분한 경우를 구분한다.
  2. 팝업 이벤트 중복 구독 같은 버그를 재현한다.
  3. 빠른 열기/닫기·씬 전환 중 async 완료·파괴 후 콜백 접근을 테스트로 남긴다.

**핵심 용어**: PlayMode 테스트(실제 실행 환경에서 도는 테스트 — 수명·이벤트 검증), 회귀 테스트(고친 버그가 다시 생기는지 막는 테스트).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Test Framework overview | https://docs.unity3d.com/Packages/com.unity.test-framework%402.0/manual/index.html |
| Running tests from the command line | https://docs.unity3d.com/Packages/com.unity.test-framework%401.0/manual/reference-command-line.html |

**리서치 범위**

- **다룰 내용**: PlayMode 테스트, 이벤트 중복 구독 재현, 수명·콜백 회귀 테스트.
- **다루지 않을 내용**: 순수 로직 EditMode 테스트(→ 08), CI 연동(→ 20).

**산출물**: `PopupController_Bad.cs`, `PopupController_Fixed.cs`, PlayMode 테스트, `Docs/playmode-regression-note.md`.

### 20. Build/Test Automation

**목적·목표**

- **목적**: 테스트와 빌드를 로컬·CI에서 재현 가능한 최소 자동화 흐름으로 만들 수 있게 된다.
- **목표**
  1. `-batchmode`·`-runTests`·`-testResults`·exit code를 확인한다.
  2. 로컬 명령줄로 EditMode 테스트를 실행하고 결과 XML을 저장한다.
  3. GitHub Actions 워크플로 초안을 만든다.

**핵심 용어**: batchmode(UI 없이 에디터를 실행하는 모드 — CI 실행), exit code(프로세스 종료 시 성공/실패 코드 — CI 성패 판단).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Command-line interface | https://docs.unity3d.com/6000.4/Documentation/Manual/CommandLineArguments.html |
| Unity Editor command line arguments | https://docs.unity3d.com/6000.4/Documentation/Manual/EditorCommandLineArguments.html |
| Unity Test Framework command line | https://docs.unity3d.com/Packages/com.unity.test-framework%402.0/manual/reference-command-line.html |
| GameCI Unity Builder | https://github.com/game-ci/unity-builder |

**리서치 범위**

- **다룰 내용**: 명령줄 테스트 실행, 결과 XML 저장, GitHub Actions 초안.
- **다루지 않을 내용**: 테스트 작성 자체(→ 08, → 11), 스토어 배포.

**산출물**: `run-editmode-tests.sh` 또는 `.bat`, `.github/workflows/test.yml`, `Docs/build-test-automation-note.md`.

### 28. 에디터 확장과 커스텀 툴

**목적·목표**

- **목적**: 커스텀 인스펙터·에디터 윈도우·검증 툴로 팀의 반복 작업을 줄이는 에디터 확장을 만들 수 있게 된다(시니어가 구조와 도구를 정하는 역량).
- **목표**
  1. `CustomEditor`로 데이터 에셋의 인스펙터를 개선한다(검증·미리보기·버튼).
  2. `EditorWindow`로 데이터 일괄 점검·생성 툴 1종을 만든다.
  3. 에셋·씬의 흔한 실수(누락 참조 등)를 찾아 경고하는 검증 스크립트를 만든다.

**핵심 용어**: CustomEditor(특정 타입의 인스펙터 표시를 재정의 — 검증·버튼 추가), EditorWindow(직접 만드는 에디터 창 — 일괄 작업 툴), OnValidate(인스펙터 값 변경 시 호출 — 즉시 검증).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Custom Editors | https://docs.unity3d.com/Manual/editor-CustomEditors.html |
| Editor windows | https://docs.unity3d.com/Manual/editor-EditorWindows.html |
| Custom Unity Editor tools (EditorTool) | https://docs.unity3d.com/Manual/UsingCustomEditorTools.html |
| UI Toolkit for Editor | https://docs.unity3d.com/Manual/UIE-HowTo-CreateEditorWindow.html |

**리서치 범위**

- **다룰 내용**: 커스텀 인스펙터, EditorWindow 툴, OnValidate·검증 스크립트, 작업 자동화.
- **다루지 않을 내용**: 런타임 UI Toolkit(→ 24), CI 빌드 자동화(→ 20).

**산출물**: 검증·버튼이 추가된 커스텀 인스펙터 1종, 데이터 일괄 점검·생성 EditorWindow 툴 1종, 에셋·씬 검증 스크립트와 사용 노트.

### 32. 아키텍처와 의존성 주입(DI)

**목적·목표**

- **목적**: 의존성 주입과 표현 분리(MVP/MVVM)로 결합도를 낮추고, 테스트 가능하고 교체 쉬운 클라이언트 구조를 설계할 수 있게 된다(시니어의 구조 설계 역량).
- **목표**
  1. 싱글톤·직접 참조의 한계를 작은 예제로 드러내고 DI로 개선한다.
  2. DI 컨테이너(예: VContainer)로 의존성을 등록·주입하고 생명주기를 관리한다.
  3. UI에 MVP/MVVM를 적용해 뷰와 로직을 분리하고 로직을 단위 테스트한다.

**핵심 용어**: 의존성 주입(DI, 필요한 객체를 외부에서 넣어 주는 방식 — 결합도·테스트성 개선), DI 컨테이너(의존성 등록·생성·주입 관리 도구 예: VContainer), MVP(뷰·발표자(Presenter)·모델로 책임 분리 — 로직 테스트 용이).

> VContainer는 Unity 공식 패키지가 아니다. 도입 시 라이선스·버전 고정을 별도로 확인한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity game architecture eBook (SOLID 등) | https://unity.com/resources/improve-your-code-with-solid-principles |
| ScriptableObject architecture eBook | https://unity.com/resources/create-modular-game-architecture-with-scriptable-objects-ebook |
| VContainer documentation | https://vcontainer.hadashikick.jp/ |
| VContainer GitHub | https://github.com/hadashiA/VContainer |

**리서치 범위**

- **다룰 내용**: 싱글톤 한계, DI 컨테이너 등록·주입·생명주기, MVP/MVVM 적용, 로직 단위 테스트.
- **다루지 않을 내용**: 도메인 데이터 설계(→ 13), 테스트 작성 기초(→ 08).

**산출물**: DI 적용 전후 비교 샘플과 컨테이너 구성 코드, MVP/MVVM 화면 1종과 로직 단위 테스트, 아키텍처·DI 적용 기준 노트.

---

## 출시·운영·네트워크

### 22. 최종 문서 정리와 공개 글 선정

**목적·목표**

- **목적**: 지금까지 만든 Lab 중 공개해도 안전하고 커리어에 강한 문서를 선별·정리한다.
- **목표**
  1. Lab 목록에서 커리어 증거로 강한 5개를 고른다.
  2. 공개 가능한 글 후보의 제목·요약을 작성한다.
  3. 회사·내부 정보가 드러나지 않는지 점검한다.

**선행 학습 자료**: 작성한 Lab README(각 Lab `Docs/`), AI 리뷰 체크리스트(→ 00), 문서 작성 원칙(저장소 writing-guidelines), FinalProject 커밋 로그.

**리서치 범위**

- **다룰 내용**: 공개 후보 선정, 글 제목·요약, 정보 노출 점검.
- **다루지 않을 내용**: 개별 기술 주제(각 Lab 문서), 진단·로깅 구현(→ 23).

**산출물**: `Docs/public-article-candidates.md`, `Docs/final-project-rules.md`, `Docs/ai-mistake-catalog-draft.md`.

### 23. Diagnostics / QA / Logging

**목적·목표**

- **목적**: 로컬에서만 재현되는 기능을 QA가 재현 가능한 형태로 바꾸도록 진단·로깅·디버그 오버레이를 설계할 수 있게 된다.
- **목표**
  1. crash·exception·ANR·telemetry 등 QA 재현 정보 항목을 정리한다.
  2. FPS·현재 씬·메모리·Addressables 핸들 수를 보여 주는 DebugOverlay와 마지막 행동 기록기를 만든다.
  3. QA가 버그를 재현·보고할 수 있는 리포트 템플릿을 만든다.

**핵심 용어**: Telemetry(실행 중 수집되는 상태·이벤트 데이터), ANR(앱이 응답하지 않는 상태, Application Not Responding), 디버그 오버레이(화면에 상태값을 띄우는 진단 UI).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Cloud Diagnostics | https://docs.unity.com/en-us/cloud-diagnostics |
| Getting Started with Cloud Diagnostics | https://docs.unity.com/en-us/cloud-diagnostics/getting-started-with-cloud-diagnostics |
| Unity Dashboard Diagnostics | https://docs.unity.com/en-us/cloud/developer-data/diagnostics |

**리서치 범위**

- **다룰 내용**: 진단 정보 항목 정의, 디버그 오버레이, 마지막 행동 로깅, QA 재현 리포트 템플릿.
- **다루지 않을 내용**: 최종 통합·포트폴리오 정리, 성능 측정 자체(→ 01).

**산출물**: `DebugOverlay.cs`, `ClientLogger.cs`, `LastActionRecorder.cs`, `Docs/qa-report-template.md`, `Docs/diagnostics-and-qa-policy.md`.

### 33. 멀티플레이어·네트워킹 기초

**목적·목표**

- **목적**: 클라이언트-서버 동기화의 기본(상태 복제·RPC·소유권)을 이해하고, 지연·끊김에 대응하는 기초를 다룰 수 있게 된다.
- **목표**
  1. Netcode for GameObjects 등으로 호스트-클라이언트 연결과 오브젝트 스폰을 구현한다.
  2. 네트워크 변수(상태 복제)와 RPC(명령 전달)의 차이를 예제로 확인한다.
  3. 인위적 지연·패킷 손실을 넣어 보간·예측의 필요성을 관찰한다.

**핵심 용어**: 상태 복제(서버 값을 클라이언트로 자동 동기화 — 네트워크 변수), RPC(원격 호출 — 명령 전달), 소유권(Authority, 어떤 객체를 누가 통제하는지 — 충돌·치트 방지 핵심).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Netcode for GameObjects | https://docs-multiplayer.unity3d.com/netcode/current/about/ |
| NetworkVariable | https://docs-multiplayer.unity3d.com/netcode/current/basics/networkvariable/ |
| RPCs | https://docs-multiplayer.unity3d.com/netcode/current/advanced-topics/messaging-system/ |
| Unity Multiplayer overview | https://unity.com/solutions/multiplayer |

**리서치 범위**

- **다룰 내용**: 연결·스폰, 네트워크 변수·RPC, 소유권, 지연·손실 관찰과 보간/예측의 필요성.
- **다루지 않을 내용**: 전용 서버·매치메이킹 인프라, 안티치트(→ 35).

**산출물**: 호스트-클라이언트 연결·스폰 데모, 네트워크 변수/RPC 비교 예제, 지연·손실 환경 관찰 노트.

### 34. 라이브옵스(Remote Config·Analytics·IAP)

**목적·목표**

- **목적**: 출시 후 운영에 필요한 원격 설정·지표 수집·인앱 결제의 기본 흐름을 이해하고, 클라이언트에서 안전하게 연동할 수 있게 된다.
- **목표**
  1. Remote Config로 빌드 재배포 없이 값(밸런스·플래그)을 바꾸는 흐름을 구현한다.
  2. Analytics로 핵심 이벤트(진입·이탈·구매)를 정의·전송한다.
  3. In-App Purchasing의 구매·복원·영수증 검증 흐름을 정리한다.

**핵심 용어**: Remote Config(서버에서 클라이언트 설정값을 내려 주는 기능 — 재배포 없이 변경), 이벤트(Analytics, 사용자 행동을 기록하는 단위), 영수증 검증(결제가 정당한지 확인 — 치트·환불 대응).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Remote Config | https://docs.unity.com/ugs/manual/remote-config/manual/welcome |
| Unity Analytics | https://docs.unity.com/ugs/manual/analytics/manual/overview |
| Unity In-App Purchasing | https://docs.unity3d.com/Packages/com.unity.purchasing@latest |
| Unity Gaming Services overview | https://unity.com/solutions/gaming-services |

**리서치 범위**

- **다룰 내용**: Remote Config 값 변경 흐름, Analytics 이벤트 설계·전송, IAP 구매·복원·영수증 검증.
- **다루지 않을 내용**: 서버 측 분석 파이프라인, 안티치트(→ 35).

**산출물**: Remote Config로 값을 바꾸는 데모와 적용 노트, 핵심 Analytics 이벤트 정의 표, IAP 구매·복원·검증 흐름 정리.

### 35. 보안·안티치트·데이터 보호

**목적·목표**

- **목적**: 클라이언트 데이터 변조·메모리 조작·통신 가로채기의 위협을 이해하고, 현실적인 방어선을 설계할 수 있게 된다.
- **목표**
  1. 세이브 파일·PlayerPrefs 변조를 재현하고, 무결성 검증·난독화로 난이도를 올린다.
  2. 메모리 값 조작(치트 도구) 위협을 이해하고, 핵심 값의 서버 권위 검증 원칙을 정리한다.
  3. 통신 구간 보호(HTTPS·인증서)와 민감 키 관리의 기본을 정리한다.

**핵심 용어**: 무결성 검증(데이터가 변조되지 않았는지 확인 — 해시·서명), 서버 권위(중요한 판정을 서버가 최종 결정 — 클라이언트 불신), 난독화(코드·데이터를 읽기 어렵게 — 지연 효과, 완전 방어 아님).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| OWASP Mobile Top 10 | https://owasp.org/www-project-mobile-top-10/ |
| Unity IAP receipt validation | https://docs.unity3d.com/Packages/com.unity.purchasing@latest/manual/BackendReceiptValidation.html |
| Android app security best practices | https://developer.android.com/privacy-and-security/security-tips |
| iOS Data protection / Keychain | https://developer.apple.com/documentation/security/keychain_services |

**리서치 범위**

- **다룰 내용**: 세이브·설정 변조 재현과 방어, 무결성 검증·난독화, 서버 권위 원칙, 통신·키 보호 기본.
- **다루지 않을 내용**: 상용 안티치트 SDK 심층 비교, 전용 서버 보안 인프라 구축.

**산출물**: 세이브 변조 재현과 무결성 검증 적용 전후 비교, "클라이언트에서 믿으면 안 되는 값" 목록과 서버 권위 원칙 노트, 통신·키 관리 기본 점검 항목.

### 39. 라이브 서비스 콘텐츠 패치

**목적·목표**

- **목적**: 출시 후 스토어 재배포 없이 에셋·데이터를 갱신하는 콘텐츠 패치 흐름을 이해하고, 원격 카탈로그·버전 호환·롤백을 다룰 수 있게 된다.
- **목표**
  1. Addressables 원격 카탈로그와 원격 호스팅을 구성해 런타임에 새 콘텐츠를 받는 흐름을 만든다.
  2. 콘텐츠 업데이트 빌드(Update a Previous Build)와 "Check for Content Update Restrictions"로 변경분만 패치한다.
  3. 패치 실패·구버전 클라이언트 호환·롤백 전략과 다운로드 진행·재시도 UX를 정리한다.

**핵심 용어**: 콘텐츠 패치(코드 재배포 없이 에셋·데이터만 갱신 — 스토어 심사 회피), 원격 카탈로그(어떤 에셋이 어디 있는지 적은 원격 목록 — 해시로 갱신 판단), CCD(Unity Cloud Content Delivery, 콘텐츠 호스팅·배포 서비스).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Addressables Content update builds | https://docs.unity3d.com/Packages/com.unity.addressables@latest/manual/ContentUpdateWorkflow.html |
| Remote content & catalogs | https://docs.unity3d.com/Packages/com.unity.addressables@latest/manual/RemoteContentDistribution.html |
| Unity Cloud Content Delivery (CCD) | https://docs.unity.com/ugs/manual/ccd/manual/UnityCCD |

**리서치 범위**

- **다룰 내용**: 원격 카탈로그·호스팅, 콘텐츠 업데이트 빌드, 변경분 패치, 버전 호환·롤백, 다운로드 UX.
- **다루지 않을 내용**: 코드 로직 핫픽스(→ 36, → 37), Addressables 기본 로드/해제(→ 07).
- **선행**: 07. Addressables Load/Release 수명 관리

**산출물**: 원격 카탈로그로 새 콘텐츠를 받는 데모와 업데이트 빌드 절차, 변경분 패치·롤백 전략 노트, 다운로드 진행·재시도 UX 메모.

### 40. 라이브 에러 수집과 대응

**목적·목표**

- **목적**: 출시된 클라이언트의 크래시·예외를 자동 수집·심볼화하고, 우선순위를 정해 대응하는 운영 흐름을 설계할 수 있게 된다.
- **목표**
  1. 크래시·예외 리포팅 도구를 연동해 스택트레이스와 디바이스·OS 정보를 수집한다.
  2. IL2CPP 빌드의 심볼(디버그 심볼) 업로드로 읽을 수 있는 스택트레이스를 확보한다.
  3. 사용자 메타데이터·재현 단계(마지막 행동)를 함께 보내 분류·우선순위·대응 절차를 만든다.

**핵심 용어**: 크래시 리포팅(비정상 종료·예외를 자동 수집 — 스택트레이스 포함), 심볼화(Symbolication, 난독·압축된 스택을 읽을 수 있게 복원), 그룹화(같은 원인의 리포트를 묶기).

> Unity Cloud Diagnostics는 향후 단계적으로 폐지 예정이다. 외부 도구(Sentry 등)와 Unity 6.2+ 신규 리포팅을 함께 검토한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Cloud Diagnostics - Crash/Exception Reporting | https://docs.unity.com/ugs/manual/cloud-diagnostics/manual/CrashandExceptionReporting/AboutCrashandExceptionReporting |
| CrashReportHandler API | https://docs.unity3d.com/ScriptReference/CrashReportHandler.CrashReportHandler.html |
| Sentry for Unity | https://docs.sentry.io/platforms/unity/ |
| Sentry IL2CPP Line Numbers | https://docs.sentry.io/platforms/unity/configuration/il2cpp/ |

**리서치 범위**

- **다룰 내용**: 크래시·예외 자동 수집, IL2CPP 심볼 업로드·심볼화, 메타데이터·재현 단계, 그룹화·우선순위·대응 절차.
- **다루지 않을 내용**: 로컬 디버그 오버레이·로깅 구현(→ 23), 지표 분석(→ 34).
- **선행**: 23. Diagnostics / QA / Logging · **연계**: 31, 34

**산출물**: 크래시 리포팅 연동 데모와 심볼 업로드 절차, 메타데이터·재현 단계 설계와 분류·우선순위 기준, 라이브 에러 대응 절차(트리아지) 노트.

---

## 스크립팅·핫업데이트

### 36. xLua 스크립팅과 핫픽스

**목적·목표**

- **목적**: xLua로 C# 로직 일부를 Lua로 작성·교체해, 스토어 재심사 없이 런타임 버그를 고치는 핫픽스(hotfix) 흐름을 이해하고 위험을 다룰 수 있게 된다.
- **목표**
  1. xLua를 설치하고 C#↔Lua 상호 호출(함수·객체·콜백) 기본을 동작시킨다.
  2. `HOTFIX_ENABLE`·코드 생성·인젝션을 거쳐 기존 C# 메서드를 Lua로 교체(hotfix)하는 예제를 재현한다.
  3. 핫픽스의 적용 범위·한계(제네릭·구조체·플랫폼 제약)와 배포 절차를 정리한다.

**핵심 용어**: 핫픽스(Hotfix, 빌드 재배포 없이 실행 중 로직 수정 — 스토어 재심사 회피), 바인딩/코드 생성(C# 타입을 Lua에서 부르게 하는 연결 코드 — 성능·용량 영향), 인젝션(빌드된 C# 메서드에 Lua 교체 지점을 심기 — HOTFIX_ENABLE 필요).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Tencent/xLua 저장소 | https://github.com/Tencent/xLua |
| xLua README (English) | https://github.com/Tencent/xLua/blob/master/README_EN.md |
| xLua Hotfix 가이드 (English) | https://github.com/Tencent/xLua/blob/master/Assets/XLua/Doc/Hotfix_EN.md |
| xLua Hotfix 예제 (08_Hotfix) | https://github.com/Tencent/xLua/tree/master/Assets/XLua/Examples/08_Hotfix |
| Lua 5.3 Reference Manual | https://www.lua.org/manual/5.3/ |

**리서치 범위**

- **다룰 내용**: xLua 설치·상호 호출, 코드 생성, 핫픽스 인젝션·교체, 적용 한계, 배포 절차.
- **다루지 않을 내용**: TypeScript 기반 대안(→ 37), 에셋 콘텐츠 패치(→ 39), Lua로 짠 게임 전체 아키텍처.
- **비교 대상**: 37. PuerTS(TypeScript) 스크립팅

**산출물**: C#↔Lua 상호 호출 샘플과 핫픽스 교체 데모, 핫픽스 적용 한계·플랫폼 제약 정리 노트, Lua 패치 배포 절차 메모.

### 37. PuerTS(TypeScript) 스크립팅

**목적·목표**

- **목적**: PuerTS로 게임 로직을 TypeScript로 작성·갱신하고, 정적 타입의 장점과 스크립트 런타임의 비용·핫업데이트 가능성을 이해할 수 있게 된다.
- **목표**
  1. PuerTS를 설치하고 C#↔TypeScript 상호 호출과 타입 선언(.d.ts) 생성을 동작시킨다.
  2. 간단한 게임 로직을 TypeScript로 작성해 C# 호스트와 연결한다.
  3. xLua(Lua)와 비교해 타입 안정성·성능·코드 용량 측면의 차이를 정리한다.

**핵심 용어**: PuerTS(Unity/UE에서 TS·JS·Lua·Python을 쓰게 하는 스크립팅 솔루션 — Tencent 제작), 타입 선언(.d.ts, C# API를 TypeScript에서 타입 안전하게 부르게 하는 선언 — 자동 생성), 스크립트 엔진(TS/JS를 실행하는 런타임 v8·quickjs·nodejs — 성능·용량 절충).

> PuerTS는 Unity 공식 패키지가 아니다. 도입 시 라이선스·버전 고정·스크립트 엔진 선택을 별도로 확인한다.

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Tencent/puerts 저장소 | https://github.com/Tencent/puerts |
| PuerTS 설치 문서 (Unity, English) | https://github.com/Tencent/puerts/blob/master/doc/unity/en/install.md |
| PuerTS 성능·IL2CPP 문서 | https://github.com/Tencent/puerts/blob/master/doc/unity/en/performance/il2cpp.md |
| PuerTS 공식 문서 사이트 | https://puerts.github.io/en/docs/puerts/unity/ |
| TypeScript Handbook | https://www.typescriptlang.org/docs/handbook/intro.html |

**리서치 범위**

- **다룰 내용**: PuerTS 설치·상호 호출, 타입 선언 생성, TS 로직 연결, 스크립트 엔진 선택, xLua와 비교.
- **다루지 않을 내용**: Lua 기반 대안 자체(→ 36), 에셋 콘텐츠 패치(→ 39).
- **비교 대상**: 36. xLua 스크립팅과 핫픽스

**산출물**: C#↔TypeScript 상호 호출 샘플과 타입 선언 생성 결과, TS로 작성한 게임 로직 데모, xLua vs PuerTS 비교 표(타입 안정성·성능·용량·팀 숙련도).

---

## 1인 개발·사업·운영

> 클라이언트 엔지니어링 밖에서 "혼자 만들고 서비스를 지속"하는 데 필요한 축. 향후 필요 시 꺼내 리서칭한다.

### 41. 서버·백엔드 연동(BaaS)

**목적·목표**

- **목적**: 1인·소규모로도 라이브 서비스를 지탱하도록, 직접 서버를 짜기보다 관리형 백엔드(BaaS, Backend as a Service)를 골라 인증·저장·랭킹·재화를 연동하고 "서버 권위" 경계를 설계할 수 있게 된다.
- **목표**
  1. UGS·PlayFab·Firebase·Nakama 등 후보를 기능·가격·운영 부담으로 비교해 선택 기준을 만든다.
  2. 익명/계정 인증과 클라우드 세이브를 연동해 기기 교체에도 진행이 유지되게 한다.
  3. 랭킹과 재화(경제)를 서버 권위로 처리하고, 클라이언트에서 "믿으면 안 되는 값"의 경계를 정리한다.

**핵심 용어**: BaaS(인증·DB·서버 로직을 제공하는 관리형 백엔드 — 직접 운영 부담 절감), 서버 권위(중요한 판정을 서버가 최종 결정 — 치트 방지 핵심), 클라우드 세이브(진행 데이터를 서버에 보관·동기화 — 기기 교체 대비).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Gaming Services 개요 | https://docs.unity.com/ugs/en-us/manual/overview/manual/unity-gaming-services-home |
| UGS Authentication | https://docs.unity.com/ugs/manual/authentication/manual/get-started |
| UGS Cloud Save | https://docs.unity.com/ugs/manual/cloud-save/manual |
| UGS Leaderboards / Economy | https://docs.unity.com/ugs/manual/leaderboards/manual/unity-leaderboards-home |
| Microsoft PlayFab 문서 | https://learn.microsoft.com/en-us/gaming/playfab/ |
| Firebase for games | https://firebase.google.com/docs |
| Nakama (Heroic Labs) | https://heroiclabs.com/docs/nakama/ |

**리서치 범위**

- **다룰 내용**: BaaS 선택 기준(기능·가격·운영), 인증, 클라우드 세이브, 랭킹·재화의 서버 권위, 클라이언트 신뢰 경계.
- **다루지 않을 내용**: 실시간 멀티플레이어 클라이언트(→ 33), 직접 서버·DB·DevOps 구축, 결제 연동(→ 34).
- **연계**: 33. 멀티플레이어·네트워킹 기초, 35. 보안·안티치트·데이터 보호

**산출물**: BaaS 후보 비교 표(기능·가격·운영 부담), 인증 + 클라우드 세이브 연동 데모, "서버가 판정해야 하는 값" 목록과 권위 경계 노트.

### 42. 게임 디자인 기초

**목적·목표**

- **목적**: 기술이 아니라 "재미"를 설계하는 언어를 익혀, 코어 루프·진행·밸런싱을 스스로 정의하고 검증할 수 있게 된다.
- **목표**
  1. 만들 게임의 코어 게임플레이 루프를 한 장으로 그리고, 그 루프가 왜 재미있는지 설명한다.
  2. 진행(progression)·난이도 곡선·보상 구조를 설계하고 스프레드시트로 밸런싱을 시뮬레이션한다.
  3. 종이 프로토타입 또는 그레이박스로 핵심 재미를 빠르게 검증한다.

**핵심 용어**: 코어 루프(플레이어가 반복하는 핵심 행동 사이클 예: 전투→보상→성장), 진행(Progression, 시간에 따라 열리는 콘텐츠·성장 곡선), 그레이박스(아트 없이 회색 도형으로 만든 검증용 빌드).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| The Art of Game Design (Jesse Schell) | https://www.schellgames.com/art-of-game-design/ |
| A Theory of Fun for Game Design (Raph Koster) | https://www.theoryoffun.com/ |
| Rules of Play (Salen & Zimmerman) | https://mitpress.mit.edu/9780262240451/rules-of-play/ |
| Level Up! (Scott Rogers) | https://www.wiley.com/en-us/Level+Up!+The+Guide+to+Great+Video+Game+Design%2C+2nd+Edition-p-9781118877197 |
| GDC Vault (디자인 강연) | https://www.gdcvault.com/ |
| Game Maker's Toolkit (YouTube) | https://www.youtube.com/@GMTK |

**리서치 범위**

- **다룰 내용**: 코어 루프 정의, 진행·난이도 곡선, 보상 구조, 스프레드시트 밸런싱, 종이·그레이박스 프로토타입.
- **다루지 않을 내용**: 수익화 설계(→ 45), 구현 기술(다른 리서치 주제들).
- **연계**: 45. 라이브 운영 디자인과 데이터 의사결정

**산출물**: 코어 루프 1장 다이어그램과 재미 가설, 진행·밸런싱 스프레드시트, 종이/그레이박스 프로토타입과 검증 메모.

### 43. 콘텐츠·아트 파이프라인

**목적·목표**

- **목적**: 1인 개발의 실제 병목인 콘텐츠 생산을 다루도록, 아트 에셋의 규격·임포트 표준·제작/외주 워크플로를 세워 일관되고 빠르게 콘텐츠를 찍어낼 수 있게 된다.
- **목표**
  1. 스프라이트·텍스처·오디오·모델의 임포트 규격(해상도·압축·네이밍·폴더 구조)을 표준으로 정한다.
  2. 아틀라스·스프라이트 시트·LOD 등 런타임 비용을 고려한 에셋 가공 절차를 만든다.
  3. 직접 제작과 외주/마켓 에셋을 섞는 워크플로와 라이선스 점검 절차를 정리한다.

**핵심 용어**: 임포트 규격(에셋을 들일 때 적용하는 압축·해상도 등 기준 — 일관성·용량 관리), 아틀라스(여러 스프라이트를 한 텍스처로 묶기 — 드로우콜 절감), 에셋 라이선스(외주·마켓 에셋의 사용 허용 범위 — 상용 배포 전 필수 점검).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Importing assets (Unity Manual) | https://docs.unity3d.com/Manual/ImportingAssets.html |
| Texture import settings | https://docs.unity3d.com/Manual/class-TextureImporter.html |
| Sprite Atlas | https://docs.unity3d.com/Manual/sprite-atlas.html |
| Presets (임포트 표준화) | https://docs.unity3d.com/Manual/Presets.html |
| Unity Asset Store 라이선스 | https://unity.com/legal/as-terms |
| Creative Commons 라이선스 | https://creativecommons.org/licenses/ |

**리서치 범위**

- **다룰 내용**: 임포트 규격·네이밍·폴더 표준, 아틀라스·시트·LOD 가공, 외주/마켓 워크플로, 라이선스 점검.
- **다루지 않을 내용**: 아트 제작 기술 자체(드로잉·모델링), 셰이더·VFX 구현(→ 14, → 21), 에디터 임포트 자동화 툴(→ 28).
- **연계**: 28. 에디터 확장과 커스텀 툴

**산출물**: 에셋 임포트 규격·네이밍·폴더 표준 문서, 아틀라스·가공 절차와 적용 예제, 외주/마켓 에셋 워크플로와 라이선스 점검 체크리스트.

### 44. 스토어 출시와 사업·법무

**목적·목표**

- **목적**: 만든 게임을 실제로 출시·유지하도록, 스토어 심사·노출(ASO)·법무(개인정보·연령등급)·결제/세금의 비기술 영역을 이해하고 출시 체크리스트를 만들 수 있게 된다.
- **목표**
  1. App Store·Google Play 심사 정책의 흔한 반려 사유를 정리하고 출시 전 점검 항목을 만든다.
  2. 개인정보처리방침·연령등급(IARC)·GDPR/COPPA 등 필수 법무 요건을 파악한다.
  3. ASO(제목·키워드·스크린샷)와 결제/세금·정산 흐름의 기본을 정리한다.

**핵심 용어**: ASO(앱 스토어 검색 노출 최적화 — 제목·키워드·스크린샷), IARC(국제 연령등급 통합 시스템), 개인정보처리방침(수집·이용·보관 고지 문서 — 스토어 등록 필수).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| App Store Review Guidelines | https://developer.apple.com/app-store/review/guidelines/ |
| Google Play Developer Policy Center | https://support.google.com/googleplay/android-developer/answer/9858738 |
| Apple App Store Product Page (ASO) | https://developer.apple.com/app-store/product-page/ |
| IARC 연령등급 | https://www.globalratings.com/ |
| GDPR 공식 안내 | https://gdpr.eu/ |
| Google Play Families / COPPA 정책 | https://support.google.com/googleplay/android-developer/answer/9893335 |

**리서치 범위**

- **다룰 내용**: 스토어 심사·반려 사유, ASO, 개인정보처리방침·연령등급·GDPR/COPPA, 결제/세금·정산 개요.
- **다루지 않을 내용**: 클라이언트 IAP 구현(→ 34), 마케팅/UA 캠페인 운영 심화.
- **연계**: 34. 라이브옵스, 35. 보안·안티치트·데이터 보호

**산출물**: 출시 전 심사·법무 점검 체크리스트, 개인정보처리방침·연령등급 준비 메모, ASO 요소 초안과 결제/세금 흐름 정리.

### 45. 라이브 운영 디자인과 데이터 의사결정

**목적·목표**

- **목적**: 출시 후 지표를 읽고 무엇을 바꿀지 결정하는 운영 루프를 갖추도록, 리텐션·수익화 설계와 코호트·A/B 기반 의사결정을 이해할 수 있게 된다.
- **목표**
  1. 핵심 지표(리텐션 D1/D7/D30, DAU/MAU, ARPU/ARPPU, 전환율)를 정의하고 대시보드로 본다.
  2. 코호트·퍼널 분석으로 이탈 지점을 찾고 가설을 세운다.
  3. A/B 테스트로 변경(밸런스·가격·온보딩)을 검증하고, 건전한 수익화 설계 원칙을 정리한다.

**핵심 용어**: 리텐션(가입 후 N일에 돌아온 비율 D1/D7/D30), 코호트(같은 시점에 들어온 사용자 묶음), ARPU/ARPPU(사용자당·결제자당 평균 매출), 퍼널(단계별 이탈을 보는 흐름 분석).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Analytics | https://docs.unity.com/ugs/manual/analytics/manual/overview |
| GameAnalytics 문서 | https://docs.gameanalytics.com/ |
| Deconstructor of Fun | https://www.deconstructoroffun.com/ |
| GameAnalytics 모바일 벤치마크 | https://gameanalytics.com/reports/ |
| A/B testing 개념 | https://www.optimizely.com/optimization-glossary/ab-testing/ |

**리서치 범위**

- **다룰 내용**: 핵심 지표 정의·대시보드, 코호트·퍼널 분석, A/B 테스트, 리텐션·수익화 설계 원칙, 운영 의사결정 루프.
- **다루지 않을 내용**: 이벤트 전송·Remote Config 구현(→ 34), 코어 재미 설계(→ 42).
- **선행**: 34. 라이브옵스, 42. 게임 디자인 기초

**산출물**: 핵심 지표 정의 표와 대시보드 구성안, 코호트·퍼널 분석으로 도출한 이탈 가설, A/B 테스트 설계 예시와 수익화 설계 원칙 노트.

---

## 추가 주제(46–61)

기존 46개에서 빠져 있던 **모바일 플랫폼 통합·온디바이스 AI·데이터/콘텐츠 파이프라인**을 보강한 주제다. 번호는 작성 순서이며, 분류는 각 절 제목 옆 대괄호로 표기한다(인덱스의 분류와 동일). 선행 학습 자료의 URL은 2026-06 기준 공식 문서를 확인해 채웠다.

### 46. Adaptive Performance·프레임 페이싱·발열/배터리 〔성능·메모리〕

**목적·목표**

- **목적**: 모바일 기기의 발열·전력 상태에 따라 품질을 동적으로 낮춰, 쓰로틀링과 배터리 소모로 인한 체감 저하를 다룰 수 있게 된다.
- **목표**
  1. `Application.targetFrameRate`·`QualitySettings.vSyncCount`로 목표 프레임과 프레임 페이싱을 제어한다.
  2. Adaptive Performance로 온도·전력 경고 이벤트를 받아 해상도·LOD·이펙트 품질을 단계적으로 낮춘다.
  3. 발열 누적 시나리오를 재현하고 동적 스케일러 적용 전후의 프레임·온도 추이를 비교한다.

**핵심 용어**: 쓰로틀링(기기 과열 시 CPU/GPU 클럭을 강제로 낮추는 것), 스케일러(Scaler, 부하에 따라 해상도·품질 항목을 조절하는 단위), 프레임 페이싱(프레임 간격을 고르게 유지해 끊김을 줄이는 것).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Adaptive Performance 패키지 매뉴얼 | https://docs.unity3d.com/Packages/com.unity.adaptiveperformance@6.0/manual/index.html |
| Adaptive Performance (Unity Manual) | https://docs.unity3d.com/6000.2/Documentation/Manual/com.unity.adaptiveperformance.html |
| Application.targetFrameRate | https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html |
| QualitySettings.vSyncCount | https://docs.unity3d.com/ScriptReference/QualitySettings-vSyncCount.html |

**리서치 범위**

- **다룰 내용**: 목표 프레임·vsync 제어, 온도·전력 이벤트, 동적 스케일러, 발열 시나리오 측정.
- **다루지 않을 내용**: 프레임당 연산 비용 자체(→ 01, → 29), 빌드 용량(→ 31).

**산출물**: 프레임 페이싱·targetFrameRate 적용 노트, 동적 스케일러 적용 전후 프레임·온도 비교, 발열 대응 정책 노트.

### 47. 앱 생명주기·플랫폼 통합 〔출시·운영·네트워크〕

**목적·목표**

- **목적**: 포커스/일시정지·복귀, Safe Area(노치), 런타임 권한, 딥링크 등 OS와 맞닿는 모바일 기반을 안전하게 처리할 수 있게 된다.
- **목표**
  1. `OnApplicationPause`/`OnApplicationFocus`로 백그라운드 전환 시 타이머·오디오·세이브를 안전하게 처리한다.
  2. `Screen.safeArea`로 노치·둥근 모서리 기기에서 UI 잘림을 막는다.
  3. 런타임 권한 요청과 딥링크(`Application.deepLinkActivated`) 진입 흐름을 구현한다.

**핵심 용어**: Safe Area(노치·홈 인디케이터를 피한 안전 표시 영역), 런타임 권한(실행 중 사용자에게 카메라·알림 등 권한을 요청), 딥링크(특정 화면으로 바로 진입하는 URL).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| MonoBehaviour.OnApplicationPause | https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html |
| Screen.safeArea | https://docs.unity3d.com/ScriptReference/Screen-safeArea.html |
| Deep linking (Unity Manual) | https://docs.unity3d.com/Manual/deep-linking.html |
| Requesting permissions (Android) | https://docs.unity3d.com/Manual/android-RequestingPermissions.html |

**리서치 범위**

- **다룰 내용**: 일시정지·포커스·복귀 처리, Safe Area 레이아웃, 런타임 권한, 딥링크 진입.
- **다루지 않을 내용**: 푸시 알림 발송(→ 48), 어트리뷰션·딥링크 마케팅 연동(→ 60).

**산출물**: 생명주기 처리 샘플(타이머·오디오·세이브), Safe Area 적용 UI, 권한·딥링크 진입 데모와 정책 노트.

### 48. 로컬·푸시 알림(Mobile Notifications) 〔출시·운영·네트워크〕

**목적·목표**

- **목적**: 로컬 알림 스케줄링과 FCM/APNs 푸시로 리텐션 훅을 만들고, 권한·채널·예약 정책을 다룰 수 있게 된다.
- **목표**
  1. Mobile Notifications 패키지로 로컬 알림(1회·반복)을 예약·취소하고 Android 알림 채널을 구성한다.
  2. FCM/APNs로 서버 발송 푸시를 수신하고, 알림 탭 진입을 딥링크와 연결한다.
  3. 알림 권한(Android 13+)·발송 빈도·야간 차단 등 운영 정책을 정리한다.

**핵심 용어**: 로컬 알림(앱이 기기에 직접 예약하는 알림), 푸시 알림(서버가 FCM/APNs 통해 보내는 원격 알림), 알림 채널(Android 8+의 알림 분류 단위).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Mobile Notifications (Unity Manual) | https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.mobile.notifications.html |
| Mobile Notifications 패키지 | https://docs.unity3d.com/Packages/com.unity.mobile.notifications@2.4/manual/index.html |
| UGS Push Notifications | https://docs.unity.com/ugs/en-us/manual/push-notifications/manual/overview |
| Firebase Cloud Messaging (Unity) | https://firebase.google.com/docs/cloud-messaging/unity/client |

**리서치 범위**

- **다룰 내용**: 로컬 알림 예약·취소·채널, FCM/APNs 푸시 수신, 알림 탭 진입, 권한·빈도 정책.
- **다루지 않을 내용**: 앱 생명주기·딥링크 기반(→ 47), 분석 이벤트 설계(→ 34).

**산출물**: 로컬 알림 예약·채널 샘플, 푸시 수신·진입 데모, 알림 권한·발송 정책 노트.

### 49. 게임 데이터 테이블 파이프라인 〔게임플레이·데이터〕

**목적·목표**

- **목적**: Excel/CSV/Google Sheets의 기획 밸런스 데이터를 코드와 분리해 SO·바이너리로 변환하는 재현 가능한 파이프라인을 만들 수 있게 된다.
- **목표**
  1. CSV/스프레드시트를 ScriptedImporter·AssetPostprocessor로 SO/직렬화 에셋으로 변환하는 임포트 흐름을 만든다.
  2. 컬럼 추가·타입 변경·키 누락 같은 데이터 변경에 견디는 스키마·검증을 둔다.
  3. 기획자 수정 → 임포트 → 런타임 반영의 왕복을 빠르게 만드는 워크플로를 정리한다.

**핵심 용어**: ScriptedImporter(특정 확장자 파일을 에셋으로 변환하는 커스텀 임포터), 데이터 테이블(행=레코드, 열=필드로 구성한 기획 데이터), 코드 생성(테이블 스키마에서 타입·접근 코드를 자동 생성).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Scripted Importers | https://docs.unity3d.com/Manual/ScriptedImporters.html |
| AssetPostprocessor | https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html |
| JsonUtility | https://docs.unity3d.com/ScriptReference/JsonUtility.html |
| Google Sheets API (외부 데이터 연동) | https://developers.google.com/sheets/api/guides/concepts |

**리서치 범위**

- **다룰 내용**: CSV/시트 임포트, 스키마·검증, SO/바이너리 변환, 코드 생성, 기획 왕복 워크플로.
- **다루지 않을 내용**: 데이터 구조 설계 원칙(→ 13), 세이브 직렬화(→ 27), 에디터 툴 일반(→ 28).

**산출물**: 시트/CSV → SO 변환 임포터와 검증, 데이터 변경 대응 노트, 기획 왕복 워크플로 정리.

### 50. Unity Sentis·ML-Agents(온디바이스 AI) 〔AI·머신러닝〕

**목적·목표**

- **목적**: 학습된 신경망을 기기에서 직접 추론(Sentis)하거나 강화학습으로 NPC를 학습(ML-Agents)시켜, 게임 안에 AI를 넣는 기준과 비용을 다룰 수 있게 된다.
- **목표**
  1. Sentis(Inference Engine)로 ONNX 모델을 임포트해 CPU/GPU로 추론을 돌리고 입력·출력을 게임과 연결한다.
  2. ML-Agents로 간단한 에이전트를 학습(보상 설계)시키고 학습된 모델을 런타임에 추론한다.
  3. 모델 크기·추론 비용·기기 편차를 측정하고 "게임에 AI를 넣을 때의 도입 기준"을 정리한다.

**핵심 용어**: Sentis / Inference Engine(`com.unity.ai.inference`, Unity 내 신경망 추론 라이브러리), ONNX(프레임워크 독립 신경망 모델 포맷), ML-Agents(강화·모방 학습으로 에이전트를 훈련하는 툴킷).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Sentis (Inference Engine, Unity Manual) | https://docs.unity3d.com/6000.3/Documentation/Manual/com.unity.ai.inference.html |
| Sentis 패키지 매뉴얼 | https://docs.unity3d.com/Packages/com.unity.ai.inference@2.6/manual/index.html |
| ML-Agents 저장소 | https://github.com/Unity-Technologies/ml-agents |
| ML-Agents 패키지 매뉴얼 | https://docs.unity3d.com/Packages/com.unity.ml-agents@latest |

**리서치 범위**

- **다룰 내용**: Sentis 모델 임포트·추론·입출력 연결, ML-Agents 학습·추론, 모델 크기·추론 비용·기기 편차 측정, 도입 기준.
- **다루지 않을 내용**: 클라우드 LLM API 연동, 학습 인프라(대규모 GPU 클러스터), 게임 전체를 AI로 대체하는 설계.

**산출물**: Sentis 추론 데모와 입출력 연결, ML-Agents 학습·추론 예제, 모델 비용·기기 편차 측정과 도입 기준 노트.

### 51. URP 포스트 프로세싱·2D 라이팅 〔그래픽스·애니메이션·사운드〕

**목적·목표**

- **목적**: Volume 기반 포스트 프로세싱과 URP 2D 라이팅으로 화면 전체 룩을 잡고, 모바일에서의 비용을 다룰 수 있게 된다.
- **목표**
  1. Global/Local Volume로 Bloom·Color Adjustments·Vignette 등 효과를 구성하고 코드로 제어한다.
  2. 커스텀 포스트 프로세스(Volume + Renderer Feature) 1종을 만든다.
  3. URP 2D 렌더러의 Light 2D·노멀맵 라이팅을 적용하고 효과별 모바일 비용을 비교한다.

**핵심 용어**: Volume(영역·전역으로 후처리 효과를 거는 프레임워크), Renderer Feature(URP 렌더 파이프라인에 패스를 추가하는 확장), Light 2D(URP 2D 렌더러의 광원 컴포넌트).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Add post-processing in URP | https://docs.unity3d.com/6000.3/Documentation/Manual/urp/add-post-processing.html |
| Post-processing Volume Overrides reference | https://docs.unity3d.com/6000.0/Documentation/Manual/urp/EffectList.html |
| Custom post-processing with Volume | https://docs.unity3d.com/6000.2/Documentation/Manual/urp/post-processing/custom-post-processing-with-volume.html |
| URP introduction(2D 라이팅은 매뉴얼의 2D 렌더러 섹션) | https://docs.unity3d.com/6000.4/Documentation/Manual/urp/urp-introduction.html |

**리서치 범위**

- **다룰 내용**: Volume 효과 구성·코드 제어, 커스텀 포스트 프로세스, URP 2D 라이팅, 효과별 모바일 비용.
- **다루지 않을 내용**: 머티리얼 효과 셰이더(→ 14, → 19), 배칭·드로우콜(→ 29).

**산출물**: Volume 후처리 구성과 코드 제어 샘플, 커스텀 포스트 프로세스 1종, 2D 라이팅 적용 씬과 효과별 비용 비교.

### 52. TextMeshPro와 CJK 폰트 관리 〔UI·입력·연출〕

**목적·목표**

- **목적**: SDF 기반 TextMeshPro의 폰트 에셋·아틀라스·동적 폰트를 이해하고, 한중일(CJK) 글리프의 용량·메모리 문제를 다룰 수 있게 된다.
- **목표**
  1. Font Asset Creator로 SDF 폰트 에셋·아틀라스·머티리얼을 만들고 외곽선·그림자 효과를 적용한다.
  2. Static/Dynamic 폰트 에셋 차이를 이해하고, CJK처럼 글리프가 많은 폰트를 동적 SDF로 관리한다.
  3. 폴백 폰트 체인과 아틀라스 해상도·패딩이 용량·메모리·선명도에 주는 영향을 측정한다.

**핵심 용어**: SDF(Signed Distance Field, 확대·효과에 강한 폰트 렌더링 방식), 폰트 아틀라스(글리프를 모은 텍스처), Dynamic 폰트 에셋(필요한 글리프를 런타임에 아틀라스에 추가).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| TextMeshPro Font Assets | https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/FontAssets.html |
| Font Asset Creator | https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/FontAssetsCreator.html |
| About SDF fonts | https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/FontAssetsSDF.html |

**리서치 범위**

- **다룰 내용**: SDF 폰트 에셋·아틀라스·효과, Static/Dynamic 폰트, CJK 글리프 관리, 폴백 체인, 용량·메모리·선명도 측정.
- **다루지 않을 내용**: 문자열 현지화 흐름(→ 18), 대량 UI 리스트 성능(→ 03).

**산출물**: SDF 폰트 에셋·효과 샘플, CJK 동적 폰트·폴백 구성, 아틀라스 설정별 용량·메모리·선명도 비교 표.

### 53. 멀티플레이어 인프라(UGS Relay·Lobby·Matchmaker) 〔출시·운영·네트워크〕

**목적·목표**

- **목적**: 전용 서버 없이 Relay로 P2P를 중계하고 Lobby로 방을, Matchmaker로 매칭을 구성하는 실서비스 멀티플레이어 인프라를 다룰 수 있게 된다.
- **목표**
  1. Relay 할당·조인 코드로 NAT 뒤 클라이언트를 연결하고 Netcode 전송과 연동한다.
  2. Lobby로 방 생성·검색·입장과 플레이어 상태 공유를 구현한다.
  3. Matchmaker로 티켓 기반 매칭을 구성하고 Relay·Lobby와 연결한다.

**핵심 용어**: Relay(전용 서버 없이 트래픽을 중계해 P2P 연결을 돕는 서비스), Lobby(방 생성·검색·플레이어 목록 관리), Matchmaker(조건 기반으로 플레이어를 묶어 주는 서비스).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Unity Relay 소개 | https://docs.unity.com/ugs/en-us/manual/relay/manual/introduction |
| Relay vs Lobby | https://docs.unity.com/ugs/en-us/manual/relay/manual/relay-vs-lobby |
| Lobby–Relay 통합 | https://docs.unity.com/ugs/manual/lobby/manual/relay-integration |
| Matchmaker 개요 | https://docs.unity.com/ugs/en-us/manual/matchmaker/manual/matchmaker-overview |

**리서치 범위**

- **다룰 내용**: Relay 할당·연결, Lobby 방·상태 공유, Matchmaker 티켓 매칭, 세 서비스 연동.
- **다루지 않을 내용**: 상태 복제·RPC 기초(→ 33), 전용 서버(DGS) 구축, 안티치트(→ 35).
- **선행**: 33. 멀티플레이어·네트워킹 기초

**산출물**: Relay 연결 + Lobby 방 데모, Matchmaker 매칭 예제, Relay·Lobby·Matchmaker 연동 흐름 노트.

### 54. 이벤트 버스·메시징 아키텍처 〔아키텍처·테스트·툴링〕

**목적·목표**

- **목적**: 이벤트 버스·시그널로 모듈 간 직접 참조를 끊어, 약결합·테스트 가능한 통신 구조를 설계할 수 있게 된다.
- **목표**
  1. C# event·UnityEvent·ScriptableObject 이벤트·메시지 버스 라이브러리의 차이를 비교한다.
  2. UI·게임플레이·사운드가 서로를 직접 참조하지 않고 이벤트로 통신하게 바꾼다.
  3. 구독 누락·중복 구독·파괴 후 호출 같은 함정과 수명 정책을 정리한다.

**핵심 용어**: 이벤트 버스(발행/구독으로 메시지를 중계하는 허브), 약결합(서로의 구체 타입을 모른 채 통신), 발행/구독(Pub/Sub, 보내는 쪽과 받는 쪽을 분리).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| UnityEvents | https://docs.unity3d.com/Manual/UnityEvents.html |
| C# events / delegates | https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/ |
| ScriptableObject architecture eBook(이벤트 채널) | https://unity.com/resources/create-modular-game-architecture-with-scriptable-objects-ebook |
| MessagePipe(Cysharp, 라이브러리) | https://github.com/Cysharp/MessagePipe |

> MessagePipe 등 외부 라이브러리는 Unity 공식 패키지가 아니다. 도입 시 라이선스·버전 고정을 별도로 확인한다.

**리서치 범위**

- **다룰 내용**: 이벤트 방식 비교, 약결합 통신 전환, 구독 수명·함정, 메시지 버스 정책.
- **다루지 않을 내용**: 의존성 주입·MVP(→ 32), PlayMode 이벤트 회귀 테스트(→ 11).
- **연계**: 32. 아키텍처와 의존성 주입(DI)

**산출물**: 이벤트 방식 비교 표, 약결합 통신 적용 전후 샘플, 구독 수명·메시지 버스 정책 노트.

### 55. 게임 플로우·부트스트랩·전역 상태 〔아키텍처·테스트·툴링〕

**목적·목표**

- **목적**: 씬 전환 위의 앱 전역 상태와 기동(부트스트랩) 흐름을 한 곳에서 관리해, 초기화 순서·전역 서비스 수명을 다룰 수 있게 된다.
- **목표**
  1. 스크립트 실행 순서·`RuntimeInitializeOnLoadMethod`·`DontDestroyOnLoad`로 부트스트랩 진입점을 만든다.
  2. Boot → Title → Lobby → InGame을 관리하는 전역 상태 머신과 전역 서비스 등록을 구현한다.
  3. 초기화 순서 의존·중복 초기화·씬 재진입 문제를 재현하고 정책을 정리한다.

**핵심 용어**: 부트스트랩(앱 기동 시 전역 서비스를 초기화하는 진입 흐름), 전역 상태 머신(앱 수준의 화면·모드 전이 관리), DontDestroyOnLoad(씬 전환에도 파괴되지 않는 오브젝트).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Order of execution for event functions | https://docs.unity3d.com/Manual/ExecutionOrder.html |
| RuntimeInitializeOnLoadMethod | https://docs.unity3d.com/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html |
| Object.DontDestroyOnLoad | https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html |
| Script execution order settings | https://docs.unity3d.com/Manual/class-MonoManager.html |

**리서치 범위**

- **다룰 내용**: 부트스트랩 진입점, 전역 상태 머신, 전역 서비스 수명, 초기화 순서·재진입 함정.
- **다루지 않을 내용**: 씬 비동기 로딩 자체(→ 09), DI 컨테이너 생명주기(→ 32).
- **선행**: 09. 씬 로딩과 팝업 수명 · **연계**: 32

**산출물**: 부트스트랩 진입점과 전역 상태 머신 샘플, 전역 서비스 등록·수명 코드, 초기화 순서 정책 노트.

### 56. 네이티브 빌드 파이프라인 커스터마이징 〔아키텍처·테스트·툴링〕

**목적·목표**

- **목적**: Android Gradle·iOS Xcode 빌드를 후처리로 자동 수정해, 플러그인·권한·서명 설정을 빌드마다 재현 가능하게 다룰 수 있게 된다.
- **목표**
  1. `IPostGenerateGradleAndroidProject`·Gradle 템플릿으로 Android 빌드의 의존성·권한·설정을 수정한다.
  2. `IPostprocessBuildWithReport`·`PBXProject`로 iOS Info.plist·Capability·프레임워크를 자동 설정한다.
  3. 네이티브 플러그인(JNI/Objective-C 브리지) 연동의 기본 흐름과 빌드 후처리 위치를 정리한다.

**핵심 용어**: PostProcessBuild(빌드 후 생성물을 코드로 수정하는 후크), Gradle(Android 빌드 시스템), PBXProject(Xcode 프로젝트를 코드로 다루는 API).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Modify the Gradle project files | https://docs.unity3d.com/Manual/android-modify-gradle-project-files-methods.html |
| IPostprocessBuildWithReport | https://docs.unity3d.com/ScriptReference/Build.IPostprocessBuildWithReport.html |
| Build.IPostprocessBuild.OnPostprocessBuild | https://docs.unity3d.com/ScriptReference/Build.IPostprocessBuild.OnPostprocessBuild.html |
| iOS.Xcode.PBXProject | https://docs.unity3d.com/ScriptReference/iOS.Xcode.PBXProject.html |

**리서치 범위**

- **다룰 내용**: Gradle 후처리·템플릿, iOS PostProcessBuild·PBXProject, 네이티브 플러그인 브리지 기본.
- **다루지 않을 내용**: IL2CPP·용량(→ 31), CI 워크플로(→ 20).
- **연계**: 20. Build/Test Automation, 31. IL2CPP·빌드·앱 용량 최적화

**산출물**: Android Gradle·iOS Xcode 후처리 스크립트, 네이티브 플러그인 연동 예제, 빌드 후처리 정리 노트.

### 57. 시간·서버 타임·일일 리셋·오프라인 보상 〔게임플레이·데이터〕

**목적·목표**

- **목적**: 일일/주간 리셋·오프라인 누적 보상처럼 시간에 의존하는 로직을 기기 시계 조작에 견디게 설계할 수 있게 된다.
- **목표**
  1. 로컬 시계와 서버 시간을 비교하고, 서버 시간을 기준으로 리셋·쿨다운을 판정한다.
  2. 일일/주간 리셋(타임존·자정 경계)과 오프라인 경과 시간 기반 누적 보상을 구현한다.
  3. 기기 시계 앞당김·타임존 변경 등 치트 시나리오를 재현하고 방어 원칙을 정리한다.

**핵심 용어**: 서버 권위 시간(판정 기준을 서버 시각으로 두는 것), 리셋 경계(자정·주 시작 같은 기준 시점), 오프라인 보상(앱을 끈 동안의 경과 시간으로 주는 보상).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| DateTimeOffset (C#) | https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset |
| UnityEngine.Time | https://docs.unity3d.com/ScriptReference/Time.html |
| UnityWebRequest(서버 시간 수신) | https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.html |

**리서치 범위**

- **다룰 내용**: 로컬 vs 서버 시간, 리셋·쿨다운 판정, 타임존·자정 경계, 오프라인 누적 보상, 시계 조작 방어.
- **다루지 않을 내용**: 데이터 저장·직렬화(→ 27), 안티치트 일반(→ 35), 백엔드 연동 기반(→ 41).
- **연계**: 34. 라이브옵스, 35. 보안·안티치트·데이터 보호, 41. 서버·백엔드 연동(BaaS)

**산출물**: 서버 시간 기준 리셋·쿨다운 샘플, 오프라인 보상 계산 코드, 시계 조작 시나리오 재현과 방어 정책 노트.

### 58. 콜드 스타트·앱 기동 시간 최적화 〔성능·메모리〕

**목적·목표**

- **목적**: 앱 실행부터 첫 인터랙션까지의 콜드 스타트 시간을 분해·측정하고 단축할 수 있게 된다.
- **목표**
  1. 스플래시 → 부트 → 첫 씬 도달까지 구간을 나눠 시간을 측정한다.
  2. 초기 로딩 에셋·동기 초기화·리플렉션 비용을 줄이고 지연 로딩으로 미룬다.
  3. 코드 스트리핑·초기 씬 경량화·프리로드 전략으로 기동 시간을 비교·단축한다.

**핵심 용어**: 콜드 스타트(프로세스가 새로 뜨는 첫 실행), 기동 시간(첫 인터랙션 가능 시점까지의 시간), 지연 로딩(필요해질 때까지 초기화를 미루기).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Splash Screen 설정(Player Settings) | https://docs.unity3d.com/Manual/class-PlayerSettingsSplashScreen.html |
| Collect performance data on a target platform | https://docs.unity3d.com/6000.3/Documentation/Manual/profiling-target-device.html |
| Managed code stripping(기동 비용) | https://docs.unity3d.com/Manual/ManagedCodeStripping.html |

**리서치 범위**

- **다룰 내용**: 기동 구간 분해·측정, 초기 로딩·동기 초기화 절감, 지연 로딩·프리로드, 기동 시간 비교.
- **다루지 않을 내용**: 씬 전환 흐름 자체(→ 09), 빌드 용량(→ 31), 런타임 GC(→ 01).
- **연계**: 09. 씬 로딩과 팝업 수명, 31. IL2CPP·빌드·앱 용량 최적화

**산출물**: 기동 구간별 측정 노트, 지연 로딩·프리로드 적용 전후 비교, 콜드 스타트 단축 체크리스트.

### 59. 접근성(Accessibility) 〔UI·입력·연출〕

**목적·목표**

- **목적**: 스크린 리더·시스템 접근성 설정에 대응해, 더 많은 사용자가 쓸 수 있는 UI를 만들 수 있게 된다.
- **목표**
  1. Unity Accessibility 모듈의 Assistive Support API로 UI 요소에 스크린 리더 레이블·역할을 부여한다.
  2. 시스템 폰트 스케일·고대비·색맹 대응(색 외 단서)을 반영한다.
  3. 접근성 계층(Hierarchy)을 점검하고 기본 준수 체크리스트를 만든다.

**핵심 용어**: 스크린 리더(화면 내용을 음성으로 읽어 주는 보조 기술), 접근성 계층(스크린 리더가 읽는 UI 요소 트리), 색 외 단서(색맹 사용자를 위해 색 말고 형태·아이콘으로도 구분).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Accessibility for Unity applications | https://docs.unity3d.com/6000.3/Documentation/Manual/accessibility.html |
| Accessibility for mobile applications | https://docs.unity3d.com/6000.1/Documentation/Manual/mobile-accessibility.html |
| UnityEngine.AccessibilityModule (API) | https://docs.unity3d.com/6000.0/Documentation/ScriptReference/UnityEngine.AccessibilityModule.html |
| WCAG (W3C 접근성 지침) | https://www.w3.org/WAI/standards-guidelines/wcag/ |

**리서치 범위**

- **다룰 내용**: 스크린 리더 레이블·역할, 폰트 스케일·고대비·색맹 대응, 접근성 계층 점검, 준수 체크리스트.
- **다루지 않을 내용**: 현지화(→ 18), UI 성능(→ 03), TMP 폰트 관리(→ 52).

**산출물**: 스크린 리더 대응 UI 샘플, 폰트 스케일·색 대비 적용 예제, 접근성 점검 체크리스트.

### 60. UA·어트리뷰션·딥링크·인앱 리뷰 〔출시·운영·네트워크〕

**목적·목표**

- **목적**: 설치 출처 추적(어트리뷰션)·딥링크·인앱 리뷰 유도 등 사용자 확보(UA)·마케팅 연동의 기본을 다룰 수 있게 된다.
- **목표**
  1. AppsFlyer/Adjust 등 어트리뷰션 SDK로 설치·캠페인 출처를 추적한다.
  2. 디퍼드 딥링크로 광고 클릭 → 설치 → 특정 화면 진입을 연결한다.
  3. 적절한 시점의 인앱 리뷰 유도(Google Play / App Store)를 구현하고 정책을 정리한다.

**핵심 용어**: 어트리뷰션(설치를 어떤 광고·캠페인에 귀속시킬지 추적), 디퍼드 딥링크(설치 후 첫 실행에서 목적 화면으로 보내는 딥링크), 인앱 리뷰(앱을 벗어나지 않고 평점을 남기게 하는 흐름).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Deep linking (Unity Manual) | https://docs.unity3d.com/Manual/deep-linking.html |
| AppsFlyer Unity SDK | https://dev.appsflyer.com/hc/docs/unity-plugin |
| Adjust Unity SDK | https://dev.adjust.com/en/sdk/unity/ |
| Google Play In-App Review / Apple SKStoreReview | https://developer.android.com/guide/playcore/in-app-review |

**리서치 범위**

- **다룰 내용**: 어트리뷰션 SDK 연동, 디퍼드 딥링크, 인앱 리뷰 유도·시점 정책.
- **다루지 않을 내용**: 앱 내부 딥링크 진입 처리(→ 47), 스토어 심사·ASO(→ 44), 분석 이벤트(→ 34).
- **연계**: 44. 스토어 출시와 사업·법무, 45. 라이브 운영 디자인과 데이터 의사결정

**산출물**: 어트리뷰션 SDK 연동 데모, 디퍼드 딥링크 진입 예제, 인앱 리뷰 유도와 마케팅 연동 정책 노트.

### 61. 2D Tilemap·Sprite·절차적 생성 〔그래픽스·애니메이션·사운드〕

**목적·목표**

- **목적**: Tilemap·Sprite Shape로 2D 레벨을 구성하고, 간단한 절차적 생성으로 콘텐츠를 코드로 만들 수 있게 된다.
- **목표**
  1. Tile Palette·Tilemap·Tilemap Collider 2D로 타일 기반 레벨과 충돌을 구성한다.
  2. Sprite Shape로 곡선·지형 같은 가변 형태를 만든다.
  3. 룰 타일·시드 기반 절차적 생성으로 맵을 코드로 만들고 재현성·밸런스를 점검한다.

**핵심 용어**: Tilemap(타일을 격자에 배치하는 2D 시스템), Sprite Shape(스플라인을 따라 스프라이트를 채우는 가변 형태 도구), 절차적 생성(규칙·시드로 콘텐츠를 알고리즘으로 만들기).

**선행 학습 자료**

| 자료 | 출처 |
|---|---|
| Tilemaps in Unity | https://docs.unity3d.com/6000.2/Documentation/Manual/tilemaps/tilemaps-landing.html |
| 2D Tilemap Editor 패키지 | https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.2d.tilemap.html |
| 2D Sprite Shape 패키지 | https://docs.unity3d.com/Packages/com.unity.2d.spriteshape@latest |
| Tilemap Collider 2D | https://docs.unity3d.com/2022.3/Documentation/Manual/class-TilemapCollider2D.html |

**리서치 범위**

- **다룰 내용**: Tile Palette·Tilemap·충돌, Sprite Shape 지형, 룰 타일·시드 기반 절차적 생성.
- **다루지 않을 내용**: Spine 2D 애니메이션(→ 17), 셰이더·VFX(→ 14, → 21), 물리 일반(→ 26).

**산출물**: 타일 기반 레벨·충돌 샘플, Sprite Shape 지형 예제, 시드 기반 절차적 맵 생성과 재현성 노트.
