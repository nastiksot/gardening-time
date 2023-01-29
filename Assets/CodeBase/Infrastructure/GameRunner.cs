using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper gameBootstrapperPrefab;

        void Awake()
        {
            if (!FindObjectOfType<GameBootstrapper>())
            {
                Instantiate(gameBootstrapperPrefab);
            }
        }
    }
}