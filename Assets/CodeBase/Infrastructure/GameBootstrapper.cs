using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCanvas loadingCanvasPrefab;
        Game m_Game;

        void Awake()
        {
            LoadingCanvas loadingCanvas = Instantiate(loadingCanvasPrefab);
            m_Game = new Game(this, loadingCanvas);
            m_Game.StateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}