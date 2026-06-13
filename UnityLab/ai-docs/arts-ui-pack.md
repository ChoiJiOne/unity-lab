# Kenney UI Pack 에셋 가이드

범용 게임 UI 에셋 팩(v2)입니다.
버튼·패널·슬라이더·체크박스·화살표에 폰트·사운드까지 포함한 종합 UI 키트입니다.

- **경로:** `Assets/Arts/kenney_ui-pack/`
- **제작자:** Kenney (www.kenney.nl) · UI Pack v2.0 (2024-06-12)
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

깔끔한 플랫 톤의 **종합 UI 키트**입니다. (이 컬렉션에서 가장 범용적인 UI 팩)
5색 + Extra × (Default/Double) PNG 870개 + SVG 434개 + 폰트 + 사운드로 구성됩니다.

| 구분 | 내용 |
|------|------|
| **색상 5종** | Blue / Green / Grey / Red / Yellow — 각 82개 요소 |
| **Extra** | 색 무관 공통 요소 24개 |
| **요소** | 버튼(rectangle/round, border/flat/gloss/depth), 9-슬라이스 패널, 슬라이더, 체크박스/라디오, 화살표(basic/decorative, 4방향 + small), 별, 탭 등 |
| **Default / Double** | 1배 / 2배 해상도 |
| **Font** | TTF 2종 (UI용) |
| **Sounds** | OGG 6개 (클릭/롤오버 등 UI 효과음) |
| **Vector** | 색상별 SVG |

> **5색 × 다양한 버튼 스타일 + 폰트 + 효과음**까지 갖춘 가장 완결적인 UI 팩. 버튼은 `depth`(입체) 변형으로 눌림 상태 표현. 패널은 9-슬라이스.

---

## 2. 언제 사용하면 되는가?

- **범용 게임 UI** — 메뉴, 설정, HUD, 다이얼로그, 인벤토리
- 어떤 장르든 **기본 UI 키트**로 우선 채택 (가장 무난)
- 색상으로 상태/팀/난이도 구분
- 폰트·효과음까지 한 번에 (UI 사운드 피드백 포함)

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **폰트(TTF) + 사운드(OGG) 포함** — 그래픽 외 요소까지 완비된 종합 키트.
- **버튼 `depth` 변형** — 입체 버튼으로 일반/눌림(pressed) 상태 자연스럽게 표현.
- **패널은 9-슬라이스** — Sprite Editor에서 Border 지정 후 Sliced.
- **파일명이 의미 기반**(`button_rectangle_depth_gloss`, `arrow_basic_n`) → 식별 매우 쉬움.
- **Default / Double 2해상도** — 고DPI는 Double.
- 픽셀 톤이 필요하면 [arts-pixel-ui-pack.md](arts-pixel-ui-pack.md), RPG 톤은 [arts-ui-pack-rpg-expansion.md](arts-ui-pack-rpg-expansion.md).

---

## 형태 선택 가이드

- **버튼:** `PNG/<색>/Default/button_*`(depth로 눌림 표현)
- **창/패널:** 9-슬라이스 패널 → Border 지정 후 Sliced
- **슬라이더/체크박스/화살표:** 해당 요소 PNG
- **폰트/효과음:** `Font/`, `Sounds/`
- **고해상도:** `Double/`
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
