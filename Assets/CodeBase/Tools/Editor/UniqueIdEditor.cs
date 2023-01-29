using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace CodeBase.Tools.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        void OnEnable()
        {
            var uniqueId = (UniqueId)target;
            if (IsPrefab(uniqueId))
                return;

            if (string.IsNullOrEmpty(uniqueId.guid))
            {
                Generate(uniqueId);
            }
        }

        bool IsPrefab(UniqueId uniqueId) => uniqueId.gameObject.scene.rootCount == 0;

        void Generate(UniqueId uniqueId)
        {
            uniqueId.guid = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}
