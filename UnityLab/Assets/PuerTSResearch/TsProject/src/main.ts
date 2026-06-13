// PuerTS 리서치 진입 모듈 (TypeScript 원본).
// tsc 또는 ts-loader 로 컴파일하면 ../../Resources/main.mjs 가 된다.
// 'csharp' 는 PuerTS 내장 모듈 — C# 타입을 타입 안전하게 호출하는 진입점.
import { UnityEngine } from 'csharp'

// 모듈 로드 시점에 실행 → TS 에서 C# 호출 (TS→C#)
UnityEngine.Debug.Log('[PuerTS] main 모듈이 TypeScript 에서 로드됨')

// C# 에서 호출할 함수 export → C#→TS
export function greet(name: string): void {
    UnityEngine.Debug.Log(`[PuerTS] 안녕하세요, ${name}! (C# 에서 호출됨)`)
}
