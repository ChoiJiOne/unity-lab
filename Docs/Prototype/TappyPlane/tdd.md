---
title: Tappy Plane 개발 설계서 (TDD)
type: tdd
status: draft
tags: [unity, tdd, prototype, arcade]
---

# Tappy Plane — Lean TDD (개발 설계서)

> 대상 GDD: [`gdd.md`](gdd.md)
> 작성 절차: [`../../workflow/tdd-authoring-workflow.md`](../../workflow/tdd-authoring-workflow.md)
>
> **스크립팅 정책:** 순수 C#/Unity 로 구현한다. PuerTS는 사용하지 않는다.

## 1. 개요 & 범위

- GDD의 MVP(Must): 탭 상승+중력, 장애물 스크롤·충돌 사망, 점수 +1, Ready/Playing/GameOver 흐름·재시작, 점수 HUD.
- 이 문서 범위: 위 Must를 end-to-end로 구현하기 위한 구조·클래스·마일스톤.
- 다루지 않음: 베스트 저장/메달/난이도 곡선/사운드(Nice-to-have), 스킨·온라인(Out of scope).

## 2. 개발 환경 & 툴링

- **Unity:** 6000.4.9f1, URP, 2D. 사용 패키지: 2D Sprite, Input System(기존 `InputSystem_Actions`), UniTask(선택).
- **Unity MCP 연결 확인 (필수 선행):**
  - [ ] **미연결** — 현재 세션에 Unity MCP 도구가 노출되지 않음.
  - 폴백: 에디터에서 수동으로 씬 열기 → Play 검증. 손으로 작성한 씬/`.meta`는 **에디터로 한 번 열어 임포트·검증**해야 한다.
- **에디터 자동화 범위:** MCP 연결 시 씬 열기·Play·로그/스크린샷 확인을 도입.

## 3. 아키텍처 개요

데이터 흐름: `입력(탭) → GameRoot(상태) → Player 물리 / Spawner / Score → 렌더·HUD`.

- `TappyPlaneGameRoot` 가 상태 머신을 소유하고 각 시스템을 켜고 끈다(엔트리 포인트, 씬에 1개).
- 물리는 `FixedUpdate`(Rigidbody2D), 입력 수집은 `Update`.

## 4. 클래스 / 모듈 설계

| 클래스 | 타입 | 책임 | 핵심 멤버 |
|---|---|---|---|
| `TappyPlaneGameRoot` | MonoBehaviour | 상태 머신 소유, 시스템 초기화/리셋, 점수 보관 | `State`, `OnTap()`, `StartGame()`, `GameOver()`, `Restart()` |
| `PlayerPlane` | MonoBehaviour | 탭 임펄스 상승·중력 하강, 충돌 알림 | `Flap()`, `OnCollisionEnter2D` |
| `ObstacleSpawner` | MonoBehaviour | 장애물 쌍 풀링 스폰·스크롤·재활용 | `Spawn()`, `Despawn()`, 풀 |
| `ScrollingBackground` | MonoBehaviour | 배경·지형 무한 타일 스크롤 | `speed` |
| `ScoreGate` | MonoBehaviour | 장애물 쌍 중앙 트리거, 통과 시 +1 | `OnTriggerEnter2D` |
| `ScoreHud` | MonoBehaviour | 비트맵 숫자로 점수 표시 | `SetScore(int)` |

- 의존: 각 시스템은 `GameRoot` 를 통해 이벤트(탭/충돌/득점)를 주고받는다. 시스템 간 직접 참조 최소화.

## 5. 상태 머신 & 핵심 로직

- `enum State { Ready, Playing, GameOver }`.
- **Ready:** 중력 off, 비행기 호버. 탭 → `StartGame()` → Playing.
- **Playing:** 중력·스크롤·스폰·점수 on. 충돌 알림 수신 → `GameOver()`.
- **GameOver:** 입력·물리 정지, 패널 표시. 탭 → `Restart()` → 씬 상태 리셋 → Ready.
- 점수: `ScoreGate` 트리거당 +1 → `GameRoot` 가 보관 → `ScoreHud.SetScore`.

## 6. 데이터 & 설정

- 튜닝 파라미터(`[SerializeField]`): 중력 스케일, 탭 임펄스, 스크롤 속도, 장애물 간격/갭 높이.
- 영속 데이터: (Nice-to-have) 베스트 점수 `PlayerPrefs`. MVP에서는 세션 내 점수만.

## 7. 프로젝트 구조 & 스캐폴딩

[`../../../UnityLab/ai-docs/prototype-structure.md`](../../../UnityLab/ai-docs/prototype-structure.md) 규약을 따른다.

| 항목 | 값 |
|---|---|
| 프로토타입 폴더 | `Assets/Prototype/TappyPlane/` |
| asmdef | 이름 `Prototype.TappyPlane` / 참조: 없음(MVP), 필요 시 `UniTask` |
| 엔트리 씬 | `Assets/Prototype/TappyPlane/Scenes/TappyPlane.unity` |
| 엔트리 포인트 | `Scripts/TappyPlaneGameRoot.cs` |
| DEVLOG | `Assets/Prototype/TappyPlane/DEVLOG.md` |

## 8. 씬 / 프리팹 / 에셋 구성

- 씬 계층: `Main Camera`(Orthographic, 2D), `GameRoot`(엔트리 포인트), 이후 `Player`/`Background`/`Spawner` 추가.
- 프리팹(M1+): 장애물 쌍, 비행기.
- 에셋 임포트: `kenney_tappy-plane` 스프라이트 Pixels Per Unit·슬라이싱 확인. 정렬 레이어 배경<지형<장애물<플레이어<UI.
- 에셋 매핑은 GDD 6절을 따른다.

## 9. End-to-End 구현 계획

| 단계 | 목표 | 끝났을 때 실행되는 것 |
|---|---|---|
| **M0** | 스캐폴딩 | 빈 씬 Play + `[TappyPlane] GameRoot 부트` 로그 |
| M1 | 플레이어 물리 | 탭으로 비행기 상승·중력 하강 |
| M2 | 스크롤·장애물 | 배경/지형 스크롤 + 장애물 쌍 스폰·충돌 사망 |
| M3 | 점수·흐름 | 통과 시 +1, Ready/Playing/GameOver·재시작, 점수 HUD |

각 단계는 빌드·Play 가능 상태를 유지한다.

## 10. 검증 & 테스트

- 실행 확인: Unity MCP 연결 시 Play/로그/스크린샷, 미연결 시 수동 Play.
- 각 마일스톤을 GDD 9절 Done 항목과 매핑해 점검.
- 진행·문제·결정은 `Assets/Prototype/TappyPlane/DEVLOG.md` 에 기록.

## 11. 리스크 & 미결 사항

- **씬/`.meta` 수기 작성** — 에디터 밖에서 만든 M0 씬은 Unity 로 한 번 열어 임포트·GUID·컴포넌트 연결을 검증해야 한다(미검증 상태로 "완료" 처리 금지).
- Unity MCP 미연결 — 자동 검증 경로가 없어 수동 Play 의존. 연결되면 검증 자동화.
- 충돌 정확도/입력 지연 — M1~M2에서 파라미터 튜닝으로 대응.
