# Kenney Physics Assets 에셋 가이드

물리 기반 퍼즐/파괴 게임용 2D 에셋 팩입니다.
재질(나무·돌·금속·유리·폭발물)별 블록과 캐릭터로 Angry Birds류 게임을 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_physics-assets/`
- **제작자:** Kenney (www.kenney.nl) · Physics Asset Pack
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

물리 시뮬레이션 게임용 **재질별 블록 + 캐릭터 에셋**입니다. 플랫/벡터 스타일.
PNG 345개 + SVG/SWF 벡터 + 스프라이트시트(재질별 분리)로 구성됩니다.

| 카테고리 | 개수 | 내용 |
|----------|------:|------|
| **Wood / Stone / Glass elements** | 각 55 | 재질별 블록 — 사각/삼각/원형, 다양한 크기 |
| **Explosive elements** | 58 | 폭발물 블록, TNT 등 |
| **Aliens** | 15 | 캐릭터(5색 × round/square/suit) — 발사체/주인공 역할 |
| **Debris** | 9 | 파괴 잔해(유리/돌/나무 파편) |
| **Backgrounds** | 8 | 배경(desert/grass/land/shroom × blue/colored) |
| **Other** | 20 | 별, 깃발, 코인, 선인장, 버튼 등 보조 |
| **Spritesheet** | 8 | 재질별 스프라이트시트 + XML |

> 블록이 **5가지 재질(나무/돌/유리/금속/폭발물)** 로 제공 → 재질별 내구도·물리 속성 차등 적용에 적합.

---

## 2. 언제 사용하면 되는가?

- **물리 파괴 퍼즐** — Angry Birds류 투척/붕괴 게임
- **2D 물리 샌드박스** — 블록 쌓기, 구조물 무너뜨리기
- 재질별 무게·마찰·파괴 연출 실험 (Unity 2D Physics)
- 폭발/잔해 이펙트 프로토타입

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **파일명이 의미 기반**(`alienBlue_round`, `debrisGlass_1`, `blue_grass`) → 식별·로딩 용이.
- **재질별 스프라이트시트 분리** — `spritesheet_wood/stone/glass/metal/explosive` 등으로 나뉘어 재질 단위 관리 편함.
- **블록 형태 다양**(사각/삼각/원형/크기 변형) → Unity Collider 형태에 맞춰 선택.
- **Aliens 5색** — 캐릭터/발사체 구분에 활용.
- **플랫 벡터 스타일** — 픽셀아트 아님.

---

## 형태 선택 가이드

- **개별 물리 오브젝트:** `PNG/<재질>/` 낱개 (각자 Collider 부여)
- **재질 단위 일괄 임포트:** `Spritesheet/spritesheet_<재질>` + XML
- **고해상도/벡터:** `Vector/` (SVG)
- **참고:** `preview.png`(전체), `sample.png`(배치 예시)
