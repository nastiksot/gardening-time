using System.Linq;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace CodeBase.HUD.InteractiveUI
{
    public class PictureUI : MonoBehaviour
    {
        [SerializeField]
        Image image;
        IStaticDataService m_StaticDataService;
        readonly Random m_Random = new Random();

        void Awake()
        {
            m_StaticDataService = ServiceLocator.Container.Single<IStaticDataService>();
            DropRandomItem();
        }

        public void DropRandomItem()
        {
            PictureConfig[] configs = m_StaticDataService.PictureConfigs;
            int totalRarity = m_StaticDataService.PictureConfigs.Sum(dropItem => dropItem.rarity);
            int randomValue = m_Random.Next(0, totalRarity);

            foreach (PictureConfig pictureConfig in configs)
            {
                randomValue -= pictureConfig.rarity;
                if (randomValue <= 0)
                {
                    image.sprite = pictureConfig.pictureSprite;
                    break;
                }
            }
        }
    }
}
