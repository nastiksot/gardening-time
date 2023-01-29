using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        readonly ICoroutineRunner m_CoroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            m_CoroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onComplete = null)
        {
            m_CoroutineRunner.StartCoroutine(LoadSceneRoutine(name, onComplete));
        }

        IEnumerator LoadSceneRoutine(string name, Action onComplete = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onComplete?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}