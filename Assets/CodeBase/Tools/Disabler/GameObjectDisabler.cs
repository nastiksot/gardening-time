using System;
using System.Collections;
using UnityEngine;

public class GameObjectDisabler : BaseObjectDisabler
{
    public override void DisplayObject(bool state)
    {
        if (gameObject.activeSelf == state) return;
        gameObject.SetActive(state);
    }

    public override IEnumerator DisplayObject(bool isVisible, float delay, Action<BaseObjectDisabler> action = null)
    {
        DisplayObject(isVisible);
        yield return new WaitForSeconds(delay);
        action?.Invoke(this);
    }
}