using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "Vector3EventSO", menuName = "Scriptable Objects/Scriptable Events/Custom/Vector3 Event", order = 1)]
    public class Vector3EventSO : GenericEventSO<Vector3>
    {
#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField]
        Vector3 editorData;

        [CustomEditor(typeof(Vector3EventSO), true)]
        public class Vector3EventSOEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Raise"))
                {
                    var e = target as Vector3EventSO;
                    e.Raise(e.editorData);
                }
            }
        }
#endif
    }
}