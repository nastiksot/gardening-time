using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup loadingCanvas;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            loadingCanvas.alpha = 1;
        }

        public void Hide()
        {
            FadeIn();
        }

        async void FadeIn()
        {
            while (loadingCanvas.alpha > 0)
            {
                loadingCanvas.alpha -= 0.03f;
                await Task.Delay(3);
            }

            gameObject.SetActive(false);
        }
    }
}