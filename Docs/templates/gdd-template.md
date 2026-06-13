---
title: <게임명> 기획서
type: gdd
status: draft        # draft → review → approved
tags: [unity, prototype, <장르>]
---

# <게임명> — Lean GDD

> 1인·프로토타입 규모용 경량 기획서 양식. 풀 GDD 대신 핵심만 담아
> "기획서 바로 뽑아 구현 착수"를 목표로 한다. 프로토타입 주제 목록은
> [`../../asset-prototype-game-ideas.md`](../../asset-prototype-game-ideas.md) 참조.

## 1. 한 줄 컨셉 (Elevator Pitch)

"<누가> <어떤 조작>으로 <핵심 재미>를 즐기는 <장르> 게임."

## 2. 핵심 루프 (Core Loop)

입력 → 결과 → 보상 → 반복. (한 사이클을 한 문장으로)

## 3. 메커닉 / 규칙

- 조작:
- 승리/패배 조건:
- 점수·진행 규칙:

## 4. 게임 상태 머신

Ready → Playing → GameOver (전이 조건 명시)
※ 이 절은 PuerTS/TS 로직 검증 단위와 직결

## 5. 화면 / 씬 구성

- 화면 수, 각 화면 요소(HUD, 버튼 등)

## 6. 에셋 매핑

| 게임 요소 | 사용 에셋 (폴더/파일) |
|---|---|
| 플레이어 | ... |
| 배경/장애물 | ... |
| UI/폰트/사운드 | ... |

※ [`../../../UnityLab/ai-docs/`](../../../UnityLab/ai-docs/) 의 `arts-*.md` 에서 실제 파일명으로 채움

## 7. 기술 메모

- 스크립팅: C# / PuerTS(TS) 분담
- 주요 시스템: 스크롤·풀링·충돌 등

## 8. MVP 범위 (Scope)

- [ ] 반드시 포함 (Must)
- [ ] 시간 남으면 (Nice-to-have)
- [ ] 이번엔 제외 (Out of scope)

## 9. 검증 기준 (Done의 정의)

"무엇이 동작하면 이 프로토타입은 완성인가"
