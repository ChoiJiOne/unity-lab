// main.ts 를 컴파일한 결과(ESM). PuerTS DefaultLoader 가 Resources 에서 .mjs 로 로드한다.
// 'csharp' 는 PuerTS 내장 모듈 — C# 타입에 접근하는 진입점.
import { UnityEngine } from 'csharp';

// 모듈 로드 시점에 실행 → TS 에서 C# 호출 (TS→C#)
UnityEngine.Debug.Log('[PuerTS] main 모듈이 TypeScript 에서 로드됨');

// C# 에서 호출할 함수 export → C#→TS
export function greet(name) {
    UnityEngine.Debug.Log(`[PuerTS] 안녕하세요, ${name}! (C# 에서 호출됨)`);
}
