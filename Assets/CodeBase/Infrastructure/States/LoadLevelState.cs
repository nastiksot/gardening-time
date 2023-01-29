using CodeBase.PersistentProgress.Services;
using CodeBase.Room;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        readonly GameStateMachine m_StateMachine;
        readonly SceneLoader m_SceneLoader;
        readonly LoadingCanvas m_LoadingCanvas;
        readonly IGameFactory m_GameFactory;
        readonly IPersistentProgressService m_ProgressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCanvas loadingCanvas,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            m_StateMachine = stateMachine;
            m_SceneLoader = sceneLoader;
            m_LoadingCanvas = loadingCanvas;
            m_GameFactory = gameFactory;
            m_ProgressService = progressService;
        }

        public void Enter(string payload)
        {
            m_LoadingCanvas.Show();
            m_GameFactory.Cleanup();
            m_SceneLoader.Load(payload, OnLoadedScene);
        }

        void OnLoadedScene()
        {
            InitGameWorld();
            InformProgressReaders();
            m_StateMachine.Enter<GameLoopState>();
        }

        void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in m_GameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(m_ProgressService.Progress);
            }
        }

        void InitGameWorld()
        {
            m_GameFactory.InstantiateHUD();
            var mugPlace = GameObject.FindGameObjectsWithTag("MugPlace");
            foreach (GameObject gameObject in mugPlace)
            {
                m_GameFactory.Register(gameObject.GetComponent<MugPlaceholder>());
            }
        }

        public void Exit()
        {
            m_LoadingCanvas.Hide();
        }
    }
}
