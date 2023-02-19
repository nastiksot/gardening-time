using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public enum ScrollSide
{
    None = -1,
    Left = 0,
    Right = 1
}

public abstract class CanvasScroll<T> : MonoBehaviour where T : Behaviour
{
    [SerializeField]
    protected float animationTime = 1f;

    [SerializeField]
    private protected List<T> scrollContentList = new List<T>();

    [FormerlySerializedAs("m_CurrentPageIndex")]
    public int currentPageIndex;

    void ScrollAnimation(List<T> content, float endXPosition, float duration)
    {
        for (var i = 0; i < content.Count; i++)
        {
            content[i].transform.DOBlendableLocalMoveBy(new Vector3(endXPosition, 0, 0), duration);
        }
    }

    private protected virtual void ScrollToPage(ScrollSide scrollSide, RectTransform rectTransform)
    {
        T previousPage = currentPageIndex - 1 >= 0 ? scrollContentList[currentPageIndex - 1] : null;
        T nextPage = currentPageIndex + 1 < scrollContentList.Count
            ? scrollContentList[currentPageIndex + 1]
            : null;
        float rectWidth = rectTransform.rect.width;
        switch (scrollSide)
        {
            case ScrollSide.Left:
                if (previousPage == null) return;
                currentPageIndex--;
                ScrollAnimation(scrollContentList, rectWidth, animationTime);
                break;
            case ScrollSide.Right:
                if (nextPage == null) return;
                currentPageIndex++;
                ScrollAnimation(scrollContentList, -rectWidth, animationTime);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
