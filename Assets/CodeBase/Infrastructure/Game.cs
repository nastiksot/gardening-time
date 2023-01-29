using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCanvas loadingCanvas)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCanvas, ServiceLocator.Container);
        }
    }
}