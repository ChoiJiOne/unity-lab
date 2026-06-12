---
title: Unity 리서칭 주제 인덱스
type: note
status: draft
tags: [unity, research, index]
---

# Unity 리서칭 주제 인덱스

Unity 클라이언트 개발 역량을 주제별로 정리한 리서칭 인덱스다. 각 주제를 한 줄로 요약하고 분류로 묶었다. 주제별 상세(목적·목표, 핵심 용어, 선행 학습 자료, 리서치 범위, 산출물)는 [unity-research-topics.md](unity-research-topics.md)에 있다.

번호는 작성 순서일 뿐 학습 순서가 아니다. 아래 분류로 골라 보면 된다. 총 62개 주제(01~45 + 추가 46~61).

## 성능·메모리

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 01 | C# 런타임 비용과 GC Allocation | `Update()` 속 할당·박싱·LINQ 비용을 Profiler로 측정하고 줄이는 코드 규칙 정리 |
| 02 | Profiler 기본과 Object Pooling | Profiler 읽는 법과 표준 `ObjectPool`로 데미지 텍스트 풀링 실습 |
| 05 | Memory Profiler와 에셋 수명 | GameObject 파괴 ≠ 에셋 해제임을 스냅샷 비교로 확인하고 참조·해제 다루기 |
| 29 | 렌더링 배칭과 드로우콜 최적화 | SRP Batcher·GPU 인스턴싱·정적 배칭으로 드로우콜을 줄이는 모바일 GPU 최적화 |
| 30 | C# Job System·Burst·DOTS 기초 | 무거운 연산을 멀티스레드·SIMD로 가속하고 DOTS/ECS 도입 기준 판단 |
| 31 | IL2CPP·빌드·앱 용량 최적화 | Mono vs IL2CPP, 코드 스트리핑·텍스처 압축으로 앱 용량·성능 다루기 |
| 46 | Adaptive Performance·프레임 페이싱·발열/배터리 | 온도·전력 상태에 따라 품질을 동적으로 낮춰 쓰로틀링·배터리 대응 |
| 58 | 콜드 스타트·앱 기동 시간 최적화 | 실행~첫 인터랙션 구간을 분해·측정하고 지연 로딩으로 단축 |

## UI·입력·연출

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 03 | 대량 UI 리스트 성능 | 셀 1,000개 리스트의 끊김을 셀 풀링·Canvas 분리로 개선 |
| 04 | Input System 기본 | 입력을 액션 단위로 추상화하고 UI/게임플레이 입력 충돌 제어 |
| 12 | DOTween과 UI Animation | 트윈 수명·취소 관점에서 팝업 연출과 파괴 후 콜백 안전 처리 |
| 15 | Timeline과 Playables | 스킬 컷신·튜토리얼을 Timeline으로 만들고 Signal로 게임 코드 연결 |
| 16 | Cinemachine과 Camera | 카메라 follow·shake·blend·컷신 전환을 코드·Timeline과 연동 |
| 24 | UI Toolkit 런타임 UI | UXML·USS로 런타임 UI를 만들고 uGUI와의 선택 기준 비교 |
| 52 | TextMeshPro와 CJK 폰트 관리 | SDF 폰트 에셋·아틀라스·동적 폰트와 한중일 글리프 용량·메모리 관리 |
| 59 | 접근성(Accessibility) | 스크린 리더·폰트 스케일·색맹 대응으로 더 많은 사용자가 쓰는 UI |

## 그래픽스·애니메이션·사운드

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 10 | Animator와 State Machine | 캐릭터 상태 전이·Animation Event를 코드 FSM과 분리해 다루기 |
| 14 | Shader Graph와 URP 기초 | 노드 셰이더로 Hit Flash·Dissolve·Outline 같은 실무 효과 제작 |
| 17 | Spine Runtime | Spine 2D 애니메이션의 재생·전환·스킨 교체와 UI/월드 비용 비교 |
| 19 | ShaderLab / HLSL 기초 | 최소한의 HLSL/ShaderLab 구조를 읽고 수정하며 노드 구현과 비교 |
| 21 | Particle System / VFX와 이펙트 풀링 | 전투 이펙트의 생성·파괴·풀링·sorting·overdraw 다루기 |
| 25 | 오디오 시스템과 AudioMixer | BGM/SFX/Voice를 AudioMixer로 분리하고 보이스 한도·로드 타입 관리 |
| 51 | URP 포스트 프로세싱·2D 라이팅 | Volume 기반 후처리(Bloom·컬러)와 URP 2D 라이팅으로 화면 룩 완성 |
| 61 | 2D Tilemap·Sprite·절차적 생성 | Tilemap·Sprite Shape로 2D 레벨 구성과 시드 기반 절차적 맵 생성 |

## 게임플레이·데이터

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 13 | 인벤토리 설계 변경과 ScriptableObject | 요구사항 변경으로 구조가 무너지는 지점을 겪고 설정/상태 분리 설계 |
| 18 | Localization 기본 | 텍스트·폰트·Smart String 기반 현지화 흐름과 키 네이밍·폴백 정책 |
| 26 | 물리(Physics)와 충돌 | Rigidbody·Collider·충돌 매트릭스·NonAlloc 레이캐스트로 물리 비용 다루기 |
| 27 | 세이브 시스템과 직렬화 | JSON/바이너리/PlayerPrefs 비교, 버전 마이그레이션·원자적 쓰기 |
| 38 | NavMesh와 AI 내비게이션 | NavMesh 길 찾기·회피, 런타임 베이크·동적 장애물·경로 비용 |

## 비동기·리소스 수명

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 06 | Async/Await, Cancellation, UniTask | 비동기 기본과 CancellationToken 취소 정책을 작은 샘플로 정리 |
| 07 | Addressables Load/Release 수명 관리 | 참조 카운트·핸들 소유권으로 load/release 균형 맞추기 |
| 09 | 씬 로딩과 팝업 수명 | `LoadSceneAsync`·Additive·로딩 UI·취소를 하나의 전환 흐름으로 안전하게 연결 |

## 아키텍처·테스트·툴링

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 00 | Lab 템플릿과 AI 코드 리뷰 기준 | 재사용 Lab 폴더 구조(asmdef)와 AI 코드 PR 리뷰 체크리스트 만들기 |
| 08 | EditMode Test와 순수 C# 로직 분리 | 도메인 로직을 MonoBehaviour에서 분리해 EditMode 테스트 작성 |
| 11 | PlayMode 회귀 테스트 | 고친 버그를 재발 방지 테스트로 남기고 런타임 전용 문제 검증 |
| 20 | Build/Test Automation | `batchmode` 명령줄 테스트와 GitHub Actions CI 초안 구성 |
| 28 | 에디터 확장과 커스텀 툴 | CustomEditor·EditorWindow·검증 스크립트로 반복 작업 줄이기 |
| 32 | 아키텍처와 의존성 주입(DI) | DI 컨테이너·MVP/MVVM로 결합도를 낮춘 교체·테스트 쉬운 구조 설계 |

## 출시·운영·네트워크

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 22 | 최종 문서 정리와 공개 글 선정 | Lab 중 커리어 증거로 강하고 공개해도 안전한 문서 선별·정리 |
| 23 | Diagnostics / QA / Logging | 디버그 오버레이·행동 로깅·리포트 템플릿으로 QA 재현성 확보 |
| 33 | 멀티플레이어·네트워킹 기초 | 상태 복제·RPC·소유권과 지연·끊김 대응 기초 |
| 34 | 라이브옵스(Remote Config·Analytics·IAP) | 원격 설정·지표 수집·인앱 결제의 안전한 클라이언트 연동 |
| 35 | 보안·안티치트·데이터 보호 | 데이터 변조·메모리 조작·통신 가로채기 위협과 현실적 방어선 설계 |
| 39 | 라이브 서비스 콘텐츠 패치 | 스토어 재배포 없이 Addressables 원격 카탈로그로 콘텐츠 갱신·롤백 |
| 40 | 라이브 에러 수집과 대응 | 크래시·예외 자동 수집과 IL2CPP 심볼화·트리아지 운영 흐름 |

## 스크립팅·핫업데이트

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 36 | xLua 스크립팅과 핫픽스 | xLua로 C# 로직 일부를 Lua로 교체해 재심사 없이 런타임 버그 핫픽스 |
| 37 | PuerTS(TypeScript) 스크립팅 | PuerTS로 로직을 TypeScript로 작성하고 xLua와 타입·성능·용량 비교 |

## 1인 개발·사업·운영

> 클라이언트 엔지니어링 밖에서 "혼자 만들고 서비스를 지속"하는 데 필요한 축. 향후 필요 시 꺼내 리서칭한다.

| # | 주제 | 한 줄 요약 |
|---|---|---|
| 41 | 서버·백엔드 연동(BaaS) | 관리형 백엔드로 인증·세이브·랭킹·재화를 연동하고 서버 권위 경계 설계 |
| 42 | 게임 디자인 기초 | 코어 루프·진행·밸런싱을 정의하고 프로토타입으로 재미 검증 |
| 43 | 콘텐츠·아트 파이프라인 | 에셋 임포트 규격·아틀라스·외주/라이선스 워크플로 표준화 |
| 44 | 스토어 출시와 사업·법무 | 심사·ASO·개인정보/연령등급·결제/세금 등 비기술 출시 체크리스트 |
| 45 | 라이브 운영 디자인과 데이터 의사결정 | 리텐션·수익화 지표와 코호트·A/B 기반 운영 의사결정 루프 |

## 비고

- 24~35번은 Unity 6(2025~2026) 최신 스택과 시니어 모바일 클라이언트 직무 요건을 반영해 추가한 주제다(UI Toolkit, 오디오, 물리, 세이브, 에디터 확장, 렌더링 배칭, Job System·DOTS, IL2CPP·빌드, 아키텍처·DI, 네트워킹, 라이브옵스, 보안).
- 36~40번은 실서비스 운영에 필요한 스크립팅·핫업데이트(xLua·PuerTS), AI 내비게이션(NavMesh), 라이브 콘텐츠 패치·에러 대응을 추가한 주제다.
- 41~45번은 클라이언트 엔지니어링 밖의 축(서버/백엔드, 게임 디자인, 콘텐츠·아트, 출시·법무, 데이터 기반 운영)이다. "기술 좋은 클라이언트 개발자"를 넘어 "1인 개발·운영"으로 가는 데 필요하다.
