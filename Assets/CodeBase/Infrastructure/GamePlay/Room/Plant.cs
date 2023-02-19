using System.Threading.Tasks;
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
        bool m_IsPlaying;

        PlantsConfig m_PlantsConfig;
        IStaticDataService m_StaticDataService;
        public Sprite[] PlantSprites => m_PlantsConfig.sprites;
        public PlantType PlantType => m_PlantsConfig.type;

        void Awake()
        {
            m_StaticDataService = ServiceLocator.Container.Single<IStaticDataService>();
        }

        void Update()
        {
            if (!m_IsPlaying)
            {
                UpdateSprites();
            }
        }

        async void UpdateSprites()
        {
            m_IsPlaying = true;
            for (var i = 1; i < PlantSprites.Length; i++)
            {
                image.sprite = PlantSprites[i];
                await Task.Delay((int)(1000 * Time.timeScale));
            }

            m_IsPlaying = false;
        }

        public void Initialize(PlantType plantType)
        {
            m_PlantsConfig = m_StaticDataService.ForPlant(plantType);
        }
    }
}
