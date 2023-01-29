using CodeBase.Data;
using CodeBase.PersistentProgress.Services;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService  : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriters in _gameFactory.ProgressWriters)
            {
                progressWriters.UpdateProgress(_progressService.Progress);
            }

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.Serialize());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}