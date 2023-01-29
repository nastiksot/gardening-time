using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.PersistentProgress;
using CodeBase.PersistentProgress.Services;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure
{
    public class BootStrapState : IState
    {
        private const string Landing = "Landing";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Landing, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());

            _serviceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(_serviceLocator.Single<IAssetProvider>()));

            _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _serviceLocator.Single<IPersistentProgressService>(), _serviceLocator.Single<IGameFactory>()));
        }
    }
}