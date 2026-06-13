# Kenney Input Prompts 에셋 가이드

게임 입력(컨트롤러/키보드) 안내용 아이콘 팩입니다.
조작 안내 UI·튜토리얼·키 리바인딩 화면 등을 빠르게 구성할 때 참조하세요.

- **경로:** `Assets/Arts/kenney_input-prompts_1.5/`
- **제작자:** Kenney (www.kenney.nl) · 버전 1.5
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

키보드·마우스 키, 게임패드 버튼, 트리거, 방향키 등을 표시하는 **입력 프롬프트(UI 스프라이트) 모음**입니다.
플랫폼별로 폴더가 나뉘어 있고, 각 플랫폼마다 4가지 형식 변형을 제공합니다.

### 지원 플랫폼(폴더)

| 분류 | 폴더 |
|------|------|
| PC 입력 | `Keyboard & Mouse`, `Generic` |
| Xbox | `Xbox Series` |
| PlayStation | `PlayStation Series` |
| Nintendo | `Nintendo Switch`, `Nintendo Switch 2`, `Nintendo Wii`, `Nintendo WiiU`, `Nintendo Gamecube` |
| Steam / Valve | `Steam Controller`, `Steam Deck`, `Steam Frame`, `Valve Index` |
| VR / 모바일 / 기타 | `Meta Quest`, `Touch`, `Playdate` |
| 효과/강조 | `Flairs` (화살표·하이라이트 오버레이) |

### 형식 변형 (각 플랫폼 하위)

| 변형 폴더 | 형식 | 용도 |
|-----------|------|------|
| `Default` | PNG | 기본 비트맵 아이콘 |
| `Double` | PNG (2배 해상도) | 고해상도/대형 UI |
| `Vector` | SVG | 무한 확대, 해상도 독립 |
| `Fonts` | OTF/TTF + `*_map.txt` | 텍스트에 글리프로 인라인 삽입 |

> 아이콘 수: 플랫폼당 약 100~134개 (예: Keyboard & Mouse 106, Xbox 99, PlayStation 134).

---

## 2. 언제 사용하면 되는가?

- **조작 안내 UI** — "점프: [Space]", "공격: [X]" 같은 키 가이드 표시
- **튜토리얼 / 키 리바인딩 화면**
- **플랫폼 자동 전환** — 패드 연결 시 콘솔 버튼, 키보드 입력 시 키 아이콘으로 스왑
- **프로토타입** — 직접 그릴 필요 없이 즉시 입력 안내 부착

---

## 3. 특이 사항

- **CC0 라이선스** — 상용 배포까지 안전. 크레딧은 선택.
- **메타 파일 미존재 PNG 주의** — Unity 임포트 시 `Sprite (2D and UI)` 설정을 직접 잡아줘야 할 수 있음.
- **폴더명에 공백·`&` 포함**(`Keyboard & Mouse`) — 스크립트에서 경로 처리 시 따옴표/이스케이프 주의.
- **텍스트 인라인이 필요하면 `Fonts` 변형 사용** — TextMeshPro의 Sprite Asset 또는 폰트 글리프로 본문 중간에 버튼 아이콘 삽입 가능. `*_map.txt`에 글리프 매핑 표 있음.
- **벡터가 필요하면 `Vector`(SVG)** — Unity에서 SVG 사용 시 `com.unity.vectorgraphics` 패키지 필요.
- 루트의 `Overview.html`로 전체 카탈로그 확인, `Best practices.url`에 활용 가이드 링크.

---

## 형식 선택 가이드

- **일반 UI 아이콘:** `Default`
- **고해상도/큰 화면:** `Double`
- **확대·축소가 잦은 UI:** `Vector` (SVG)
- **문장 속 버튼 표기:** `Fonts` (TMP 연동)
