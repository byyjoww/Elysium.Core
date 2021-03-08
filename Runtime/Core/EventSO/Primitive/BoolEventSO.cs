using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Elysium.Core
{
#endif

    [CreateAssetMenu(fileName = "BoolEventSO", menuName = "Scriptable Objects/Scriptable Events/Primitive/Bool Event", order = 1)]
    public class BoolEventSO : GenericEventSO<bool>
    {
#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField]
        bool editorData;

        [CustomEditor(typeof(BoolEventSO), true)]
        public class BoolEventSOEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Raise"))
                {
                    var e = target as BoolEventSO;
                    e.Raise(e.editorData);
                }
            }
        }
#endif
    }
}