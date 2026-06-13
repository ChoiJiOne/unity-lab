# Kenney Emotes Pack 에셋 가이드

캐릭터 감정 표현(말풍선 이모트) 아이콘 팩입니다.
하트·느낌표·물음표·수면(zZ) 등 머리 위 감정 아이콘을 표시할 때 사용합니다.

- **경로:** `Assets/Arts/kenney_emotes-pack/`
- **제작자:** Kenney (www.kenney.nl) · Emotes
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

캐릭터 머리 위에 띄우는 **감정/상태 이모트 아이콘 세트**입니다.
30종 아이콘 × 8스타일 × (픽셀/벡터) = PNG 513개 + 시트로 구성됩니다.

| 구분 | 내용 |
|------|------|
| **아이콘 30종** | alert, anger, heart(s)/heartBroken, exclamation(s), question, zZ(수면), faceHappy/Sad/Angry, star, music, cash($), drop(s), dots1~3, cross, cloud, haha 등 |
| **8 스타일** | 7가지 말풍선(balloon) 모양 + 1 투명배경(말풍선 없음) |
| **Pixel / Vector** | 픽셀아트 버전 / 부드러운 벡터 버전 각각 |
| **Spritesheets / Tilesheets** | 스타일별 통합 시트(16개) + XML |

> 같은 아이콘이 **픽셀/벡터 + 8가지 말풍선 스타일**로 제공 → 게임 톤과 말풍선 디자인에 맞춰 선택.

---

## 2. 언제 사용하면 되는가?

- **캐릭터 감정 표현** — 머리 위 하트/분노/물음표/수면 표시
- **NPC 상태 피드백** — 경계(alert ! ), 의문(?), 발견, 대화 중(dots)
- 스텔스 게임의 **감지 상태**(`!` 발견, `?` 의심)
- 시뮬레이션/펫 게임의 욕구·기분 아이콘
- 픽셀·벡터 양쪽 게임에 대응

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **픽셀 + 벡터 양쪽 제공** — 프로젝트 아트 스타일에 맞춰 폴더 선택(`PNG/Pixel` 또는 `PNG/Vector`).
- **8스타일(7 말풍선 + 투명)** — 말풍선 없는 버전은 자유 합성용.
- **파일명이 의미 기반**(`emote_heart`, `emote_exclamation`, `emote_faceHappy`) → 감정 단위로 직접 찾기 쉬움.
- **픽셀 버전 임포트** 시 `Filter Mode: Point`, **벡터 버전**은 일반 필터 권장.
- 스타일별 스프라이트시트로 일괄 임포트 가능.

---

## 형태 선택 가이드

- **픽셀아트 게임:** `PNG/Pixel/Style N/`
- **벡터/플랫 게임:** `PNG/Vector/Style N/`
- **말풍선 없이 합성:** Style 8(투명배경)
- **시트 일괄 임포트:** `Spritesheets/` 또는 `Tilesheets/` + XML
- **참고:** `Preview.png`(전체 + 스타일 비교)
