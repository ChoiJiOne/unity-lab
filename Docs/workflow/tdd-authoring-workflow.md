---
title: 개발 설계서(TDD) 작성 워크플로우
type: workflow
status: approved
tags: [workflow, tdd, prototype, process]
---

# 개발 설계서(TDD) 작성 워크플로우

기획서(GDD)를 **구현 가능한 개발 설계서(TDD)** 로 옮기는 표준 절차다.
산출물은 [`../templates/tdd-template.md`](../templates/tdd-template.md) 양식을 채운 설계서이며
`Docs/Prototype/<게임명>/tdd.md` 로 둔다. 기획 단계 절차는 [`gdd-authoring-workflow.md`](gdd-authoring-workflow.md) 참조.

> **스크립팅 정책:** 현 프로토타입은 **순수 C#/Unity** 로 설계·구현한다.
> PuerTS(TypeScript)는 회사 환경 대비용 향후 리서치 대상이므로 TDD에 포함하지 않는다.

## 절차

### 1. 대상 GDD 확인

[`../Prototype/`](../Prototype/) 하위 대상 게임 폴더의 `gdd.md` 를 열어 **MVP(Must) 범위**와 **9절 Done 정의**, **6절 에셋 매핑**을 추출한다.
TDD는 이 셋을 구현 관점으로 옮기는 작업이다.

### 2. 개발 환경 & 툴링 점검 (선행 필수)

- Unity 버전·필요 패키지(2D Sprite, Input System 등)를 확인한다.
- **Unity MCP 연결을 확인한다.** 세션에 Unity MCP 도구가 노출되는지 점검:
  - 연결됨 → 씬 열기·Play 진입·로그/스크린샷 확인 등 에디터 자동 검증을 설계에 반영한다.
  - 미연결 → 먼저 연결/설정하거나, 수동 Play 모드 검증으로 폴백함을 명시한다.
- 이 단계 없이 설계하면 end-to-end 검증 경로가 비므로 **반드시 먼저 처리**한다.

### 3. 아키텍처 & 클래스 설계

- 데이터 흐름(입력 → 로직 → 상태 → 렌더)을 그리고 매니저/엔트리 포인트를 정한다.
- 주요 MonoBehaviour·일반 C# 모듈의 **책임과 의존 방향**을 표로 정리한다(템플릿 4절).
- GDD 상태도를 전이 트리거·진입/갱신/이탈 수준으로 구체화한다(템플릿 5절).

### 4. 프로젝트 구조 & 스캐폴딩(M0) 정의

[`../../UnityLab/ai-docs/prototype-structure.md`](../../UnityLab/ai-docs/prototype-structure.md) 규약에 따라
이 게임의 폴더·asmdef·엔트리 씬·엔트리 포인트 클래스명을 정한다(TDD 7절).

- **M0 = 스캐폴딩 수직 슬라이스:** `Assets/Prototype/<PascalName>/` 에 asmdef·`Scenes/<PascalName>.unity`·
  `Scripts/<PascalName>GameRoot.cs` 를 만들어, **빈 씬에서 Play 가 돌고 부트 로그가 찍히는** 최소 실행본을 먼저 세운다.
- 이후 M1, M2… 는 이 위에 기능을 얹는다 — 시스템을 따로따로 완성하지 않고 항상 실행되는 상태를 유지한다.

### 5. 마일스톤 분해

- GDD의 Must 항목을 M1, M2…로 나누되, **각 단계는 빌드·실행이 가능**해야 한다.
- 각 마일스톤 종료 상태에서 "무엇이 실제로 플레이되는가"를 적는다(템플릿 8절 표).

### 6. 검증 방법 정의

- 각 마일스톤을 **GDD 9절 Done 항목**과 매핑한다.
- 실행 확인 수단(Unity MCP Play/로그/스크린샷, 또는 수동 Play)을 명시한다(템플릿 9절).

### 7. 배치 & 규칙 준수

- 파일은 `Docs/Prototype/<게임명>/tdd.md` 로 저장한다(GDD 와 같은 폴더).
- [`../CLAUDE.md`](../CLAUDE.md) 규칙을 따른다: frontmatter(`title/type/status/tags`), 상대 경로 링크.
- `status: draft` 로 시작해 검토 후 `review` → `approved`.

## 체크리스트

- [ ] 대상 GDD의 MVP·Done·에셋 매핑을 추출했다
- [ ] **Unity MCP 연결을 확인**했다(미연결 시 폴백 명시)
- [ ] 아키텍처·클래스 책임과 의존 방향을 정리했다
- [ ] **End-to-end 수직 슬라이스(M0)** 를 정의했다
- [ ] 마일스톤이 각 단계마다 실행 가능하도록 분해됐다
- [ ] 각 마일스톤을 Done 정의와 매핑하고 검증 수단을 정했다
- [ ] `Docs/Prototype/<게임명>/` 에 frontmatter·상대경로 규칙을 지켜 저장했다
