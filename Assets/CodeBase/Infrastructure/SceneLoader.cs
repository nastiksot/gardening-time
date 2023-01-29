using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onComplete = null)
        {
            _coroutineRunner.StartCoroutine(LoadSceneRoutine(name, onComplete));
        }

        private IEnumerator LoadSceneRoutine(string name, Action onComplete = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onComplete?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(name);
            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}