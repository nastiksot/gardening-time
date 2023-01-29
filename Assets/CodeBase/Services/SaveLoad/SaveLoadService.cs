using CodeBase.Data;
using CodeBase.PersistentProgress.Services;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService  : ISaveLoadService
    {
        readonly IPersistentProgressService m_ProgressService;
        readonly IGameFactory m_GameFactory;
        const string k_ProgressKey = "Progress";

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            m_ProgressService = progressService;
            m_GameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriters in m_GameFactory.ProgressWriters)
            {
                progressWriters.UpdateProgress(m_ProgressService.Progress);
            }

            PlayerPrefs.SetString(k_ProgressKey, m_ProgressService.Progress.Serialize());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(k_ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}