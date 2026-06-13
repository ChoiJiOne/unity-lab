# Kenney Space Shooter Redux (Remastered) 에셋 가이드

우주 슈팅(슈먹업) 게임용 메인 에셋 팩입니다.
플레이어/적 함선·레이저·운석·파워업·UI에 배경·폰트·사운드까지 포함한 풀세트입니다.

- **경로:** `Assets/Arts/kenney_space-shooter-remastered/`
- **제작자:** Kenney (www.kenney.nl) · Space Shooter Remastered (+fonts & sounds)
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

세로/가로 스크롤 우주 슈터용 **함선·발사체·이펙트 풀세트 + 배경/폰트/사운드**입니다. 플랫/벡터 스타일.
PNG 301개 + 배경 + OGG 7개 + TTF 폰트 2종 + SVG/SWF 벡터로 구성됩니다.

| 카테고리 | 개수 | 내용 |
|----------|------:|------|
| **Parts** | 94 | 함선/구조물 모듈 부품 |
| **Lasers** | 48 | 레이저·총알(색상·형태 변형) |
| **Power-ups** | 32 | 방패/볼트/별/체력 등 강화 아이템 |
| **Effects** | 27 | 화염 분사(`fire00~`) 등 애니메이션 프레임 이펙트 |
| **UI** | 28 | 버튼, 게이지, 커서, 숫자 |
| **Enemies** | 20 | 적 함선 |
| **Meteors** | 20 | 운석(대·중·소, 갈색/회색) |
| **Damage** | 9 | 플레이어함 피격 단계(`_damage1~3`) |
| **Backgrounds** | 4 | 별 배경(black/blue/purple/darkPurple, 타일링) |
| **Bonus** | 7 (OGG) + 2 TTF | 효과음 + 비트맵/디스플레이 폰트 |

> **피격 단계(Damage) 스프라이트**와 **화염 애니메이션 프레임**까지 있어 함선 손상·추진 연출을 바로 구현. 배경은 **타일링 가능**.

---

## 2. 언제 사용하면 되는가?

- **우주 슈팅(슈먹업)** — 갤러그/스타폭스류 종/횡스크롤 슈터
- 함선 피격·파괴 연출(Damage 단계), 추진 화염 애니메이션
- 파워업·운석 회피 아케이드
- **폰트·사운드까지 포함**되어 단독으로 완결된 게임 제작 가능
- 우주 배경 스크롤(패럴랙스/타일링)

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **폰트(TTF) + 사운드(OGG) 포함** — 아트 외 요소까지 갖춘 "메인" 팩. (extension과 달리 단독 완결형)
- **파일명이 의미 기반**(`playerShip1_damage2`, `fire07`, `meteorBrown_big1`) → 상태/프레임 묶기 쉬움.
- **피격 단계·화염 프레임 제공** — 애니메이션/상태 전환 구현 용이.
- **배경 타일링** — `Backgrounds/`는 무한 스크롤에 적합.
- **플랫 벡터 스타일** — 픽셀아트 아님.
- 참고: 부품·로켓 보강은 [arts-space-shooter-extension.md](arts-space-shooter-extension.md).

---

## 형태 선택 가이드

- **함선/적/운석:** `PNG/`(루트 함선), `PNG/Enemies`, `PNG/Meteors`
- **발사/이펙트:** `PNG/Lasers`, `PNG/Effects`(화염 프레임)
- **강화/피격:** `PNG/Power-ups`, `PNG/Damage`
- **배경 스크롤:** `Backgrounds/`(타일링)
- **폰트/사운드:** `Bonus/`
- **참고:** `preview.png`(전체), `sample.png`(배치 예시)
