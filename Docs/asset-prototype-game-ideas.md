---
title: 샘플 에셋 기반 프로토타입 게임 주제
type: note
status: draft
tags: [unity, prototype, assets, kenney, spine]
---

# 샘플 에셋 기반 프로토타입 게임 주제

`UnityLab/ai-docs/`에 정리된 샘플 에셋(Kenney 팩 + Spine 예제) 문서를 기반으로,
개발 가능한 프로토타입 게임을 **① 단일 에셋팩 / ② 보드게임 / ③ 조합형** 세 갈래로 분류한 목록이다.
각 에셋의 종류·사용 시점·특이사항은 [`UnityLab/ai-docs/`](../UnityLab/ai-docs/)와
[`UnityLab/CLAUDE.md`](../UnityLab/CLAUDE.md)의 인덱스 표를 참고한다.

## 화풍(스타일) 주의

Kenney 에셋이라도 두 가지 화풍이 섞여 있어 조합 시 톤이 깨지지 않도록 주의한다.

- **픽셀아트:** `Desert Shooter`, `Pixel UI Pack`, `Micro Roguelike`, `Roguelike Caves & Dungeons`, `Minimap Pack` → 픽셀끼리 조합 (Emotes는 픽셀/벡터 양쪽 제공)
- **플랫/벡터(나머지 전부):** 서로 자유롭게 섞어도 톤이 안 깨짐
- **Spine / Toon Characters:** 애니메이션 캐릭터(별도 톤) — 플랫/벡터 배경과 무난히 어울림
- **단색 틴팅 소재(어디든):** `Foliage`(Flat), `Generic Items`(White), `Splat`, `Particle`(그레이스케일), `Board Game Icons` — color 틴팅으로 어느 톤에나 맞춤

아래 조합형은 대부분 **플랫/벡터 클러스터** 안에서 구성해 "섞어도 이상하지 않은" 범위로 잡았다.

---

## 1. 단일 에셋팩 (한 폴더 = 한 게임)

캐릭터·UI·(일부)폰트·사운드까지 포함해 추가 아트 없이 한 게임을 완결할 수 있는 팩들이다.

| # | 프로토타입 | 장르 | 핵심 에셋 | 완결도 |
|---|-----------|------|----------|--------|
| 1 | 사막 서바이벌 슈터 | 탑다운/트윈스틱 슈터 | Desert Shooter Pack (타일+캐릭터+무기+UI+사운드) | ★★★ 사운드·HUD 폰트 포함 |
| 2 | 갤러그/스타폭스류 슈먹업 | 종/횡스크롤 슈터 | Space Shooter Remastered (함선+피격단계+화염+폰트+사운드) | ★★★ 폰트·사운드 포함 |
| 3 | 좀비 생존 슈터 | 탑다운 슈터 | Top-Down Shooter (무기별 포즈 캐릭터 10종 + 524 타일) | ★★ 무기 전환 시스템 |
| 4 | 탱크 배틀 / 2인 대전 | 탑다운 아레나 | Top-Down Tanks (차체+포탑 분리, 전장 타일) | ★★ 포탑 독립 조준 |
| 5 | 타워디펜스 | 전략 TD | Tower Defense Top-Down (경로 타일+타워+적+HUD) | ★★ 웨이포인트+자동 조준 |
| 6 | Flappy Bird류 원탭 아케이드 | 하이퍼캐주얼 | Tappy Plane (비행기+지형+장애물+비트맵 폰트) | ★★★ 점수 폰트 포함 |
| 7 | Doodle Jump류 버티컬 점퍼 | 캐주얼 | Jumper Pack (발판+적+아이템+패럴랙스 배경 4겹) | ★★ |
| 8 | 사격장 (오리 쏘기/과녁) | 클릭/탭 슈팅 | Shooting Gallery (표적+부스+크로스헤어+점수 HUD) | ★★★ READY/GAME OVER 문구 포함 |
| 9 | 매치-3 퍼즐 | Bejeweled/Candy Crush류 | Puzzle Pack 1 (6색 젬 + 9슬라이스 버튼) | ★★ |
| 10 | 벽돌깨기 (Breakout/Arkanoid) | 아케이드 퍼즐 | Puzzle Pack 2 (공+패들+8색 벽돌) | ★★ |
| 11 | 파이프 연결 퍼즐 | 퍼즐 | Puzzle Pack 2 (직선/코너/T/십자 파이프 풀세트) | ★★ |
| 12 | 테트리스류 낙하 블록 퍼즐 | 퍼즐 | Puzzle Pack 2 (8색 정사각 블록 타일 → 7종 테트로미노 색 구분, Back tiles 보드, 색 파티클 라인 클리어) | ★★ 뿌요뿌요·컬럼스·Dr.Mario 계열 공통 |
| 13 | 물리 파괴 퍼즐 (Angry Birds류) | 물리 퍼즐 | Physics Assets (5재질 블록+캐릭터+잔해) | ★★ 재질별 내구도 |

## 2. 보드게임 (1~2폴더)

표준 52장 카드 + 주사위 + 칩 + 7색 말, 액션 픽토그램 아이콘이 짝으로 있다.

| # | 프로토타입 | 핵심 에셋 |
|---|-----------|----------|
| 14 | 솔리테어 / 블랙잭 / 포커 | Boardgame Pack (표준 트럼프 완비 + 카드 사운드) |
| 15 | 주사위 확률 게임 (윷놀이/야찌) | Boardgame Pack (주사위 24종 + dieThrow 사운드) |
| 16 | 모노폴리/부루마블류 | Boardgame Pack(말 7색) + Map Pack(격자 맵) + Board Game Icons(액션 표기) |

## 3. 조합형 (의도적으로 여러 폴더를 합쳐 새 게임)

| # | 프로토타입 | 장르 | 조합 (폴더) | 조합으로 생기는 새 요소 |
|---|-----------|------|-------------|----------------------|
| 17 | 탑다운 RPG / 어드벤처 | RPG | Map Pack + Spine 캐릭터 + UI Pack RPG | 바이옴 맵 + 애니 캐릭터 + 체력/마나 바 |
| 18 | 횡스크롤 액션 플랫포머 | 액션 | Spine `hero/spineboy` + Map Pack + Pixel UI | 점프·사격 풀세트 캐릭터를 타일맵 위에 |
| 19 | 우주선 조립 + 슈팅 | 슈먹업 | Space Remastered + Space Extension | 모듈 부품으로 함선 커스텀 후 전투 |
| 20 | 영웅 디펜스 (히어로 TD) | TD+액션 | Tower Defense + Top-Down Shooter | 타워 설치 + 영웅을 직접 조종해 방어 |
| 21 | 탱크 웨이브 디펜스 | TD | Tower Defense + Top-Down Tanks + Map Pack | 진격하는 적 탱크를 타워로 막기 |
| 22 | 파괴되는 엄폐물 탱크전 | 아레나 | Top-Down Tanks + Physics Assets | 물리로 부서지는 나무/돌/유리 엄폐물 |
| 23 | 우주 타워디펜스 | TD | Tower Defense + Space Shooter | 적이 우주선, 발사체가 레이저인 우주 TD |
| 24 | 카드 배틀러 / 덱빌딩 | 카드RPG | Boardgame + Board Game Icons + UI Pack RPG | 카드 + 스킬 아이콘 + 체력/마나 (슬더스류) |
| 25 | 카드 던전 크롤러 | 로그라이크 | Boardgame + Map Pack + Spine | 카드로 싸우며 던전 타일 탐험 |
| 26 | 퍼즐 RPG (Puzzle Quest류) | 매치+RPG | Puzzle Pack 1 + Spine + UI Pack RPG | 매치-3으로 적과 턴제 전투 |
| 27 | 점프 + 슈팅 하이브리드 | 캐주얼 | Jumper Pack + Space Shooter | 발판 밟고 오르며 레이저로 적 처치 |
| 28 | 횡스크롤 슈팅 러너 | 러너+슈팅 | Tappy Plane + Space Shooter | 비행기가 레이저 쏘며 장애물 회피 |
| 29 | 파워업 강화 벽돌깨기 | 아케이드 | Puzzle Pack 2 + Space Shooter(파워업) | 방패·멀티볼 등 강화 아이템 벽돌깨기 |
| 30 | 소행성 파괴 물리게임 | 아케이드 | Space Shooter(운석) + Physics Assets | 운석을 물리 충돌로 쪼개기(Asteroids 물리판) |
| 31 | 공성전 (투석 vs 방어) | 물리+TD | Physics Assets + Tower Defense | 적은 물리로 성 부수기 / 플레이어는 타워 방어 |
| 32 | 좀비 사격 갤러리 | 라이트건 | Shooting Gallery + Top-Down Shooter | 크로스헤어·점수 UI로 좀비 표적 사격 |
| 33 | 탑다운 레이싱 | 레이싱 | Top-Down Tanks(차체) + Map Pack(도로/트랙) | 탱크 차체를 차량 삼아 트랙 주행 |
| 34 | 사격 + 카드 보상 아케이드 | 아케이드 | Shooting Gallery + Boardgame Pack | 표적 맞히면 카드/칩 보상 뽑기 |

> 거의 모든 조합에 **Input Prompts**(조작 안내)와 **Pixel UI / UI Pack RPG**(HUD)를 얹을 수 있다.
> 이들은 "재료" 레이어라 별도 게임으로 세지 않고 위 어디에나 부착되는 공통 요소다.

## 4. 고전게임

현재 에셋으로 만들 수 있는 고전/레트로 게임. 적합도는 **◎ 전용 에셋 있음 / ○ 무난**.
(전용 캐릭터 부족 등으로 improvise가 필요한 항목은 제외함 — 예: Pac-Man, Centipede.)

### 4-1. 아케이드

| # | 게임 | 핵심 에셋 | 적합도 |
|---|------|----------|:---:|
| 35 | Pong / 에어하키 | Puzzle Pack 2 (패들 + 공) | ◎ |
| 36 | Space Invaders | Space Shooter Remastered (적 20종 포메이션 + 함선 + 레이저 48) | ◎ |
| 37 | Snake (지렁이) | 타일(Map/Puzzle) 몸통 + 코인/아이템(사과) | ○ |
| 38 | 두더지잡기 (Whack-a-Mole) | Shooting Gallery (튀어나오는 표적·오리 + 크로스헤어 + 점수) | ◎ |
| 39 | Frogger (개구리 길건너기) | Map Pack(도로) + Top-Down Tanks(차량 장애물) + 캐릭터 | ○ |
| 40 | Bomberman | Map Pack(격자) + Physics(파괴블록) + 폭발(Tanks/Space) + 캐릭터 | ○ |
| 41 | Missile Command | Space Shooter(미사일) + Shooting Gallery(조준) + Space Ext(건물) | ○ |
| 42 | Lunar Lander (달 착륙) | Space Shooter(함선+화염) + 2D 물리(중력/추진) | ○ |
| 43 | Battle City / 탱크 1990 | Top-Down Tanks + Physics/Map(벽돌벽) + 본진 방어 | ◎ |

### 4-2. 퍼즐

| # | 게임 | 핵심 에셋 | 적합도 |
|---|------|----------|:---:|
| 44 | 지뢰찾기 (Minesweeper) | 격자 타일 + 비트맵 숫자(Desert/Space 폰트) + 깃발 아이콘(Board Game Icons) | ○ |
| 45 | 2048 | 숫자 타일 = 비트맵 숫자 + UI 패널 | ○ |
| 46 | 창고지기 (Sokoban) | Map Pack(격자) + 상자(Desert/Physics) + 캐릭터 | ○ |
| 47 | Simon / Mastermind (색 기억·추리) | Puzzle Pack 색 버튼·젬, 칩 | ◎ |
| 48 | 행맨 / 단어 퍼즐 | 비트맵 알파벳 A~Z (Tappy Plane / Desert Shooter) | ◎ |

### 4-3. 테이블·2인 (Boardgame Pack 계열)

| # | 게임 | 핵심 에셋 | 적합도 |
|---|------|----------|:---:|
| 49 | 카드 짝맞추기 (Concentration/Memory) | Boardgame Pack (카드 + 뒷면) | ◎ |
| 50 | 오목 / 틱택토 / Connect Four | Boardgame 칩·말(7색) + 격자 | ◎ |
| 51 | 오델로 / 리버시 | Boardgame 칩 (2색) | ◎ |
| 52 | 체커 / 체스 | Board Game Icons(체스 기물) + Boardgame 말 + 보드 | ○ |
| 53 | 뱀과 사다리 (Snakes & Ladders) | Boardgame(주사위·말) + Map Pack(보드 경로) | ◎ |
| 54 | 윷놀이 / 사다리타기 | Boardgame(주사위·말) + Board Game Icons | ◎ |

> **마작(Mahjong solitaire)** 은 전용 마작패 에셋이 없어 현재 팩으론 어렵다(보드게임 카드로 변형은 가능).

## 5. 신규 에셋(2차 추가) 기반 주제

전용 단일팩(Sokoban·Rolling Ball·Racing·Micro Roguelike·Letter Tiles·Tanks·Playing Cards)과
캐릭터/타일/지원 에셋(Toon Characters·Roguelike Caves·Emotes·Generic Items·Particle·Splat·Foliage·Minimap·UI Pack)이
추가되며 새로 가능해진 주제다. 적합도 **◎ 전용 에셋 있음 / ○ 무난**.

### 5-1. 단일 에셋 팩

| # | 프로토타입 | 장르 | 핵심 에셋 | 비고 |
|---|-----------|------|----------|------|
| 55 | 소코반 (상자 밀기) | 격자 퍼즐 | Sokoban Pack (플레이어 4방향 + 색상 상자/목표 + 타일) ◎ | 고전 #46을 전용팩으로 업그레이드 |
| 56 | 볼 굴리기 / 틸트 미로 | 물리 퍼즐 | Rolling Ball Assets (공+잠금블록+열쇠+목표구멍+숫자UI) ◎ | Marble Madness류 신규 장르 |
| 57 | 탑다운 레이싱 | 레이싱 | Racing Pack (5색 차량 + 3노면 코너/교차 트랙 + 장애물) ◎ | 고전계열 #33 조합을 전용팩으로 업그레이드 |
| 58 | 미니멀 로그라이크 / 던전 크롤러 | 로그라이크 | Micro Roguelike (영웅+몬스터+아이템+던전 타일 올인원) ◎ | 1비트 톤은 Monochrome |
| 59 | 스크래블 / 단어 맞추기 | 단어 퍼즐 | Letter Tiles (A~Z 점수 타일 + 빈/와일드 타일) ◎ | 워들·스크래블·행맨 (고전 #48 강화) |
| 60 | 탱크 배틀 (지뢰·보급) | 탑다운 슈터 | Tanks (차체+포신 분리, 지뢰 Off/On, 보급상자 4종) ◎ | 지뢰 트랩·보급 픽업 메커닉 |

### 5-2. 보드게임 (Playing Cards Pack 전용 카드)

| # | 프로토타입 | 핵심 에셋 |
|---|-----------|----------|
| 61 | 솔리테어 변형 (클론다이크/프리셀/스파이더) | Playing Cards Pack (52장 large/medium/small + 뒷면/빈카드) |
| 62 | 트릭테이킹 / 원카드 (하트·스페이드·우노류) | Playing Cards Pack (수트/랭크 규칙적 로딩) |

> 카드만 깔끔히 필요할 때는 Boardgame Pack보다 **Playing Cards Pack**(트럼프 표준 특화, 3해상도)이 적합.

### 5-3. 조합형

| # | 프로토타입 | 장르 | 조합 (폴더) | 조합으로 생기는 새 요소 |
|---|-----------|------|-------------|----------------------|
| 63 | 픽셀 로그라이크 던전 크롤러 | 로그라이크 | Micro Roguelike + Roguelike Caves & Dungeons + Minimap Pack | 호환 타일 던전 + 미니맵 HUD |
| 64 | 아이템 수집 던전 RPG | RPG | Roguelike Caves & Dungeons + Generic Items + UI Pack | 인벤토리·줍기·제작 |
| 65 | 카툰 액션 플랫포머 | 액션 | Toon Characters + Map Pack + UI Pack | Spine 없이 카툰 주인공 (포즈 스왑/본 리깅 학습) |
| 66 | 스텔스 잠입 | 스텔스 | Toon/Top-Down 캐릭터 + Emotes Pack + Map Pack | `!`/`?` 감지 상태 메커닉 |
| 67 | 펫 / 라이프 시뮬 | 시뮬 | Toon Characters + Emotes Pack + Generic Items | 기분·욕구 아이콘 + 도구/가전 |
| 68 | 스플래툰류 페인트 슈터 | 슈터 | Top-Down Shooter + Splat Pack + Particle Pack | 페인트 데칼로 영역 점령 |
| 69 | 채집·제작 생존 | 서바이벌 | Toon Characters + Foliage Sprites + Generic Items + Map Pack | 채집(식생) + 제작(아이템) |
| 70 | 연출 강화 레이싱 | 레이싱 | Racing Pack + Particle Pack + UI Pack | 먼지·스파크 VFX + 랩타임 HUD |
| 71 | PvE 탱크 캠페인 | 슈터 | Tanks + Roguelike/Map 타일 + Generic Items | 지뢰·보급 기반 미션 진행 |

---

## 부가 활용 에셋

게임 자체보단 위 프로토타입에 얹는 용도.

- **Input Prompts** — 조작 안내 UI, 튜토리얼, 키 리바인딩, 패드/키보드 자동 전환
- **UI Pack (v2)** — 폰트·사운드 포함 범용 UI 키트. 장르 불문 **1순위 UI**로 권장
- **UI Pack RPG Expansion / Pixel UI** — 체력바·패널·9슬라이스 창 (톤에 맞춰 선택)
- **Board Game Icons** — 룰북/액션 버튼 픽토그램 (보드게임 UI에 짝)
- **Generic Items** — 인벤토리/상점/퀵슬롯 아이템 아이콘 (163종, White는 틴팅)
- **Minimap Pack** — 미니맵/맵 오버뷰 HUD, 마커(시작·목표·적·계단) (6스타일)
- **Emotes Pack** — 캐릭터 머리 위 감정·감지 상태(하트/`!`/`?`/zZ) (픽셀·벡터)
- **Particle Pack** — VFX 파티클 텍스처(연기·불·마법·번개), Unity 샘플 포함
- **Splat Pack** — 페인트/피/슬라임 데칼·타격 얼룩 (틴팅)
- **Foliage Sprites** — 자연 배경 식생(잔디·풀·잎), Flat은 틴팅
- **Toon Characters** — 카툰 캐릭터 6종 소스(포즈 45종 / 분리 파츠 본 리깅) — Spine 대체 캐릭터로 활용
- **Spine 물리 데모**(`cloud-pot`, `snowglobe`, `Raggedy Spineboy`) — 물리/IK 표현 학습용
- **Spine 스킨 데모**(`mix-and-match-pro`, `goblins`) — 런타임 장비/의상 교체 시스템

## 스크립팅 메모 (PuerTS / TypeScript)

[`scripting-puerts-typescript.md`](../UnityLab/ai-docs/scripting-puerts-typescript.md) 기준으로
PuerTS(v3.0.2 + V8)가 셋업돼 있어, 위 게임 로직을 C# 대신 **TypeScript**로 작성해볼 수 있다.
해당 문서의 "다음 단계" 목표(간단한 게임 로직을 TS로 작성해 C# 호스트와 연결 — 데미지 계산/상태머신)와
묶어, 위 프로토타입 중 하나(예: 탱크 배틀의 데미지 계산, 매치-3의 보드 상태머신)를
TS 스크립팅 검증 과제로 활용하기 좋다.
