using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCanvas LoadingCanvasPrefab;
        private Game _game;

        private void Awake()
        {
            LoadingCanvas loadingCanvas = Instantiate(LoadingCanvasPrefab);
            _game = new Game(this, loadingCanvas);
            _game.StateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}