using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper GameBootstrapperPrefab;

        private void Awake()
        {
            if (!FindObjectOfType<GameBootstrapper>())
            {
                Instantiate(GameBootstrapperPrefab);
            }
        }
    }

    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCanvas loadingCanvas)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCanvas, ServiceLocator.Container);
        }
    }
}