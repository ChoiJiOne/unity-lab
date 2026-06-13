---
title: <게임명> 개발 설계서 (TDD)
type: tdd
status: draft        # draft → review → approved
tags: [unity, tdd, prototype, <장르>]
---

# <게임명> — Lean TDD (개발 설계서)

> 기획서(GDD)를 **어떻게 구현할지**로 옮기는 경량 기술 설계 문서.
> 대상 GDD: [`gdd.md`](gdd.md)
> 작성 절차는 [`../../workflow/tdd-authoring-workflow.md`](../../workflow/tdd-authoring-workflow.md) 참조.
>
> **스크립팅 정책:** 현 프로토타입은 **순수 C#/Unity** 로 구현한다.
> PuerTS(TypeScript)는 회사 환경 대비용 향후 리서치 대상이며 이 설계에는 포함하지 않는다.

## 1. 개요 & 범위

- 대상 GDD와 이번 설계가 덮는 **MVP(Must) 범위**를 한 줄로.
- 이 문서에서 **다루지 않는 것**(Out of scope)도 명시.

## 2. 개발 환경 & 툴링

- **Unity:** 6000.4.9f1 / 렌더 파이프라인 / 2D 패키지 등.
- **필요 패키지:** (예: 2D Sprite, Input System 등)
- **Unity MCP 연결 확인 (필수 선행):**
  - 에디터를 자동 구동·검증하려면 Unity MCP 서버가 연결돼 있어야 한다.
  - [ ] Unity MCP 서버 연결됨 (세션에 Unity MCP 도구가 노출되는지 확인)
  - 미연결 시: 연결/설정 후 진행하거나, 수동 Play 모드 검증으로 폴백.
- **에디터 자동화 범위:** 씬 열기, Play 진입, 로그/스크린샷 확인 등 MCP로 가능한 항목.

## 3. 아키텍처 개요

- 컴포넌트 구성도(주요 객체와 관계)와 **데이터 흐름**(입력 → 로직 → 상태 → 렌더).
- 매니저/엔트리 포인트와 갱신 루프(`Update`/`FixedUpdate`) 책임 구분.

## 4. 클래스 / 모듈 설계

| 클래스 / 모듈 | 타입 | 책임 | 핵심 멤버 |
|---|---|---|---|
| | MonoBehaviour / 일반 C# | | |

- 의존 방향(누가 누구를 참조하는지)과 이벤트/콜백 경로.

## 5. 상태 머신 & 핵심 로직

- GDD의 상태도를 **구현 수준**으로 구체화(전이 트리거, 각 상태의 진입/갱신/이탈 처리).
- 핵심 알고리즘(충돌 판정, 점수 게이트, 스폰 규칙 등)의 의사코드 또는 요약.

## 6. 데이터 & 설정

- 튜닝 파라미터(중력·속도·간격 등)와 보관 위치(`[SerializeField]` / ScriptableObject).
- 영속 데이터(베스트 점수 등)와 저장 방식(`PlayerPrefs` 등).

## 7. 프로젝트 구조 & 스캐폴딩

[`../../../UnityLab/ai-docs/prototype-structure.md`](../../../UnityLab/ai-docs/prototype-structure.md) 규약을 따른다.
이 게임의 구체값을 채운다.

| 항목 | 값 |
|---|---|
| 프로토타입 폴더 | `Assets/Prototype/<PascalName>/` |
| asmdef | 이름 `Prototype.<PascalName>` / 참조: (예: `UniTask` 또는 없음) |
| 엔트리 씬 | `Assets/Prototype/<PascalName>/Scenes/<PascalName>.unity` |
| 엔트리 포인트 | `Scripts/<PascalName>GameRoot.cs` (씬에 1개 배치) |
| DEVLOG | `Assets/Prototype/<PascalName>/DEVLOG.md` |

## 8. 씬 / 프리팹 / 에셋 구성

- 씬 계층(GameObject 트리)과 프리팹 목록, 풀링 대상.
- 에셋 임포트 설정: 스프라이트 슬라이싱, Pixels Per Unit, 정렬 레이어.
- 에셋 매핑은 GDD 6절을 따른다(실제 파일명).

## 9. End-to-End 구현 계획

**핵심 원칙: 가장 얇은 "실행되는" 수직 슬라이스를 먼저 세우고, 매 단계 끝에서 실제로 플레이 가능해야 한다.**

- **M0 = 스캐폴딩 수직 슬라이스:** 7절 구조(폴더·asmdef·엔트리 씬·`GameRoot`)를 만들어
  **빈 씬에서 Play 가 돌고 GameRoot 부트 로그가 찍히는** 최소 실행본.
- **마일스톤 분해:** GDD의 Must 항목을 아래처럼 단계화하되, 각 단계는 빌드·실행이 가능해야 한다.

| 단계 | 목표 | 끝났을 때 실행되는 것 |
|---|---|---|
| M0 | 스캐폴딩 | 빈 씬 Play + GameRoot 부트 로그 |
| M1 | | |
| M2 | | |

## 10. 검증 & 테스트

- **실행 확인 방법:** Unity MCP로 Play 진입 → 로그/스크린샷 확인(또는 수동 Play).
- 각 마일스톤 종료 시 **GDD 9절(Done 정의)** 항목과 매핑해 통과 여부 점검.
- (선택) EditMode/PlayMode 테스트 대상 로직.

## 11. 리스크 & 미결 사항

- 기술 리스크(성능·입력 지연·충돌 정확도 등)와 대응.
- 결정이 필요한 미결 항목.
