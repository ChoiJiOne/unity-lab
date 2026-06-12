# Kenney Pixel UI Pack 에셋 가이드

픽셀아트 게임용 UI 에셋 팩입니다.
9-슬라이스 패널·버튼과 아이콘·화살표로 픽셀 스타일 인터페이스를 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_pixel-ui-pack/`
- **제작자:** Kenney (www.kenney.nl), with Lynn Evers · Pixel UI Pack
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

픽셀아트 UI용 **9-슬라이스 패널/버튼 + 아이콘 시트**입니다.
PNG 36개 + 스프라이트시트로 구성됩니다.

| 카테고리 | 개수 | 내용 |
|----------|------:|------|
| **9-Slice / Ancient** | 12 | 고전풍 패널 (brown/grey/tan/white × 기본/inlay/pressed) |
| **9-Slice / Colored** | 10 | 컬러 패널 (blue/green/grey/red/yellow × 기본/pressed) |
| **9-Slice / Outline** | 8 | 외곽선 강조 패널 (색상 × 기본/pressed) |
| **Spritesheet** | 2 | 아이콘·화살표·체크박스 등 UI 요소 통합 시트 + 매핑 |

> 패널이 **9-슬라이스 전용**으로 설계 → 어떤 크기로 늘려도 모서리 깨지지 않음. `pressed` 변형으로 버튼 눌림 상태 표현.

---

## 2. 언제 사용하면 되는가?

- **픽셀아트 게임의 UI** — 메뉴, 다이얼로그, 인벤토리 패널
- **버튼/창** — 기본·눌림(pressed) 상태 토글
- 방향 화살표, 체크박스, 아이콘이 필요한 HUD
- 레트로/8비트 톤 게임 인터페이스

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **9-슬라이스(9-Slice) 임포트 필수** — Unity Sprite Editor에서 Border를 지정해 `Image > Sliced` 또는 `Tiled`로 사용해야 패널이 자연스럽게 늘어남.
- **파일명이 의미 기반**(`blue.png`, `blue_pressed.png`, `brown_inlay.png`) → 상태별 스왑 쉬움.
- **3가지 테마**(Ancient/Colored/Outline) — 게임 톤에 맞춰 선택.
- **픽셀아트** — 임포트 시 `Filter Mode: Point (no filter)`, `Compression: None` 권장.

---

## 형태 선택 가이드

- **창/패널/버튼:** `9-Slice/<테마>/` → Border 지정 후 Sliced
- **아이콘/화살표/체크박스:** `Spritesheet/` → Slice
- **참고:** `Preview.png`(전체), `9-Slice/list.png`(패널 목록)
