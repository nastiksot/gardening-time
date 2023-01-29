using System.Collections.Generic;

namespace CodeBase.Services.SaveLoad
{
    public interface IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        public void InstantiateHUD();
        public void Cleanup();
    }
}