# Kenney Desert Shooter Pack 에셋 가이드

탑다운 픽셀아트 슈터용 2D 에셋 팩입니다.
사막 테마의 타일맵·캐릭터·무기·UI·사운드까지 포함해 한 게임을 통째로 만들 수 있습니다.

- **경로:** `Assets/Arts/kenney_desert-shooter-pack_1.0/`
- **제작자:** Kenney (www.kenney.nl) · 버전 1.0 · 스폰서 GameMaker
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

사막 테마의 **탑다운(top-down) 픽셀아트 슈터 풀세트**입니다.
PNG 스프라이트 517개 + OGG 사운드 40개로 구성됩니다.

| 카테고리 | 개별 스프라이트 | 내용 |
|----------|------:|------|
| **Tiles** | 234 | 사막 환경 타일 (바닥, 벽, 구조물 등) |
| **Interface** | 198 | UI — 숫자/알파벳 비트맵 폰트, 아이콘, 버튼, HUD |
| **Weapons** | 40 | 무기 및 발사체/이펙트 |
| **Enemies** | 16 | 적 캐릭터 |
| **Players** | 16 | 플레이어 캐릭터 (애니메이션 프레임) |
| **Sounds** | 40 (OGG) | shoot, explosion, hurt, coin, jump, move, select 등 |

> 각 카테고리는 **`Tiles/`(낱개 PNG)** 와 **`Tilemap/`(스프라이트시트 1장 + 매핑)** 두 형태로 모두 제공됩니다.

---

## 2. 언제 사용하면 되는가?

- **탑다운 슈터 / 트윈스틱 슈터 프로토타입**
- 사막·서바이벌 테마 2D 게임
- 픽셀아트 타일맵 레벨 디자인 실험 (Unity Tilemap 연동)
- UI 폰트·HUD가 포함되어 **아트 없이 완결된 미니 게임** 제작 가능
- 사운드까지 있어 입력 피드백/효과음 프로토타이핑

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유. 생성일 2024-04-24.
- **스프라이트 시트 vs 낱개 선택 가능** — `Tilemap/` 시트를 Unity에서 Multiple로 슬라이스하거나, `Tiles/`의 낱개 PNG를 바로 사용.
- **픽셀아트 임포트 설정 권장** — `Filter Mode: Point (no filter)`, `Compression: None`, 필요 시 `Pixels Per Unit` 통일.
- **파일명이 `tile_0000.png` 식 인덱스** — 의미를 알려면 `Preview.png` / `Sample A·B.png`로 눈으로 매칭해야 함 (이름만으론 식별 불가).
- **Interface에 비트맵 폰트 글리프 포함** — 숫자/알파벳을 스프라이트로 찍어 점수·HUD 표현 가능.
- **사운드는 OGG** — Unity 기본 지원 포맷이라 바로 사용 가능.

---

## 형태 선택 가이드

- **Unity Tilemap으로 레벨 제작:** `Tiles/`의 낱개 PNG → Tile Palette 등록
- **수동 배치/단일 오브젝트:** `Tiles/`의 낱개 PNG 직접 사용
- **시트로 한 번에 임포트:** `Tilemap/` 스프라이트시트 → Sprite Editor에서 Slice
- **루트 참고 파일:** `Preview.png`(전체), `Sample A.png`·`Sample B.png`(배치 예시)
