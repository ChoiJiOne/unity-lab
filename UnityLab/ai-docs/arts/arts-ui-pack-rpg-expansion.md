# Kenney UI Pack: RPG Expansion 에셋 가이드

RPG풍 UI 확장 에셋 팩입니다.
체력/마나 바, 패널, 화살표·커서·아이콘으로 RPG 인터페이스를 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_ui-pack-rpg-expansion/`
- **제작자:** Kenney (www.kenney.nl) · UI Pack: RPG Expansion
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

판타지/RPG 톤의 **UI 바 + 패널 + 아이콘 확장 세트**입니다. 플랫/벡터 스타일.
PNG 90개 + SVG/SWF 벡터 + 스프라이트시트로 구성됩니다.

| 카테고리 | 내용 |
|----------|------|
| **Bars (체력/마나/경험치)** | 색상별(Blue/Green/Red/Yellow 등) 진행 바 — 가로/세로, `_left/_mid/_right`(또는 `_top/_mid/_bottom`) 분할로 임의 길이 |
| **barBack** | 바 배경(빈 게이지) — 가로/세로 분할 |
| **Panels** | 9-슬라이스 패널/창 — beige/brown/grey/white 등 RPG 우드/스톤 톤 |
| **Arrows** | 방향 화살표(Beige/Blue/Brown/Silver × 좌/우) |
| **Cursors / Icons** | 손가락 커서, 검 아이콘, 체크/엑스, 라디오·불릿 등 |
| **Spritesheet** | 통합 시트 + XML |

> **바가 분할 타일(left/mid/right)** 로 제공 → mid를 늘려 어떤 길이의 체력바도 구성. 패널은 9-슬라이스.

---

## 2. 언제 사용하면 되는가?

- **RPG UI** — 체력/마나/경험치 바, 인벤토리·대화 패널
- **판타지/어드벤처** 톤 인터페이스
- 진행 게이지(로딩·스태미나)와 9-슬라이스 창
- 기본 UI 팩을 보강하는 **RPG 확장(expansion)** 용도

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **바 = 분할 타일 구조**(`bar<색>_horizontalLeft/Mid/Right`) → mid를 타일링/스케일해 가변 길이 게이지 구현. `barBack`은 빈 배경.
- **패널은 9-슬라이스** — Sprite Editor에서 Border 지정 후 Sliced.
- **파일명이 의미 기반**(`barRed_horizontalMid`, `arrowBlue_left`) → 식별 쉬움.
- **이름이 `expansion`** — 단독 사용 가능하나 기본 UI 팩과 함께 쓰도록 설계된 보강 팩.
- **픽셀 UI 팩과 차이** — 이쪽은 **부드러운 플랫/RPG 톤**, pixel-ui-pack은 픽셀아트. 톤에 맞춰 선택 → [arts-pixel-ui-pack.md](arts-pixel-ui-pack.md).

---

## 형태 선택 가이드

- **체력/마나 바:** `bar<색>_horizontal*`(채움) + `barBack_*`(배경), mid 스케일/타일링
- **창/패널:** 패널 PNG → Border 지정 후 9-슬라이스
- **화살표/커서/아이콘:** 해당 PNG 낱개
- **시트 일괄 임포트:** `Spritesheet/` + XML
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `preview.png`(전체), `sample.png`(배치 예시)
