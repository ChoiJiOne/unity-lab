---
title: 게임 기획서 작성 워크플로우
type: workflow
status: approved
tags: [workflow, gdd, prototype, process]
---

# 게임 기획서 작성 워크플로우

프로토타입 게임 주제를 골라 **Lean GDD**를 작성하기까지의 표준 절차다.
산출물은 [`../templates/gdd-template.md`](../templates/gdd-template.md) 양식을 채운 기획서이며
`Docs/Prototype/<게임명>/gdd.md` 로 둔다. 실제 사례: [`../Prototype/TappyPlane/gdd.md`](../Prototype/TappyPlane/gdd.md).

## 절차

### 1. 주제 후보 선정

[`../asset-prototype-game-ideas.md`](../asset-prototype-game-ideas.md) 에서 후보를 고르고 **선정 근거**를 함께 제시한다.
"기획서 바로 뽑아 구현 착수"가 목적이면 다음 기준으로 우선순위를 매긴다.

- **에셋 완결도** — 추가 아트 없이 한 폴더/조합으로 완결되는가 (완결도 ★ 표기 참고).
- **스코프** — 단일 화면·단일 메커닉처럼 작고 위험이 낮은가.
- **규칙 표준성** — 해석 여지가 적어 기획서를 빈칸 없이 채울 수 있는가.
- **학습/검증 가치** — PuerTS(TS) 로직 검증 등 저장소 목표와 묶이는가.

여러 후보를 표로 비교하되 **1순위 추천 1개를 명확히** 지정한다.

### 2. 에셋 구성 검증

선정한 주제의 핵심 에셋 문서를 [`../../UnityLab/ai-docs/arts/`](../../UnityLab/ai-docs/arts/) 의 `arts-*.md` 에서 열어
실제 폴더·파일 구성(캐릭터/타일/UI/폰트/사운드 유무)을 확인한다. 기획서 6절 에셋 매핑에 **실제 파일명**을
박아 넣어 구현 중 에셋 탐색으로 멈추지 않게 한다.

### 3. 템플릿 채우기

[`../templates/gdd-template.md`](../templates/gdd-template.md) 를 복사해 9개 절을 모두 채운다. 특히:

- **4절 상태 머신** — Ready/Playing/GameOver 등 전이 조건을 명시(= 로직 구현 단위).
- **6절 에셋 매핑** — 2단계에서 확인한 실제 파일명으로 표를 채움.
- **7절 기술 메모** — C# 호스트 / PuerTS(TS) 로직 분담과 핵심 시스템(스크롤·풀링·충돌 등).
- **8절 MVP 범위** — Must / Nice-to-have / Out-of-scope 로 스코프를 잠금.
- **9절 Done 정의** — "무엇이 동작하면 완성인가"를 검증 가능한 문장으로.

### 4. 배치 및 규칙 준수

- 파일은 `Docs/Prototype/<게임명>/gdd.md` 로 저장한다(게임별 문서를 한 폴더에 모음).
- [`../CLAUDE.md`](../CLAUDE.md) 문서 작성 규칙을 따른다: frontmatter(`title/type/status/tags`)로 시작, 문서 간 링크는 상대 경로.
- 최초 작성 시 `status: draft` 로 두고, 검토 후 `review` → `approved` 로 올린다.

## 체크리스트

- [ ] 후보를 근거와 함께 비교하고 1순위를 지정했다
- [ ] 핵심 에셋의 실제 파일 구성을 `ai-docs/arts/` 에서 확인했다
- [ ] 9개 절을 모두 채웠다(에셋 매핑은 실제 파일명)
- [ ] `Docs/Prototype/<게임명>/` 에 frontmatter·상대경로 규칙을 지켜 저장했다
- [ ] MVP "Must" 와 Done 정의로 스코프가 잠겼다
