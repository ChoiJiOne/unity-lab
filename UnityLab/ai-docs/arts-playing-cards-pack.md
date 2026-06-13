# Kenney Playing Cards Pack 에셋 가이드

표준 트럼프 카드 전용 에셋 팩입니다.
포커·솔리테어 등 카드게임의 52장 카드 그래픽으로 사용합니다.

- **경로:** `Assets/Arts/kenney_playing-cards-pack/`
- **제작자:** Kenney (www.kenney.nl) · Playing Cards Pack (2020-12-01)
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

표준 52장 **트럼프 카드 그래픽 세트**입니다. 플랫/벡터 스타일.
3가지 크기(large/medium/small) PNG 276개 + 타일시트 + CSV 매핑으로 구성됩니다.

| 크기(폴더) | 개수 | 내용 |
|------------|------:|------|
| **Cards (large)** | 56 | 표준 52장 + 카드 뒷면/빈 카드 (고해상도) |
| **Cards (medium)** | 136 | 중간 크기 (변형/요소 포함 더 많음) |
| **Cards (small)** | 84 | 소형 (미니 카드, 칩/주사위 포함) |
| **Tilesheet** | 6 | 통합 시트 + CSV 매핑 |

**카드:** 4수트(clubs/diamonds/hearts/spades) × A~K, 카드 뒷면, 빈 카드. 일부 크기엔 미니 칩·주사위 등 보조 요소 포함.

> 보드게임 팩의 카드보다 **트럼프 표준 디자인에 특화**(랭크/수트 명확). large/medium/small 3해상도.

---

## 2. 언제 사용하면 되는가?

- **카드 게임** — 포커, 블랙잭, 솔리테어, 하트, 프리셀 등
- 표준 52장 덱이 필요한 모든 게임
- 카드 크기를 화면/용도에 맞게(large=상세, small=핸드 다수)
- 보드게임 팩과 달리 **카드만 깔끔히** 필요할 때

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **파일명이 의미 기반**(`card_hearts_A`, `card_spades_K`, `card_back`) → 수트/랭크로 직접 로딩, 규칙적 코드 작성 쉬움.
- **3가지 크기(large/medium/small)** — 해상도/용도에 맞춰 선택.
- **CSV 매핑 포함** — 타일시트에서 카드 위치 참조.
- **카드 뒷면·빈 카드** 포함 — 덮인 카드/플레이스홀더 표현.
- **플랫 벡터 스타일** — 픽셀아트 아님.
- 카드 외 말/주사위 그래픽이 더 필요하면 [arts-boardgame-pack.md](arts-boardgame-pack.md) 참조.

---

## 형태 선택 가이드

- **상세 카드 표시:** `PNG/Cards (large)/`
- **핸드에 여러 장:** `PNG/Cards (small)` 또는 `(medium)`
- **수트/랭크 로딩:** `card_<suit>_<rank>` 규칙 사용
- **시트 일괄 임포트:** `Tilesheet/` + CSV
- **참고:** `Preview.png` / `Preview A·B.png`(전체)
