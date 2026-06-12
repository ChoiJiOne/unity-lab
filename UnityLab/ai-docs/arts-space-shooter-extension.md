# Kenney Space Shooter Extension 에셋 가이드

우주 슈팅 게임용 확장 에셋 팩입니다.
우주선·로켓·우주인·미사일·정거장 부품으로 우주 슈터/조립을 확장 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_space-shooter-extension/`
- **제작자:** Kenney (www.kenney.nl) · Space Shooter Extension
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

우주 슈터용 **함선·부품·우주인·발사체 확장 세트**입니다. 플랫/벡터 스타일.
PNG 561개(Sprites + Sprites X2 2배 해상도) + SVG/SWF 벡터로 구성됩니다.

| 카테고리 | 개수(1배) | 내용 |
|----------|------:|------|
| **Parts** | 98 | 모듈형 함선/구조물 부품 (조립용 패널·노즐·날개 등) |
| **Missiles** | 40 | 미사일·발사체·레이저 변형 |
| **Rocket parts** | 31 | 로켓 조립 부품(노즈콘·동체·엔진·핀) |
| **Station** | 31 | 우주 정거장 모듈 |
| **Building** | 25 | 우주 구조물/건물 |
| **Astronauts** | 18 | 우주인 캐릭터(포즈 변형) |
| **Effects** | 18 | 화염/폭발/추진 이펙트 |
| **Ships** | 9 | 완성형 우주선 |
| **Rockets / Meteors** | 각 4 | 완성형 로켓 / 운석 |

> **Sprites / Sprites X2** 2단계 해상도 제공. 부품(Parts·Rocket parts)이 많아 **모듈 조립형 함선** 제작에 강함.

---

## 2. 언제 사용하면 되는가?

- **우주 슈팅(슈먹업)** 게임 — 적기·미사일·이펙트 확장
- **우주선/로켓 조립** 시스템 (모듈 부품 결합)
- 우주 정거장·건물 배경 구성
- 우주인 캐릭터가 필요한 장면
- 기존 space-shooter 에셋의 **콘텐츠 보강(extension)** 용도

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **이름이 `extension`** — 단독 사용도 가능하나, 기본 Space Shooter 팩(space-shooter-remastered 등)과 **함께 쓰도록 설계된 보강 팩**.
- **Sprites / Sprites X2** 2단계 해상도 — 고DPI는 X2.
- **파일명이 `spaceParts_001` 식 인덱스** — 어떤 부품인지는 `Preview.png`로 매칭 필요.
- **모듈형 부품 다수** — 함선을 부품 조합으로 구성하는 워크플로에 적합.
- **플랫 벡터 스타일** — 픽셀아트 아님.
- 참고: 완성형 함선 위주 팩은 [arts-space-shooter-remastered.md](arts-space-shooter-remastered.md).

---

## 형태 선택 가이드

- **모듈 조립 함선/로켓:** `PNG/Sprites/Parts`, `Rocket parts`
- **발사체/이펙트:** `Missiles/`, `Effects/`
- **고해상도:** `PNG/Sprites X2/`
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
