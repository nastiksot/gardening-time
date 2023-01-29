using System;
using System.Linq;
using CodeBase.Tools;
using TMPro;
using UnityEngine;

[Serializable]
public class NamedDisabler : Named<string, BaseObjectDisabler>
{
}

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] protected NamedDisabler[] tableNamedDisabler;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private float delayTime;
    [SerializeField] private bool showFirstOnAwake = true;

    private Action<int> _onCanvasSwitched;

    public event Action<int> OnCanvasSwitched
    {
        add => _onCanvasSwitched += value;
        remove => _onCanvasSwitched -= value;
    }

    private void Awake()
    {
        if (!showFirstOnAwake) return;
        HideAllTables();
        if (tableNamedDisabler.Length == 0 || tableNamedDisabler[0] == null) return;
        tableNamedDisabler[0].value.DisplayObject(true);
        SetText(tableNamedDisabler[0]);
    }

    public void OpenTable(BaseObjectDisabler objectDisabler)
    {
        HideAllTables();
        var namedGroup = GetNamedGroup(objectDisabler);
        if (namedGroup == null) return;
        namedGroup.value.DisplayObject(true);
        SetText(namedGroup);
        _onCanvasSwitched?.Invoke(objectDisabler.GetHashCode());
    }

    private void SetText(NamedDisabler namedGroup)
    {
        if (textField != null)
            textField.text = namedGroup.key;
    }

    private NamedDisabler GetNamedGroup(BaseObjectDisabler objectDisabler)
    {
        var namedGroup = tableNamedDisabler.FirstOrDefault(n => n.value.GetHashCode() == objectDisabler.GetHashCode());
        return namedGroup;
    }

    public void DelayedOpenTable(BaseObjectDisabler objectDisabler)
    {
        HideAllTables();
        var namedGroup = GetNamedGroup(objectDisabler);
        if (namedGroup == null) return;
        SetText(namedGroup);
        _onCanvasSwitched?.Invoke(objectDisabler.GetHashCode());
        StartCoroutine(namedGroup.value.DisplayObject(true, delayTime, (canvas) => { namedGroup.value = canvas; }));
    }

    public void HideAllTables()
    {
        for (int i = 0; i < tableNamedDisabler.Length; i++)
        {
            tableNamedDisabler[i].value.DisplayObject(false);
        }
    }
}