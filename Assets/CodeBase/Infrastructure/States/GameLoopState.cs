
namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        readonly GameStateMachine m_GameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            m_GameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}