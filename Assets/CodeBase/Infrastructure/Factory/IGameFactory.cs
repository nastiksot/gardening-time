using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public interface IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        public void InstantiateHUD();
        public GameObject InstantiatePlant(PlantType type, Transform parent);
        public void Cleanup();
        public void Register(ISavedProgressReader progressReader);

    }
}