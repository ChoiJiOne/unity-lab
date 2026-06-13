# Kenney Micro Roguelike 에셋 가이드

초소형 픽셀 로그라이크용 타일셋입니다.
미니멀 픽셀 캐릭터·적·아이템·던전 타일로 로그라이크를 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_micro-roguelike/`
- **제작자:** Kenney (www.kenney.nl) · Micro Roguelike v1.4 (2021-11-01)
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

초소형(미니멀) 픽셀아트 **로그라이크 타일셋**입니다.
160종 × (Colored / Monochrome) = PNG 320개 + 타일맵 시트로 구성됩니다.

| 구분 | 개수 | 내용 |
|------|------:|------|
| **Tiles / Colored** | 160 | 채색 타일 — 캐릭터/적/아이템/무기/벽·바닥/장식 |
| **Tiles / Monochrome** | 160 | 흑백 타일(레트로/1비트 톤) |
| **Tilemap** | 4 | colored/monochrome 타일맵 시트 + packed |
| **Playdate Pulp** | — | Playdate Pulp 엔진용 포맷(zip) |

**소재:** 영웅·몬스터(슬라임/박쥐/해골), 무기·포션·열쇠·보물, 던전 벽/문/계단/함정, 횃불 등 로그라이크 필수 요소.

> 타일이 **소형 픽셀(8x8 계열)** 미니멀 디자인. Colored/Monochrome 두 톤. **Tilesheet.txt** 매핑 포함.

---

## 2. 언제 사용하면 되는가?

- **로그라이크 / 던전 크롤러** — 그리드 기반 턴제 탐험
- **미니멀/레트로 픽셀** 게임 (1비트 톤은 Monochrome)
- 프로시저럴 던전 생성 프로토타입 (타일 종류 풍부)
- 게임잼용 초경량 타일셋 (적은 용량, 빠른 구성)

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **Colored / Monochrome 2톤** — 1비트 레트로 룩은 Monochrome.
- **소형 픽셀 타일** — 임포트 시 `Filter Mode: Point`, `Compression: None`, `Pixels Per Unit`을 타일 크기에 맞춤.
- **파일명이 `tile_0000` 식 인덱스** — `Tilesheet.txt` / `Preview.png`로 매칭 필요.
- **packed 타일맵** 제공 — 여백 없는 시트로 Tilemap 슬라이스 효율적.
- **Playdate Pulp 포맷** 동봉 — Playdate 개발 시 활용.

---

## 형태 선택 가이드

- **Unity Tilemap:** `Tilemap/colored_tilemap_packed.png` → Slice → Tile Palette
- **개별 배치:** `Tiles/Colored/` 또는 `Tiles/Monochrome/` 낱개
- **1비트 레트로 룩:** `Monochrome`
- **참고:** `Preview.png`(전체), `Tilesheet.txt`(타일 매핑), `Sample.png`(배치 예시)
