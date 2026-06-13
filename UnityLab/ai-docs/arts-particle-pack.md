# Kenney Particle Pack 에셋 가이드

파티클 시스템용 텍스처 팩입니다.
연기·불꽃·마법·번개·빛 등 이펙트 파티클의 텍스처로 사용합니다.

- **경로:** `Assets/Arts/kenney_particle-pack/`
- **제작자:** Kenney (www.kenney.nl) · Particle Pack v1.1
- **라이선스:** **CC0 (Public Domain)** — 개인·교육·상업 사용 전부 자유, 크레딧 불필요(권장)

---

## 1. 어떤 종류의 에셋인가?

게임 이펙트용 **파티클 텍스처(흑백 그레이스케일) 세트**입니다.
80종 × (검정 배경 / 투명) + 회전 변형 = PNG 192개 + Unity 샘플로 구성됩니다.

| 구분 | 개수 | 내용 |
|------|------:|------|
| **PNG (Transparent)** | 80 | 투명 배경 — Unity 파티클에 바로 사용 |
| **PNG (Transparent)/Rotated** | 16 | 회전 변형 |
| **PNG (Black background)** | 80 | 검정 배경 — Additive 블렌딩용 |
| **PNG (Black background)/Rotated** | 16 | 회전 변형 |
| **Unity samples** | `.unitypackage` | 즉시 임포트 가능한 Unity 파티클 샘플 |

**소재:** 연기(smoke/cloud), 불(fire/flame), 마법(magic), 번개(lightning/spark), 빛(light/flare), 별/하트/원, 흙(dirt), 트레일 등.

> **검정 배경 버전은 Additive 블렌딩**(빛/불), **투명 버전은 Alpha 블렌딩**용. Unity 샘플 패키지 포함이 큰 장점.

---

## 2. 언제 사용하면 되는가?

- **VFX / 파티클 이펙트** — 폭발 연기, 불꽃, 마법, 타격 스파크
- 발사체 트레일, 빛 번짐(flare), 마법진
- Unity **Particle System / VFX Graph** 의 Texture Sheet
- 모든 장르의 이펙트 보강 (그레이스케일이라 색 틴팅 자유)

---

## 3. 특이 사항

- **CC0 라이선스** — 상업적 사용 포함 자유.
- **Unity 샘플(.unitypackage) 포함** — 임포트하면 설정된 파티클 예제 바로 확인 가능.
- **검정배경 vs 투명** 선택 — **Additive(빛/불) → Black background**, **Alpha(연기/일반) → Transparent**.
- **그레이스케일** — 파티클 Start Color로 원하는 색 입힘(재사용 효율적).
- **파일명이 의미 기반**(`fire_01`, `magic_03`, `smoke_05`) → 이펙트 종류로 찾기 쉬움.
- **Rotated 변형** 제공 — 방향성 이펙트에.

---

## 형태 선택 가이드

- **빛/불/마법(Additive):** `PNG (Black background)/`
- **연기/일반(Alpha):** `PNG (Transparent)/`
- **방향성 이펙트:** `Rotated/`
- **빠른 시작:** `Unity samples/`(.unitypackage 임포트)
- **참고:** `Preview.png`(전체 텍스처)
