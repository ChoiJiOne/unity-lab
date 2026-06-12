# Spine 샘플 에셋 가이드

Spine 런타임(`spine-unity`)에 포함된 공식 예제 스켈레톤 목록입니다.
테스트/프로토타이핑 시 참고용으로 사용하세요.

- **경로:** `Assets/Packages/Spine/Spine Examples/Spine Skeletons/`
- **총 개수:** 19개 (`*_SkeletonData.asset` 기준)
- **데이터 형식:** JSON(텍스트) 또는 Binary(`.skel.bytes`)

각 폴더에는 일반적으로 다음 파일 세트가 함께 들어 있습니다.
`*_SkeletonData.asset`(Unity 데이터) · `*.atlas.txt`(아틀라스) · `*.png`(텍스처) · `*_Material.mat`(머티리얼)

---

## 에셋별 특징

| # | 이름 | 폴더 | 형식 | 특징 |
|---|------|------|------|------|
| 1 | **celestial-circus-pro** | `celestial-circus/` | JSON | 화려한 이펙트 위주의 서커스 테마 캐릭터. 다수의 메시/디포머와 블렌딩을 활용한 비교적 무거운 데모. |
| 2 | **cloud-pot** | `cloud-pot/` | Binary | 구름 항아리 오브젝트. 물리(physics constraint) 기반 출렁임 표현 예제. |
| 3 | **dragon** | `Dragon/` | JSON | 다중 텍스처 프레임 기반의 비행 애니메이션. 시퀀스/프레임 애니메이션 데모에 자주 사용. |
| 4 | **eyes** | `Eyes/` | JSON | 눈동자만 있는 초경량 샘플. 본(bone) 추적·IK·시선 제어 테스트에 적합. |
| 5 | **FootSoldier** | `FootSoldier/` | JSON | 보병 캐릭터. 공격/이동 등 기본 게임 캐릭터 애니메이션 세트. |
| 6 | **Gauge** | `Gauge/` | JSON | 게이지/계기판 UI 형태. 본 회전으로 수치를 표현하는 UI 연동 예제. |
| 7 | **goblins** | `Goblins/` | JSON | 스킨(skin) 교체 예제의 대표 샘플. 하나의 스켈레톤에 goblin/goblingirl 두 스킨 포함. |
| 8 | **hero-pro** | `Hero/` | JSON | 횡스크롤 액션용 영웅 캐릭터. 이동/점프/공격 등 게임플레이 애니메이션. |
| 9 | **mix-and-match-pro** | `mix-and-match/` | JSON | 의상/장비 부위별 스킨 조합(equip system) 데모. 런타임 스킨 합성 학습에 핵심. |
| 10 | **Raggedy Spineboy** | `Raggedy Spineboy/` | JSON | 헝겊 인형 버전 스파인보이. 물리/줄 매달림 효과 예제. |
| 11 | **raptor** | `Raptor/` | JSON | 공룡 라이더. 메시 디포메이션과 복잡한 본 계층을 보여주는 대표 데모. |
| 12 | **raptor-pro** (+mask) | `raptor-pro-and-mask/` | JSON | raptor의 Pro 버전 + 마스킹(클리핑) 예제 포함. |
| 13 | **sack-pro** | `sack/` | Binary | 자루 오브젝트. 물리 흔들림과 바이너리 로딩 테스트용 경량 샘플. |
| 14 | **snowglobe-pro** | `snowglobe/` | Binary | 스노우볼. 파티클풍 연출과 물리 표현 예제. |
| 15 | **spineboy-pro** | `spineboy-pro/` | JSON | Spine의 마스코트 캐릭터(Pro). 가장 표준적인 레퍼런스. 걷기/달리기/점프/사격 등 풀 세트. |
| 16 | **spineboy-unity** | `spineboy-unity/` | JSON | spineboy의 Unity 전용 변형. Fill/Grayscale 등 머티리얼 효과 예제 포함. |
| 17 | **Doi** (Spineunitygirl) | `Spineunitygirl/` | JSON | Unity girl 캐릭터. 부드러운 메시 변형 위주의 데모. |
| 18 | **stretchyman** | `Stretchyman/` | JSON | 팔다리가 늘어나는 캐릭터. 본 스케일/스트레치 표현 전용 예제. |
| 19 | **whirlyblendmodes** | `whirlyblendmodes/` | JSON | 다양한 블렌드 모드(additive 등) 시연용 샘플. 렌더링/머티리얼 테스트에 적합. |

---

## 용도별 추천

- **처음 입문 / 기본 레퍼런스:** `spineboy-pro`
- **스킨 교체 학습:** `goblins`, `mix-and-match-pro`
- **물리(physics) 표현:** `cloud-pot`, `sack-pro`, `snowglobe-pro`, `Raggedy Spineboy`
- **메시 디포메이션:** `raptor`, `Doi`, `celestial-circus-pro`
- **본 제어/IK 테스트:** `eyes`, `Gauge`, `stretchyman`
- **렌더링/블렌드 모드:** `whirlyblendmodes`, `spineboy-unity`

> ⚠️ 위 에셋은 Spine 런타임에 동봉된 **예제 에셋**입니다. 실제 프로젝트에 사용할 경우 별도 폴더로 복사해서 쓰는 것을 권장합니다.
