---
title: 프로토타입 폴더 구조 규약
type: convention
status: approved
tags: [unity, prototype, structure, asmdef]
---

# 프로토타입 폴더 구조 규약

모든 프로토타입 게임은 **자기완결(self-contained)** 폴더로 만든다. 한 프로토타입에 필요한
씬·코드·게임 한정 에셋·작업 기록이 모두 한 폴더 안에 있고, 코드는 **asmdef로 격리**한다.
이 규약은 한 번만 정의하며, 각 게임의 구체값(폴더명·씬명 등)은 해당 게임의 TDD가 채운다.

## 폴더 레이아웃

```
Assets/Prototype/<PascalName>/
  <PascalName>.asmdef            # 코드 격리 어셈블리 (rootNamespace = Prototype.<PascalName>)
  DEVLOG.md                      # 작업 기록·의사결정 로그 (문제→원인→해결)
  Scenes/
    <PascalName>.unity           # 실행 씬 (엔트리 씬)
  Scripts/
    <PascalName>GameRoot.cs      # 엔트리 포인트(부트스트랩) — 씬에 1개 배치
    ...                          # 그 외 게임 로직
  Art/                           # (선택) 이 게임 한정 스프라이트/머티리얼
  Prefabs/                       # (선택) 이 게임 한정 프리팹
```

- `<PascalName>` 은 PascalCase 게임명(예: `TappyPlane`). 폴더·asmdef·씬·엔트리포인트 클래스 접두에 일관 사용.
- **공용 샘플 에셋**(`Assets/Arts/` 등)은 폴더 안에 복사하지 않고 그대로 참조한다. 폴더에는 이 게임 한정 산출물만 둔다.

## asmdef 규칙

- 어셈블리 이름은 `Prototype.<PascalName>`, `rootNamespace` 도 동일하게 둔다.
- **참조(references) 는 최소화** — 필요한 공용 어셈블리(예: `UniTask`)만 명시 참조한다.
- **프로토타입끼리 상호 참조 금지** — 각 프로토타입은 독립적으로 빌드·실행 가능해야 한다.
- 공용으로 재사용할 코드가 생기면 프로토타입 밖의 공용 어셈블리로 분리한 뒤 참조한다.

## 엔트리 포인트 규칙

- `<PascalName>GameRoot` (MonoBehaviour) 를 엔트리 포인트로 두고 **씬에 단 1개** 배치한다.
- GameRoot 가 게임 루트 흐름(상태 머신 소유, 시스템 초기화)을 책임진다.
- 씬은 GameRoot 가 올라간 오브젝트 + 카메라만 있는 상태에서도 **Play 가 돌아야** 한다(M0 수직 슬라이스).

## 작업 기록(DEVLOG)

- 4단계(테스트·기록)의 산출물. `Assets/Prototype/<PascalName>/DEVLOG.md` 에 둔다.
- 양식은 [`../../Docs/templates/devlog-template.md`](../../Docs/templates/devlog-template.md).
- **문제 → 원인 → 해결/결정** 을 시간순으로 남겨 의사결정 과정을 추적한다.

## 전체 흐름에서의 위치

GDD(무엇) → TDD(어떻게·어디에) → **이 구조로 스캐폴딩(M0)** → 구현 → DEVLOG 기록.
설계 절차는 [`../../Docs/workflow/tdd-authoring-workflow.md`](../../Docs/workflow/tdd-authoring-workflow.md) 참조.
