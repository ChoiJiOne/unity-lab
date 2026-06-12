# Kenney Top-Down Shooter 에셋 가이드

탑다운 슈터(생존/좀비) 게임용 2D 에셋 팩입니다.
무기별 포즈 캐릭터 10종과 524개 환경 타일로 탑다운 슈터를 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_top-down-shooter/`
- **제작자:** Kenney (www.kenney.nl) · Topdown Shooter Pack
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

위에서 내려다보는 **탑다운 슈터용 캐릭터 + 환경 타일 세트**입니다. 플랫/벡터 스타일.
PNG 587개 + SVG/SWF 벡터 + 스프라이트시트/타일시트로 구성됩니다.

| 카테고리 | 개수 | 내용 |
|----------|------:|------|
| **캐릭터 10종** | 각 6 포즈 | Hitman / Man Blue / Man Brown / Man Old / Robot 1 / Soldier 1 / Survivor 1 / Woman Green / Zombie 1 (+루트) |
| └ 포즈 변형 | — | `_stand / _gun / _hold / _machine / _silencer / _reload` (무기·동작별) |
| **Tiles** | 524 | 환경 타일 — 바닥/벽/도로/장애물, 무기·탄약·아이템 소품 |
| **Spritesheet / Tilesheet** | 각 2 | 통합 시트 + XML |

> 캐릭터가 **무기별 포즈(권총/기관총/소음기/장전/맨손)** 로 제공 → 무기 전환 시 스프라이트만 교체하면 됨. 좀비 포함으로 생존 슈터에 적합.

---

## 2. 언제 사용하면 되는가?

- **탑다운 슈터 / 트윈스틱 슈터** (위에서 본 시점)
- **좀비 생존** 게임 (Zombie 캐릭터 포함)
- 무기 전환 시스템(포즈 스왑) 구현
- 524개 타일로 실내/도시 맵 구성

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **캐릭터 = 위에서 본 단일 스프라이트** — 이동은 코드로 회전(rotation)시켜 360도 조준에 사용. (걷기 프레임 애니메이션은 별도 구성 필요)
- **무기별 포즈 6종** — `_gun/_machine/_silencer/_reload` 스왑으로 무기·장전 표현.
- **캐릭터 파일명은 의미 기반**, 단 **타일은 `tile_xxx` 인덱스** → 타일은 Preview/Tilesheet로 매칭.
- **Tilesheet 제공** — Unity Tilemap용 격자 Slice 편리.
- **플랫 벡터 스타일** — 픽셀아트 아님.
- 참고: desert-shooter는 픽셀아트 탑다운 슈터로 톤이 다름 → [arts-desert-shooter-pack.md](arts-desert-shooter-pack.md).

---

## 형태 선택 가이드

- **플레이어/적 캐릭터:** `PNG/<캐릭터>/`(포즈 선택, 코드로 회전)
- **맵 타일:** `PNG/Tiles/` 또는 `Tilesheet/`(Tilemap)
- **시트 일괄 임포트:** `Spritesheet/` + XML
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
