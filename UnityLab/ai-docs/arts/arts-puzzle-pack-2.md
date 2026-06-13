# Kenney Puzzle Pack II 에셋 가이드

벽돌깨기/핀볼·파이프 퍼즐 게임용 2D 에셋 팩입니다.
공·패들·벽돌 타일·파이프로 Breakout류 및 배관 퍼즐을 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_puzzle-pack-2/`
- **제작자:** Kenney (www.kenney.nl) · Puzzle Pack II
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

벽돌깨기·핀볼·파이프 퍼즐용 **공/패들/타일/파이프 세트**입니다. 플랫/벡터 스타일.
PNG 814개 + SVG/SWF 벡터 + 스프라이트시트(카테고리별)로 구성됩니다.

| 카테고리 | 개수 | 내용 |
|----------|------:|------|
| **Tiles (8색)** | 각 72 | 벽돌/블록 타일 — black/blue/green/grey/orange/pink/red/yellow, 형태·아이콘 변형 |
| **Balls (4색)** | 각 10 | 공 — Black/Blue/Grey/Yellow |
| **Paddles** | 12 | 패들(바) — 색상/길이 변형 |
| **Pipes (Green/Grey)** | 각 44 | 파이프 — 직선/코너/T자/십자 등 배관 퍼즐용 |
| **Coins** | 40 | 코인(회전 프레임 포함) |
| **Back tiles** | 18 | 배경 타일 |
| **Particles (3색)** | 각 7 | 충돌/획득 파티클 (blue/white/yellow) |
| **Spritesheet** | 14 | 카테고리별 시트 + XML |

> 타일이 **8색**, 공/파이프도 색상 분리 → 색 기반 게임 규칙에 그대로 사용.

---

## 2. 언제 사용하면 되는가?

- **벽돌깨기(Breakout / Arkanoid)** — 공·패들·벽돌 타일 완비
- **핀볼 / 볼 바운스** 퍼즐
- **파이프 연결 퍼즐** (Pipes 직선·코너·분기 타일)
- 색 매칭/수집 캐주얼 퍼즐

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **파일 수가 많음(814개)** — 8색 × 다수 형태 조합 때문. 필요한 색/카테고리만 선택 임포트 권장.
- **파일명이 의미 기반**(`Tiles red`, `Balls/Blue`, `Pipes/Green`) → 폴더·색 단위 관리 쉬움.
- **파이프 연결 타일 풀세트** — 직선/코너/T/십자 제공으로 배관 퍼즐 즉시 구성.
- **카테고리별 스프라이트시트** 제공 — 시트 단위 임포트 편리.
- 참고: puzzle-pack-1은 **매치-3 젬 계열**로 성격이 다름 → [arts-puzzle-pack-1.md](arts-puzzle-pack-1.md) 참조.

---

## 형태 선택 가이드

- **벽돌깨기:** `Tiles <색>/`(벽돌) + `Balls/` + `Paddles/`
- **파이프 퍼즐:** `Pipes/<색>/`
- **시트 일괄 임포트:** `Spritesheet/spritesheet_<카테고리>` + XML
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `Preview.png`(전체), `Sample1~4.png`(배치 예시)
