---
title: Tappy Plane 기획서
type: gdd
status: draft        # draft → review → approved
tags: [unity, prototype, arcade, hypercasual]
---

# Tappy Plane — Lean GDD

> 1인·프로토타입 규모용 경량 기획서. 프로토타입 주제 #6(Flappy Bird류 원탭 아케이드)을
> [`../../templates/gdd-template.md`](../../templates/gdd-template.md) 양식으로 작성했다.
> 주제 목록은 [`../../asset-prototype-game-ideas.md`](../../asset-prototype-game-ideas.md) 참조.

## 1. 한 줄 컨셉 (Elevator Pitch)

"플레이어가 **한 손가락 탭**만으로 비행기를 띄워, 끝없이 흐르는 지형과 장애물 사이를 통과하며 **최고 점수에 도전**하는 원탭 하이퍼캐주얼 아케이드."

## 2. 핵심 루프 (Core Loop)

탭으로 상승 → 중력으로 하강 → 장애물 한 쌍을 통과하면 +1점 → 충돌하면 즉시 종료 → 점수 확인 후 재시도. (한 손으로 무한 반복)

## 3. 메커닉 / 규칙

- **조작:** 화면 탭(또는 마우스 클릭/스페이스) 1버튼. 누를 때마다 위로 순간 상승 임펄스.
- **물리:** 상시 중력으로 하강. 탭 간격으로 고도를 유지 — 입력 안 하면 추락.
- **장애물:** 가로로 스크롤되는 바위(상/하 한 쌍) 사이 빈 공간을 통과. 위·아래 지형(천장/바닥)에도 충돌.
- **승리/패배:** 명시적 승리 없음(엔드리스). 장애물·지형에 닿으면 패배(1-hit).
- **점수:** 장애물 한 쌍을 통과할 때마다 +1. 최고 점수(베스트)는 로컬 저장. 일정 점수마다 메달(동/은/금) 부여.
- **난이도:** 스크롤 속도·장애물 간격을 점수에 따라 점진 상승(초기 고정값 → 단계적 가속).

## 4. 게임 상태 머신

`Ready → Playing → GameOver → (재시작) → Ready`

- **Ready:** "GET READY" 표시, 비행기 호버링(중력 정지). 첫 탭 입력 시 → Playing.
- **Playing:** 중력·스크롤·장애물 스폰·점수 활성. 충돌 발생 시 → GameOver.
- **GameOver:** "GAME OVER" + 점수/베스트/메달 패널, TAP(재시작) 버튼 표시. 입력 시 → Ready로 리셋.

※ 이 3-상태 머신과 점수 계산을 **PuerTS/TypeScript** 로직 검증 단위로 사용한다(아래 7절).

## 5. 화면 / 씬 구성

- **씬 수:** 1개(`TappyPlane`). 메뉴 없이 인게임에서 Ready/GameOver 오버레이로 흐름 처리.
- **레이어 구성:**
  - 배경(`background`) — 느린 패럴랙스 스크롤
  - 지형(`groundGrass` 등) — 바닥(필요 시 천장) 무한 타일 스크롤
  - 장애물(`rock`/`rockGrass`/spike) — 풀링되어 우→좌 이동
  - 플레이어(`planeBlue1~3` 프로펠러 애니메이션)
- **HUD/UI:**
  - 상단 중앙 점수 — 비트맵 숫자(`Numbers/`)
  - Ready 오버레이 — `UI/textGetReady`, `tap` 버튼
  - GameOver 오버레이 — `UI/textGameOver`, 점수/베스트, 메달(`medal*`/`star*`), 재시작 `tap` 버튼

## 6. 에셋 매핑

핵심 에셋: **Kenney Tappy Plane** (`Assets/Arts/kenney_tappy-plane/`) — 단일 팩으로 완결.
상세는 [`../../../UnityLab/ai-docs/arts/arts-tappy-plane.md`](../../../UnityLab/ai-docs/arts/arts-tappy-plane.md) 참조.

| 게임 요소 | 사용 에셋 (폴더/파일) |
|---|---|
| 플레이어 비행기 | `PNG/Planes/planeBlue1~3` (4색 중 택1, 프로펠러 프레임 애니메이션) |
| 배경 | `PNG/background` (패럴랙스) |
| 지형(바닥/천장) | `PNG/groundGrass`(또는 Dirt/Ice/Rock/Snow) — 가로 무한 타일링 |
| 장애물 | `PNG/rock`, `PNG/rockGrass`, spike류 (상/하 한 쌍) |
| 점수 폰트 | `PNG/Numbers/` (비트맵 숫자 0~9) — 별도 폰트 임포트 불필요 |
| 문구 UI | `PNG/UI/` GET READY · GAME OVER 텍스처 |
| 버튼 | `PNG/UI/tap` (시작/재시작) |
| 보상 표시 | `PNG/UI/` 메달·별(medal/star) |
| 일괄 임포트(선택) | `Spritesheet/` 통합 시트 + XML |

> 모든 비주얼이 한 폴더에 있어 추가 아트가 필요 없다. 사운드가 필요하면 `kenney_ui-pack`(범용 UI 사운드)을 부가로 얹는다.

## 7. 기술 메모

- **엔진:** Unity 6000.4.9f1, 2D.
- **스크립팅:** **순수 C#/Unity** 로 구현한다(MonoBehaviour 입력·물리·렌더·오브젝트 풀 + 일반 C# 로직).
  PuerTS(TypeScript)는 회사 환경 대비용 향후 리서치 대상이며 이 프로토타입에서는 사용하지 않는다.
- **주요 시스템:**
  - 무한 스크롤(지형·배경 타일 재배치 또는 머티리얼 오프셋)
  - 오브젝트 풀링(장애물 재사용)
  - 충돌 감지(Collider2D Trigger: 장애물/지형/통과 게이트)
  - 점수 게이트(장애물 쌍 중앙의 트리거로 +1)
  - 로컬 베스트 저장(`PlayerPrefs`)

## 8. MVP 범위 (Scope)

- [ ] **Must** — 탭 상승+중력, 단일 장애물 스크롤, 충돌 시 사망, 점수 +1, Ready/Playing/GameOver 흐름, 재시작
- [ ] **Must** — 비트맵 숫자 점수 HUD, GET READY / GAME OVER 오버레이
- [ ] **Nice-to-have** — 베스트 점수 저장, 메달, 난이도 점진 상승, 프로펠러·패럴랙스 애니, 사운드
- [ ] **Out of scope** — 비행기 스킨 선택, 광고/리더보드, 다중 스테이지/보스, 온라인 기능

## 9. 검증 기준 (Done의 정의)

- 탭으로 비행기를 띄워 장애물 사이를 통과할 수 있다.
- 장애물·지형 충돌 시 GameOver로 전이하고, 통과할 때마다 점수가 1씩 오른다.
- Ready → Playing → GameOver → 재시작이 입력만으로 끊김 없이 순환한다.
- 점수가 비트맵 숫자로 화면에 표시된다.
