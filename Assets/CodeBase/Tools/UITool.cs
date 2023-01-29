using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Tools
{
    public class UITool
    {
        public static void State(ref CanvasGroup canvas, bool isVisible)
        {
            if (canvas.interactable == isVisible) return;
            canvas.alpha = isVisible ? 1 : 0;
            canvas.interactable = isVisible;
            canvas.blocksRaycasts = isVisible;
        }

        public static IEnumerator State(CanvasGroup canvas, bool isVisible, float delay,
            Action<CanvasGroup> action = null)
        {
            canvas.alpha = isVisible ? 1 : 0;
            canvas.blocksRaycasts = isVisible;
            yield return new WaitForSeconds(delay);
            canvas.interactable = isVisible;
            action?.Invoke(canvas);
        }

        public static void State(ref CanvasGroup canvas, bool isInteractable, float alpha)
        {
            canvas.alpha = alpha;
            canvas.interactable = isInteractable;
            canvas.blocksRaycasts = isInteractable;
        }


        public static Vector2 UIClickPosition(Transform transform, Vector3 worldPos)
        {
            Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, pos, Camera.main,
                out Vector3 rectanglePos);
            return rectanglePos;
        }

        public static IEnumerator DoFade(CanvasGroup canvas, float startValue,
            float endValue, float duration)
        {
            canvas.DOFade(startValue, 0.5f);
            yield return new WaitForSeconds(duration);
            canvas.DOFade(endValue, 1f);
        }
    }
}