using System;
using System.Collections;
using CodeBase.Tools;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasDisabler : BaseObjectDisabler
{
    [SerializeField]
    CanvasGroup canvasGroup;

    Action<float> m_OnAlphaChange;
    Action<bool> m_OnInteractableChange;

    public CanvasGroup CanvasGroup => canvasGroup;

    public event Action<bool> OnInteractableChange
    {
        add => m_OnInteractableChange += value;
        remove => m_OnInteractableChange -= value;
    }

    public event Action<float> OnAlphaChange
    {
        add => m_OnAlphaChange += value;
        remove => m_OnAlphaChange -= value;
    }

    public float Alpha
    {
        get => canvasGroup.alpha;
    }

    public bool Interactable
    {
        get => canvasGroup.interactable;
    }

    void OnValidate()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public override void DisplayObject(bool state)
    {
        UITool.State(ref canvasGroup, state);
        m_OnAlphaChange?.Invoke(canvasGroup.alpha);
        m_OnInteractableChange?.Invoke(state);
    }

    public override IEnumerator DisplayObject(bool isVisible, float delay, Action<BaseObjectDisabler> action = null)
    {
        m_OnAlphaChange?.Invoke(isVisible ? 1 : 0);
        m_OnInteractableChange?.Invoke(isVisible);
        yield return UITool.State(canvasGroup, isVisible, delay, group => action?.Invoke(this));
    }
}