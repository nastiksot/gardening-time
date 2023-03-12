using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Room
{
    public class Plant : MonoBehaviour
    {
        [SerializeField]
        Image image;
        [SerializeField]
        ResourceBar resourceBar;

        float m_TimePerFrame;
        int m_SpriteLenght;
        int m_GrownIteration;

        PlantConfig m_PlantConfig;
        IStaticDataService m_StaticDataService;

        public Sprite[] PlantSprites => m_PlantConfig.sprites;
        public PlantType PlantType => m_PlantConfig.type;

        void Awake()
        {
            m_StaticDataService = ServiceLocator.Container.Single<IStaticDataService>();
        }

        public void Initialize(PlantType plantType)
        {
            m_PlantConfig = m_StaticDataService.ForPlant(plantType);

            m_SpriteLenght = m_PlantConfig.sprites.Length - 3;
            m_TimePerFrame = m_PlantConfig.grownTime / m_SpriteLenght;

            StartCoroutine(GrowAndFlower());
        }

        IEnumerator GrowAndFlower()
        {
            yield return StartTimer(m_PlantConfig.grownTime, OnGrowStart, null, OnGrowingTick);
            yield return StartTimer(m_PlantConfig.floweringTime, null, OnFloweringStop);
        }

        void OnFloweringStop()
        {
            resourceBar.SetVisibility(false);
            NextGrownIteration();
        }

        void NextGrownIteration()
        {
            m_GrownIteration++;
            image.sprite = PlantSprites[m_GrownIteration];
        }

        void OnGrowingTick(float timeSpent)
        {
            resourceBar.SetValue(timeSpent / m_PlantConfig.grownTime);

            if (timeSpent > m_TimePerFrame * m_GrownIteration)
            {
                NextGrownIteration();
            }
        }

        void OnGrowStart()
        {
            m_GrownIteration = 1;
            image.sprite = PlantSprites[m_GrownIteration];
        }

        IEnumerator StartTimer(float duration, Action onStart = null, Action onEnd = null, Action<float> onTick = null)
        {
            onStart?.Invoke();
            var timeSpent = 0f;
            while (timeSpent < duration)
            {
                yield return null;
                timeSpent += Time.deltaTime;
                onTick?.Invoke(timeSpent);
            }

            onEnd?.Invoke();
        }
    }
}
