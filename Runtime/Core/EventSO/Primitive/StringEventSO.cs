using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Elysium.Core
{
#endif

    [CreateAssetMenu(fileName = "StringEventSO", menuName = "Scriptable Objects/Scriptable Events/Primitive/String Event", order = 1)]
    public class StringEventSO : GenericEventSO<string>
    {
#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField]
        string editorData;

        [CustomEditor(typeof(StringEventSO), true)]
        public class StringEventSOEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Raise"))
                {
                    var e = target as StringEventSO;
                    e.Raise(e.editorData);
                }
            }
        }
#endif
    }
}