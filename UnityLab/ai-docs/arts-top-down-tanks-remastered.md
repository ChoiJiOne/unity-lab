# Kenney Top-Down Tanks Remastered 에셋 가이드

탑다운 탱크 배틀 게임용 2D 에셋 팩입니다.
분리형 차체+포탑 탱크와 전장 타일·총알·폭발로 탱크 슈터를 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_top-down-tanks-remastered/`
- **제작자:** Kenney (www.kenney.nl) · Top-down Tanks Remastered
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

위에서 본 **탱크 배틀용 차량 + 전장 타일 세트**입니다. 플랫/벡터 스타일.
PNG 382개(Default size + Retina 2배) + SVG/SWF 벡터 + 스프라이트시트/타일시트로 구성됩니다.

| 카테고리 | 내용 |
|----------|------|
| **Tanks (차체 + 포탑 분리)** | 탱크 바디(`tankBody_*`)와 포탑(`tankXXX_barrel`)이 별개 스프라이트 — 색상별 |
| **Bullets** | 총알(Blue/Green/Dark/Red × 1~3, `_outline` 변형) |
| **Barrels / Barricades** | 드럼통(4색 × top/side), 금속·나무 바리케이드 |
| **Explosions** | 폭발/충돌 이펙트 프레임 |
| **Tiles** | 전장 타일 — 잔디/모래/도로/크랙, 트랙 자국 |
| **Default size / Retina** | 동일 세트의 1배 / 2배(레티나) 해상도 |
| **Spritesheet / Tilesheet** | 통합 시트 + XML |

> **차체와 포탑이 분리** → 포탑만 독립 회전(마우스 조준)시키는 전형적 탑다운 탱크 컨트롤 구현에 최적.

---

## 2. 언제 사용하면 되는가?

- **탑다운 탱크 배틀** — Tank Wars류, 차체 이동 + 포탑 독립 조준
- **2인 대전 / 아레나 슈터**
- 전장 맵 타일 구성(트랙·도로·장애물)
- 드럼통 폭발·바리케이드 엄폐 메커닉

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **차체+포탑 분리 구조** — 두 스프라이트를 부모-자식으로 두고 포탑만 회전. (가장 큰 특징)
- **Default / Retina 2단계 해상도** — 고DPI는 Retina.
- **파일명이 의미 기반**(`tankBody_blue`, `bulletRed2_outline`, `barrelGreen_top`) → 식별·조합 쉬움.
- **드럼통 top/side 2뷰** — 시점/연출에 맞춰 선택.
- **Tilesheet 제공** — Unity Tilemap용 Slice 편리.
- **플랫 벡터 스타일** — 픽셀아트 아님.

---

## 형태 선택 가이드

- **플레이어 탱크:** `tankBody_*` + `tank*_barrel`(포탑 분리 회전)
- **발사체/폭발:** `bullet*`, 폭발 프레임
- **전장 맵:** `Tilesheet/`(Tilemap) 또는 타일 낱개
- **고해상도:** `PNG/Retina/`
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
