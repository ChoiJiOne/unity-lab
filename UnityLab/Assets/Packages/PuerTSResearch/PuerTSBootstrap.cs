using System;
using Puerts;
using UnityEngine;

/// <summary>
/// 주제 37 (PuerTS) 리서치용 최소 부트스트랩.
/// JsEnv 1개를 만들어 Resources 의 main.mjs 모듈을 실행하고,
/// TS→C# (모듈 로드 시 Debug.Log) 와 C#→TS (greet 호출) 양방향을 확인한다.
///
/// 빈 GameObject 에 이 컴포넌트를 붙이고 Play 하면 콘솔에 로그가 찍힌다.
/// </summary>
public class PuerTSBootstrap : MonoBehaviour
{
    private JsEnv _env;

    private void Start()
    {
        // 백엔드(V8)는 BackendType.Auto 로 자동 선택. 로더는 DefaultLoader → Resources 에서 .mjs 로드.
        _env = new JsEnv();

        // main.mjs 모듈 실행 (TS 컴파일 결과). 로드 시점에 TS 코드가 C# 를 호출한다(TS→C#).
        ScriptObject main = _env.ExecuteModule("main.mjs");

        // 모듈이 export 한 greet 함수를 C# 에서 호출한다(C#→TS).
        var greet = main.Get<Action<string>>("greet");
        greet?.Invoke("Unity 6");
    }

    private void Update()
    {
        // V8 의 마이크로태스크/타이머 처리를 위해 매 프레임 Tick.
        _env?.Tick();
    }

    private void OnDestroy()
    {
        _env?.Dispose();
        _env = null;
    }
}
