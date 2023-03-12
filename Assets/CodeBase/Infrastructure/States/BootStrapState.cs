using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Event;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.PersistentProgress.Services;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;

namespace CodeBase.Infrastructure
{
    public class BootStrapState : IState
    {
        const string k_Landing = "Landing";
        readonly GameStateMachine m_StateMachine;
        readonly SceneLoader m_SceneLoader;
        readonly ServiceLocator m_ServiceLocator;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            m_StateMachine = stateMachine;
            m_SceneLoader = sceneLoader;
            m_ServiceLocator = serviceLocator;
            RegisterServices();
        }

        public void Enter()
        {
            m_SceneLoader.Load(k_Landing, EnterLoadLevel);
        }

        public void Exit() { }

        void EnterLoadLevel()
        {
            m_StateMachine.Enter<LoadProgressState>();
        }

        void RegisterServices()
        {
            RegisterStaticDataService();

            m_ServiceLocator.RegisterSingle<IEventsService>(new EventsService());

            m_ServiceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());

            m_ServiceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            m_ServiceLocator.RegisterSingle<IGameFactory>(new GameFactory(m_ServiceLocator.Single<IAssetProvider>(),
                m_ServiceLocator.Single<IStaticDataService>()));

            m_ServiceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                m_ServiceLocator.Single<IPersistentProgressService>(), m_ServiceLocator.Single<IGameFactory>()));
        }

        void RegisterStaticDataService()
        {
            var staticDataService = new StaticDataService();
            staticDataService.LoadAll();
            m_ServiceLocator.RegisterSingle<IStaticDataService>(staticDataService);
        }
    }
}
