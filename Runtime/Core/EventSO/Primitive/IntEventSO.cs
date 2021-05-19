using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "IntEventSO", menuName = "Scriptable Objects/Scriptable Events/Primitive/Int Event", order = 1)]
    public class IntEventSO : GenericEventSO<int>
    {
#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField]
        int editorData;

        [CustomEditor(typeof(IntEventSO), true)]
        public class IntEventSOEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Raise"))
                {
                    var e = target as IntEventSO;
                    e.Raise(e.editorData);
                }

                if (GUILayout.Button("RequestRaise"))
                {
                    var e = target as IntEventSO;
                    e.RequestRaise();
                }
            }
        }
#endif
    }
}