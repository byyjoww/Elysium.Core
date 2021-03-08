using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Elysium.Core
{
#endif

    [CreateAssetMenu(fileName = "FloatEventSO", menuName = "Scriptable Objects/Scriptable Events/Primitive/Float Event", order = 1)]
    public class FloatEventSO : GenericEventSO<float>
    {
#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField]
        float editorData;

        [CustomEditor(typeof(FloatEventSO), true)]
        public class FloatEventSOEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Raise"))
                {
                    var e = target as FloatEventSO;
                    e.Raise(e.editorData);
                }
            }
        }
#endif
    }
}