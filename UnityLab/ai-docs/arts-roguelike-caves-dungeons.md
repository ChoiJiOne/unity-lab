# Kenney Roguelike: Caves & Dungeons 에셋 가이드

로그라이크 동굴/던전용 픽셀 타일셋입니다.
벽·바닥·물·계단·장식으로 동굴 던전 맵을 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_roguelike-caves-dungeons/`
- **제작자:** Kenney (www.kenney.nl) · Roguelike Caves & Dungeons
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

동굴·던전 테마 **픽셀아트 타일셋(스프라이트시트 형태)**입니다.
개별 PNG가 아니라 **시트 2장**으로 제공됩니다.

| 파일 | 내용 |
|------|------|
| **roguelikeDungeon_transparent.png** | 투명 배경 타일시트 (메인 — Unity 권장) |
| **roguelikeDungeon_magenta.png** | 마젠타 컬러키 배경 버전 (레거시) |
| **spritesheetInfo.txt** | 타일 격자/크기 정보 |

**소재:** 동굴 벽(돌/거친), 바닥, 물/용암, 계단·사다리, 문, 항아리·보물, 버섯·해골 장식, 종유석 등 던전 구성 요소.

> 같은 Kenney 로그라이크 시리즈(예: roguelike RPG/city)와 **타일 규격이 호환** — 함께 섞어 쓰기 좋음. 시트 단일 제공이라 **Slice 필수**.

---

## 2. 언제 사용하면 되는가?

- **로그라이크 / 던전 크롤러** — 동굴·지하 던전 맵
- 픽셀 탑다운 RPG의 지하/동굴 구역
- 프로시저럴 던전 생성(벽/바닥/문/계단 타일 완비)
- 다른 Kenney 로그라이크 시트와 조합한 통합 타일맵

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **스프라이트시트만 제공(낱개 PNG 없음)** — Unity Sprite Editor에서 **Grid Slice 필수**. `spritesheetInfo.txt`의 타일 크기 참조(보통 16×16 + 여백).
- **transparent vs magenta** — Unity에서는 **transparent** 사용(magenta는 구형 컬러키 방식).
- **픽셀아트** — 임포트 시 `Filter Mode: Point`, `Compression: None`, `Pixels Per Unit`을 타일 크기에 맞춤.
- **Kenney Roguelike 시리즈 호환** — 동일 규격이라 RPG/도시 등과 한 Tilemap에 혼용 가능.

---

## 형태 선택 가이드

- **Unity Tilemap:** `Spritesheet/roguelikeDungeon_transparent.png` → Multiple + Grid Slice → Tile Palette
- **타일 격자 정보:** `spritesheetInfo.txt`
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
