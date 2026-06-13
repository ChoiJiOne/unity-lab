---
title: Tappy Plane 개발 로그 (DEVLOG)
type: devlog
status: ongoing
tags: [devlog, prototype, decisions]
---

# Tappy Plane — DEVLOG

구현·테스트 중 발생한 **문제와 의사결정**을 시간순으로 기록한다(최신이 위).
관련 설계: [`../../../../Docs/Prototype/TappyPlane/tdd.md`](../../../../Docs/Prototype/TappyPlane/tdd.md)

## 진행 요약

- 현재 마일스톤: **M0(스캐폴딩)** — 임포트·컴파일 확인됨, Play 로그만 미확인
- 실행 상태: **부분 검증** — 라이브 에디터가 임포트·컴파일 성공(아래). Play 진입 시 부트 로그 출력만 남음.

---

## 기록

### 2026-06-13 — M0 스캐폴딩 생성

- **상황/목표:** `prototype-structure` 규약에 따라 `Assets/Prototype/TappyPlane/` 자기완결 폴더와
  asmdef·엔트리 씬·`TappyPlaneGameRoot` 엔트리 포인트를 만들어 빈 씬 Play 가 도는 M0 수직 슬라이스를 세운다.
- **문제:** 이 세션에 Unity MCP 가 연결돼 있지 않아 에디터에서 씬을 생성·검증할 수 없다.
- **원인:** Unity 에디터/ MCP 미연결 환경. 씬(`.unity`)과 `.meta`(GUID 포함)를 손으로 작성해야 함.
- **해결/결정:** 기존 `Assets/Scenes/DefaultScene.unity`(URP) 포맷을 참조해 최소 2D 씬(Orthographic 카메라 +
  `GameRoot`)을 수기로 작성하고, 스크립트 `.meta` GUID(`c0ffee...0006`)를 씬의 `m_Script` 참조와 일치시켰다.
  TDD 9절 정책대로 **수동 Play 검증을 폴백 경로로 명시**.
- **결과:** 라이브 Unity 6000.4.9f1 에디터가 폴더·asmdef·씬·스크립트를 자동 임포트.
  - ① **컴파일 성공** — `Library/ScriptAssemblies/Prototype.TappyPlane.dll` 빌드 확인(Editor.log, `error CS` 없음). 코드 격리 동작.
  - ② **씬 임포트 정상** — `TappyPlane.unity`(guid `c0ffee...0004`)의 `m_Script` 가 스크립트 meta(`c0ffee...0006`)와 일치.
  - ③ **미확인** — Play 진입 시 `[TappyPlane] GameRoot 부트` 로그 출력. 에디터에서 Play 1회(또는 MCP 연결 시 자동) 확인 필요.
  - 참고: 손으로 만든 `.meta` GUID(`c0ffee...`)를 에디터가 그대로 수용함(폴더 meta 만 에디터가 신규 생성).
