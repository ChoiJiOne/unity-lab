# Kenney Rolling Ball Assets 에셋 가이드

볼 굴리기 미로/물리 퍼즐 게임용 2D 에셋 팩입니다.
공·블록·구멍·열쇠·문으로 기울여 굴리는 미로 퍼즐을 구성할 수 있습니다.

- **경로:** `Assets/Arts/kenney_rolling-ball-assets/`
- **제작자:** Kenney (www.kenney.nl) · Rolling Ball Assets (2020-12-11)
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

기울임/굴리기 미로 퍼즐용 **공 + 블록 + 함정 세트**입니다. 플랫/벡터 스타일.
59종 × (Default / Retina) = PNG 118개 + SVG 벡터 + 스프라이트시트로 구성됩니다.

| 구분 | 내용 |
|------|------|
| **Balls** | 공 — 색상(blue/red) × large/small × alt(굴림 프레임) |
| **Blocks** | 벽/블록 — corner, large/narrow/small, locked(잠금), rotate(회전 기믹) |
| **Holes** | 구멍(목표/함정), 잠금 구멍(열쇠 필요) |
| **Items** | 열쇠, 별, 잠금장치 |
| **Backgrounds / Panels** | 배경(blue/brown/green), 버튼 패널 |
| **UI** | 숫자 0~9, `%`, `x`, 조준 마커 |
| **Default / Retina** | 1배 / 2배 해상도 |

> **공 + 잠금 블록 + 열쇠 + 목표 구멍** 조합 → 기울여 굴려 열쇠로 문 열고 골인하는 미로 퍼즐 메커닉이 그대로 구성됨. 회전 기믹 블록 포함.

---

## 2. 언제 사용하면 되는가?

- **볼 굴리기 미로** — 기울임(가속도계) 또는 키 입력으로 공 이동
- **물리 퍼즐** — 중력/경사로 공 굴리기
- 열쇠-자물쇠 기반 진행 퍼즐
- 하이퍼캐주얼 모바일(틸트) 게임 프로토타입

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **파일명이 의미 기반**(`ball_blue_large`, `block_locked_small`, `block_rotate_large`) → 식별·기믹 구분 쉬움.
- **공 alt 프레임** — 굴림 애니메이션 표현.
- **잠금 블록/구멍 + 열쇠** — 진행 잠금 퍼즐 즉시 구현.
- **숫자 UI 포함** — 점수/타이머 표시.
- **Default / Retina 2해상도** — 고DPI는 Retina.
- **플랫 벡터 스타일** — 픽셀아트 아님.

---

## 형태 선택 가이드

- **플레이어 공:** `ball_<색>_<크기>`(+alt 굴림)
- **미로 벽/기믹:** `block_*`(locked/rotate 등)
- **목표/함정:** 구멍(hole) 스프라이트
- **고해상도:** `PNG/Retina/`
- **시트 일괄 임포트:** `Spritesheet/` + XML
- **참고:** `Preview.png`(전체), `Sample.png`(배치 예시)
