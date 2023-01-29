using System;
using System.Collections;
using CodeBase.Tools;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasDisabler : BaseObjectDisabler
{
    [SerializeField]
    CanvasGroup canvasGroup;

    Action<float> onAlphaChange;
    Action<bool> onInteractableChange;

    public CanvasGroup CanvasGroup => canvasGroup;

    public event Action<bool> OnInteractableChange
    {
        add => onInteractableChange += value;
        remove => onInteractableChange -= value;
    }

    public event Action<float> OnAlphaChange
    {
        add => onAlphaChange += value;
        remove => onAlphaChange -= value;
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
        onAlphaChange?.Invoke(canvasGroup.alpha);
        onInteractableChange?.Invoke(state);
    }

    public override IEnumerator DisplayObject(bool isVisible, float delay, Action<BaseObjectDisabler> action = null)
    {
        onAlphaChange?.Invoke(isVisible ? 1 : 0);
        onInteractableChange?.Invoke(isVisible);
        yield return UITool.State(canvasGroup, isVisible, delay, group => action?.Invoke(this));
    }
}