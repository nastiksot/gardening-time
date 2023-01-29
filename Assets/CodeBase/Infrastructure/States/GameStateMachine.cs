using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.PersistentProgress.Services;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        readonly Dictionary<Type, IExitableState> m_States;
        IExitableState m_CurrentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCanvas loadingCanvas, ServiceLocator serviceLocator)
        {
            m_States = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader, serviceLocator),

                [typeof(LoadLevelState)] =
                    new LoadLevelState(this, sceneLoader, loadingCanvas, serviceLocator.Single<IGameFactory>(),
                        serviceLocator.Single<IPersistentProgressService>()),

                [typeof(LoadProgressState)] =
                    new LoadProgressState(this, serviceLocator.Single<IPersistentProgressService>(),
                        serviceLocator.Single<ISaveLoadService>()),

                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Exit()
        {
        }

        TState GetState<TState>() where TState : class, IExitableState
        {
            return m_States[typeof(TState)] as TState;
        }

        TState ChangeState<TState>() where TState : class, IExitableState
        {
            m_CurrentState?.Exit();

            var state = GetState<TState>();
            m_CurrentState = state;

            return state;
        }
    }
}