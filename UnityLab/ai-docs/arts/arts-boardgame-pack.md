# Kenney Boardgame Pack 에셋 가이드

보드게임/카드게임용 2D 에셋 팩입니다.
카드·주사위·칩·말(piece)과 효과음까지 포함해 테이블게임을 빠르게 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_boardgame-pack/`
- **제작자:** Kenney (www.kenney.nl) · Boardgame pack **v2**
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

보드게임 구성요소를 모은 **벡터 스타일 2D 에셋 팩**입니다. (플랫 디자인, 픽셀아트 아님)
PNG 539개 + SVG/SWF 벡터 + OGG 효과음 12개로 구성됩니다.

| 카테고리 | 개수 | 내용 |
|----------|------:|------|
| **Cards** | 68 | 표준 트럼프 카드 전체(A~K, 4수트) + 컬러별 카드 뒷면 |
| **Dice** | 24 | 6면 주사위(1~6), 색상/테두리 변형 |
| **Chips** | 32 | 포커 칩(컬러·액면 변형) |
| **Pieces** | 57 × 7색 | 말 — 폰/하우스/타워/보트/자동차/기차/깃발 등 (Black/Blue/Green/Purple/Red/White/Yellow) |
| **Bonus** | 12 (OGG) | cardPlace, cardSlide, chipsCollide, dieShuffle, dieThrow 등 효과음 |
| **Spritesheets** | 14 | 위 요소들의 스프라이트시트 + XML 매핑 |
| **Vector** | SVG/SWF | 무한 확대용 원본 벡터 |

> **말(Pieces)은 7색 동일 세트** 제공 → 멀티플레이어 색상 구분에 그대로 사용.

---

## 2. 언제 사용하면 되는가?

- **카드 게임** — 포커, 블랙잭, 솔리테어, 트릭테이킹 등 (표준 52장 완비)
- **보드게임** — 말·주사위·칩으로 윷놀이/모노폴리류 프로토타입
- **주사위 굴리기 / 확률 게임**
- 보드게임 **UI 토큰**(턴 표시, 점수 칩) 용도
- 효과음까지 있어 카드 배분·주사위 던지기 피드백 즉시 구현

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **파일명이 의미 기반** (`cardBack_blue1`, `dieRed6`, `pieceRed_border03`) → desert-shooter와 달리 **이름만으로 식별 가능**, 스크립트에서 규칙적 로딩 쉬움.
- **벡터 원본 제공**(`Vector/` SVG) → 고해상도/인쇄 품질 필요 시 활용. Unity SVG 사용은 `com.unity.vectorgraphics` 패키지 필요.
- **테두리 변형(`_border`)** — 외곽선 있는/없는 버전이 쌍으로 제공됨.
- **플랫 벡터 스타일** — 픽셀아트가 아니므로 임포트 시 `Filter Mode: Bilinear` 등 일반 설정 권장(Point 불필요).
- **Spritesheets + XML** — 시트로 한 번에 임포트하거나, `PNG/`의 낱개 파일을 직접 사용.

---

## 형태 선택 가이드

- **낱개 오브젝트(카드/말 개별 제어):** `PNG/` 낱개 파일
- **시트 일괄 임포트:** `Spritesheets/` + XML
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `preview.png`(전체 카탈로그), `sample.png`(배치 예시)
