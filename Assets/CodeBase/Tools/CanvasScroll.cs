using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum ScrollSide
{
    None = -1,
    Left = 0,
    Right = 1
}
public abstract class CanvasScroll<T> : MonoBehaviour where T : Behaviour
{
    [SerializeField] protected float animationTime = 1f;

    [SerializeField] private protected List<T> scrollContentList = new List<T>();

    int _currentPageIndex;

    void ScrollAnimation(List<T> content, float endXPosition, float duration)
    {
        for (var i = 0; i < content.Count; i++)
        {
            content[i].transform.DOBlendableLocalMoveBy(new Vector3(endXPosition, 0, 0), duration);
        }
    }
    
    private protected virtual void ScrollToPage(ScrollSide scrollSide, RectTransform rectTransform)
    {
        T previousPage = _currentPageIndex - 1 >= 0 ? scrollContentList[_currentPageIndex - 1] : null;
        T nextPage = _currentPageIndex + 1 < scrollContentList.Count
            ? scrollContentList[_currentPageIndex + 1]
            : null;
        float rectWidth = rectTransform.rect.width;
        switch (scrollSide)
        {
            case ScrollSide.Left:
                if (previousPage == null) return;
                _currentPageIndex--;
                ScrollAnimation(scrollContentList, rectWidth, animationTime);
                break;
            case ScrollSide.Right:
                if (nextPage == null) return;
                _currentPageIndex++;
                ScrollAnimation(scrollContentList, -rectWidth, animationTime);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}