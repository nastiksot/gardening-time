using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.HUD.InteractiveUI
{
    public class CloudsUI : MonoBehaviour
    {
        const float k_InitialPosition = -500;
        const float k_EndPosition = 500;
        const float k_DelayDuration = 3;
        const float k_MaxMoveDuration = 90;
        const float k_MinMoveDuration = 50;

        void Start()
        {
            MoveClouds();
        }

        void MoveClouds()
        {
            transform.DOLocalMoveX(k_EndPosition, UnityEngine.Random.Range(k_MinMoveDuration, k_MaxMoveDuration)).OnComplete(ResetPosition);
        }

        async void ResetPosition()
        {
            Vector3 transformLocalPosition = transform.localPosition;
            transformLocalPosition.x = k_InitialPosition;
            transform.localPosition = transformLocalPosition;
            await Task.Delay((int)(k_DelayDuration * 1000 * Time.timeScale));
            MoveClouds();
        }
    }
}
