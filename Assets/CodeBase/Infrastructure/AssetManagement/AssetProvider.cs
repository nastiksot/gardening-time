using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string prefabPath, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(prefabPath);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public GameObject Instantiate(string prefabPath)
        {
            var prefab = Resources.Load<GameObject>(prefabPath);
            return Object.Instantiate(prefab);
        }
    }
}