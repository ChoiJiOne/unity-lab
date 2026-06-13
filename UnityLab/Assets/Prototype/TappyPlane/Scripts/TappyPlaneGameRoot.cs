using UnityEngine;

namespace Prototype.TappyPlane
{
    /// <summary>
    /// Tappy Plane 프로토타입 엔트리 포인트(부트스트랩).
    /// 씬에 단 1개 배치되어 게임 루트 흐름(상태 머신·시스템 초기화)을 소유한다.
    /// M0(스캐폴딩) 단계에서는 부트 로그만 찍어 빈 씬 Play 가 도는지 확인한다.
    /// </summary>
    public sealed class TappyPlaneGameRoot : MonoBehaviour
    {
        private enum State
        {
            Ready,
            Playing,
            GameOver,
        }

        private State _state = State.Ready;

        private void Awake()
        {
            Debug.Log("[TappyPlane] GameRoot 부트 — 상태 머신 초기화 (state=" + _state + ")");
        }
    }
}
