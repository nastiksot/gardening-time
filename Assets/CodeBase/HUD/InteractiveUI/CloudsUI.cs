using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.HUD.InteractiveUI
{
    public class CloudsUI : MonoBehaviour
    {
        const float k_InitialPosition = -300;
        const float k_EndPosition = 400;
        const float k_TimeToReachEnd = 6;
        
        float m_CurrentPosition;

        void Start()
        {
            MoveClouds();
        }

        void MoveClouds()
        {
            m_CurrentPosition = transform.localPosition.x;
            float distance = math.abs(m_CurrentPosition - k_EndPosition);

            float speed = distance / k_TimeToReachEnd;

            transform.DOLocalMoveX(k_EndPosition, speed).OnComplete(ResetPosition);
        }

        void ResetPosition()
        {
            Vector3 transformLocalPosition = transform.localPosition;
            transformLocalPosition.x = k_InitialPosition;
            transform.localPosition = transformLocalPosition;
            MoveClouds();
        }
    }
}
