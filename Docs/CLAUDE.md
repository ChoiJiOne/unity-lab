# CLAUDE.md

`Docs/` 하위 문서의 용도와 참조 시점을 정리한다.
문서 경로는 항상 **이 파일 기준의 상대 경로**로 작성한다.

## 프로토타입 게임 주제

프로토타입 게임 주제(아이디어 목록, 에셋 조합, 장르 분류 등)를 다룰 때는
[`asset-prototype-game-ideas.md`](asset-prototype-game-ideas.md) 를 참조한다.
샘플 에셋의 종류·사용 시점은 [`../UnityLab/ai-docs/`](../UnityLab/ai-docs/) 인덱스를 함께 확인한다.

## 게임 기획 문서

게임 기획서를 작성할 때는 [`workflow/gdd-authoring-workflow.md`](workflow/gdd-authoring-workflow.md) 의
작성 절차를 따른다. 양식은 [`templates/gdd-template.md`](templates/gdd-template.md)(1인·프로토타입 규모용
경량 GDD)를 사용하며, 작성한 기획서는 `Docs/Prototype/<게임명>/gdd.md` 로 둔다
(한 프로토타입의 문서는 `Docs/Prototype/<게임명>/` 폴더에 함께 모은다).

## 개발 설계 문서 (TDD)

기획서(GDD)를 구현 설계로 옮기는 개발 설계서를 작성할 때는
[`workflow/tdd-authoring-workflow.md`](workflow/tdd-authoring-workflow.md) 의 작성 절차를 따른다.
양식은 [`templates/tdd-template.md`](templates/tdd-template.md)(경량 TDD)를 사용하며,
작성한 설계서는 `Docs/Prototype/<게임명>/tdd.md` 로 둔다. 현 프로토타입은 **순수 C#/Unity** 로 설계하며(PuerTS는 향후 리서치 대상),
end-to-end 구현 가능성과 **Unity MCP 연결 확인**을 선행 점검한다.
프로토타입 폴더 구조(asmdef 격리·씬·엔트리 포인트·DEVLOG)는
[`../UnityLab/ai-docs/prototype-structure.md`](../UnityLab/ai-docs/prototype-structure.md) 규약을 따르며,
작업 기록은 [`templates/devlog-template.md`](templates/devlog-template.md) 양식으로 프로토타입 폴더 안 `DEVLOG.md` 에 남긴다.

## 유니티 리서칭 주제

유니티 리서칭 주제를 다룰 때는 다음을 참조한다.

- 주제를 고르거나 전체 목차를 훑을 때는 [`unity-research-index.md`](unity-research-index.md) (한 줄 요약 + 분류별 목차).
- 특정 주제의 상세(목적·목표, 핵심 용어, 선행 학습 자료, 리서치 범위, 산출물)가 필요할 때는 [`unity-research-topics.md`](unity-research-topics.md).
- 두 문서는 같은 주제 번호로 연결되며, index에서 주제를 고른 뒤 topics에서 상세를 확인하는 순서로 쓴다.

## 문서 작성 규칙

- 모든 문서는 frontmatter(`title`, `type`, `status`, `tags`)로 시작한다.
- 문서 간 링크는 절대 경로 대신 상대 경로를 사용한다.
