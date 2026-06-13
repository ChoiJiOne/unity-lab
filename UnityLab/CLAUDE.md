# CLAUDE.md

## 문서 경로 참조 지침

문서 경로를 참조할 때는 절대 경로를 사용하지 말고 상대 경로를 사용한다.
이때 상대 경로는 경로를 기재하는 **해당 파일 기준**의 상대 경로로 작성한다.

## 샘플 에셋

샘플 에셋(목록 조회, 특징 확인, 샘플 선택, 사용법, 새 에셋 추가 등) 관련 작업을 진행할 때는
먼저 [`ai-docs/sample-assets-index.md`](ai-docs/sample-assets-index.md) 의 지침과 에셋 문서 인덱스를 확인한다.

## 프로토타입 게임 주제

프로토타입 게임 주제 관련 작업을 진행할 때는
[`../Docs/asset-prototype-game-ideas.md`](../Docs/asset-prototype-game-ideas.md) 를 참조한다.

## 게임 기획 문서

게임 기획 문서를 작성할 때는
[`../Docs/workflow/gdd-authoring-workflow.md`](../Docs/workflow/gdd-authoring-workflow.md) 의 작성 절차를 따르고,
[`../Docs/templates/gdd-template.md`](../Docs/templates/gdd-template.md) 양식을 사용한다.

## 개발 설계 문서 (TDD)

개발 설계서를 작성할 때는
[`../Docs/workflow/tdd-authoring-workflow.md`](../Docs/workflow/tdd-authoring-workflow.md) 의 작성 절차를 따르고,
[`../Docs/templates/tdd-template.md`](../Docs/templates/tdd-template.md) 양식을 사용한다.
현 프로토타입은 **순수 C#/Unity** 로 구현하며(PuerTS는 향후 리서치 대상), end-to-end 구현 가능성과
**Unity MCP 연결 확인**을 선행 점검한다.

## 프로토타입 폴더 구조

프로토타입을 스캐폴딩·구현할 때는 [`ai-docs/prototype-structure.md`](ai-docs/prototype-structure.md) 규약을 따른다.
각 프로토타입은 `Assets/Prototype/<PascalName>/` 자기완결 폴더(asmdef 격리 + 씬 + 엔트리 포인트 + DEVLOG)로 둔다.

## 유니티 리서칭 주제

유니티 리서칭 주제 관련 작업을 진행할 때는
[`../Docs/unity-research-topics.md`](../Docs/unity-research-topics.md) 를 참조한다.
