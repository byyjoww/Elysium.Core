using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Elysium.Core
{
#endif

    [CreateAssetMenu(fileName = "LongEventSO", menuName = "Scriptable Objects/Scriptable Events/Primitive/Long Event", order = 1)]
    public class LongEventSO : GenericEventSO<long>
    {
#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField]
        long editorData;

        [CustomEditor(typeof(LongEventSO), true)]
        public class LongEventSOEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Raise"))
                {
                    var e = target as LongEventSO;
                    e.Raise(e.editorData);
                }

                if (GUILayout.Button("RequestRaise"))
                {
                    var e = target as LongEventSO;
                    e.RequestRaise();
                }
            }
        }
#endif
    }
}