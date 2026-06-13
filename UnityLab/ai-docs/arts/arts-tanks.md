# Kenney Tanks 에셋 가이드

탑다운 탱크 전투 게임용 2D 에셋 팩입니다.
지형 테마별 탱크(차체+포신 분리)와 총알·지뢰·보급상자로 탱크 게임을 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_tanks/`
- **제작자:** Kenney (www.kenney.nl) · Tanks Pack
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

위에서 본 **탱크 전투용 차량 + 전장 요소 세트**입니다. 플랫/벡터 스타일.
88종 × (Default size / Retina) = PNG 176개 + SVG/SWF 벡터 + 스프라이트시트로 구성됩니다.

| 카테고리 | 내용 |
|----------|------|
| **Tanks (테마 4종)** | Green/Dark/Desert/Sand 등 진영·지형별 탱크. `tank<테마>` 완성형 + `tank<테마>_body`(차체) + 포신(barrel) 분리 |
| **Barrels** | 포신(Green/Grey/Red) — 차체와 별도 회전 |
| **Bullets / Explosions** | 총알, 폭발/충돌 이펙트 |
| **Mines** | 지뢰(Off/On/Pressed 상태) |
| **Crates** | 보급 상자(Ammo/Armor/Repair/Wood) |
| **Default size / Retina** | 1배 / 2배 해상도 |
| **Spritesheet** | 통합 시트 + XML |

> **차체(body) + 포신(barrel) 분리** → 포신만 독립 회전(조준). 4가지 지형 테마 탱크로 진영 구분.

---

## 2. 언제 사용하면 되는가?

- **탑다운 탱크 배틀** — 차체 이동 + 포신 독립 조준
- 2인 대전/아레나, 캠페인 탱크 슈터
- 지뢰·보급(탄약/장갑/수리) 아이템 메커닉
- top-down-tanks-remastered와 유사 — 이쪽은 **지형 테마 탱크** 중심.

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **차체+포신 분리 구조** — 두 스프라이트를 부모-자식으로 두고 포신만 회전(핵심).
- **파일명이 의미 기반**(`tanks_tankDesert1`, `tanks_tankDesert_body1`, `tanks_mineOn`) → 테마/상태 식별 쉬움.
- **지뢰 상태(Off/On/Pressed)** — 트랩 기믹 표현.
- **보급 상자 4종** — 아이템 드롭/픽업.
- **Default / Retina 2해상도** — 고DPI는 Retina.
- **플랫 벡터 스타일** — 픽셀아트 아님.
- 더 큰 전장 타일/총알 변형이 필요하면 [arts-top-down-tanks-remastered.md](arts-top-down-tanks-remastered.md) 참조.

---

## 형태 선택 가이드

- **플레이어/적 탱크:** `tank<테마>_body` + 포신(barrel) 분리 회전
- **발사체/폭발:** bullet/explosion 스프라이트
- **트랩/아이템:** `mine*`, `crate*`
- **고해상도:** `PNG/Retina/`
- **시트 일괄 임포트:** `Spritesheet/` + XML
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
