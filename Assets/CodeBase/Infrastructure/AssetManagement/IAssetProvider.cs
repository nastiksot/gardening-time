using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string prefabPath, Vector3 position);
        GameObject Instantiate(string prefabPath);
    }
}