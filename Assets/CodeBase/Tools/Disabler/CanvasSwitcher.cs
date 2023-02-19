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
    [SerializeField]
    TMP_Text textField;
    [SerializeField]
    float delayTime;
    [SerializeField]
    bool showFirstOnAwake = true;

    Action<int> m_OnCanvasSwitched;

    public event Action<int> OnCanvasSwitched
    {
        add => m_OnCanvasSwitched += value;
        remove => m_OnCanvasSwitched -= value;
    }

    void Awake()
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
        NamedDisabler namedGroup = GetNamedGroup(objectDisabler);
        if (namedGroup == null) return;
        namedGroup.value.DisplayObject(true);
        SetText(namedGroup);
        m_OnCanvasSwitched?.Invoke(objectDisabler.GetHashCode());
    }

    void SetText(NamedDisabler namedGroup)
    {
        if (textField != null)
            textField.text = namedGroup.key;
    }

    NamedDisabler GetNamedGroup(BaseObjectDisabler objectDisabler)
    {
        NamedDisabler namedGroup = tableNamedDisabler.FirstOrDefault(n => n.value.GetHashCode() == objectDisabler.GetHashCode());
        return namedGroup;
    }

    public void DelayedOpenTable(BaseObjectDisabler objectDisabler)
    {
        HideAllTables();
        NamedDisabler namedGroup = GetNamedGroup(objectDisabler);
        if (namedGroup == null) return;
        SetText(namedGroup);
        m_OnCanvasSwitched?.Invoke(objectDisabler.GetHashCode());
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