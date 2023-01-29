using CodeBase.Data;
using CodeBase.PersistentProgress.Services;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCanvas _loadingCanvas;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCanvas loadingCanvas,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCanvas = loadingCanvas;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            _loadingCanvas.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(payload, OnLoadedScene);
        }

        private void OnLoadedScene()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        { 
            _gameFactory.InstantiateHUD(); 
        }

        public void Exit()
        {
            _loadingCanvas.Hide();
        }
    }
}