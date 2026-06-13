# Kenney Puzzle Pack (1) 에셋 가이드

매치-3 / 보석 퍼즐 게임용 2D 에셋 팩입니다.
색상·형태별 젬(보석)과 버튼·패널로 Bejeweled류 퍼즐을 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_puzzle-pack-1/`
- **제작자:** Kenney (www.kenney.nl) · Puzzle Pack v1.1
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

매치-3 퍼즐용 **젬(보석) + UI 요소 세트**입니다. 플랫/벡터 스타일.
PNG 172개(Default + Double 2배 해상도) + SVG/SWF 벡터로 구성됩니다.

| 카테고리 | 내용 |
|----------|------|
| **Elements (젬)** | 6색(purple/red/yellow/green/blue/grey) × 형태(diamond/polygon/square/rectangle) × glossy 변형 |
| **Buttons** | 9-슬라이스 버튼(Default/Selected) — `_top/_mid/_left/_topright` 등 분할 타일 |
| **Balls / 기타** | ballBlue, ballGrey 등 보조 오브젝트, 별·커서·격자 |
| **Cursors** | 화살표/손가락 커서 변형 |
| **Default / Double** | 동일 세트의 1배 / 2배 해상도 |

> 젬이 **6색 × 다중 형태**로 제공 → 매치-3 보드의 타일 종류로 그대로 사용. `_glossy`로 강조/특수 타일 표현.

---

## 2. 언제 사용하면 되는가?

- **매치-3 퍼즐** — Bejeweled, Candy Crush류
- **색 맞추기 / 보석 수집** 캐주얼 퍼즐
- 퍼즐 게임 UI — 9-슬라이스 버튼, 패널, 격자
- 보드 기반 타일 매칭 프로토타입

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **Default / Double 2단계 해상도** — 고DPI/큰 화면은 `Double` 사용.
- **버튼이 9-슬라이스 분할 타일**(`buttonDefault_mid`, `_topleft` 등) → 임의 크기 버튼 조합 가능.
- **파일명이 의미 기반**(`element_blue_diamond_glossy`) → 색/형태 단위 로딩 쉬움.
- **플랫 벡터 스타일** — 픽셀아트 아님.
- 참고: 비슷한 puzzle-pack-2는 **벽돌깨기/핀볼 계열**(공·패들·파이프)로 성격이 다름 → [arts-puzzle-pack-2.md](arts-puzzle-pack-2.md) 참조.

---

## 형태 선택 가이드

- **매치-3 타일:** `PNG/Default/element_*` (고해상도는 `Double/`)
- **버튼/패널:** `PNG/Default/button*` 분할 타일
- **시트 일괄 임포트:** `Spritesheet/` + XML
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
