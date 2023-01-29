using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper GameBootstrapperPrefab;

        private void Awake()
        {
            if (!FindObjectOfType<GameBootstrapper>())
            {
                Instantiate(GameBootstrapperPrefab);
            }
        }
    }
}