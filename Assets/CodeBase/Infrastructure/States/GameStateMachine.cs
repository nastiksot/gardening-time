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
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCanvas loadingCanvas, ServiceLocator serviceLocator)
        {
            _states = new Dictionary<Type, IExitableState>
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

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;

            return state;
        }
    }
}