using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Elysium.Utils
{
#if UNITY_EDITOR
    public abstract class EditorWithSubEditors<T> : Editor where T : Object
    {
        private List<bool> fold = default;
        protected Dictionary<T, Editor> editors = default;

        private GUIStyle WindowStyle
        {
            get
            {
                GUIStyle style = GUI.skin.window;                
                style.padding.top = 5;
                return style;
            }
        }

        protected virtual void OnEnable()
        {
            editors = new Dictionary<T, Editor>();
            fold = new List<bool>();
        }

        protected virtual void OnDisable()
        {
            CleanupEditors();
        }

        public void DrawSubInspectors(ref List<T> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                T asset = targets[i];
                EditorGUILayout.BeginVertical(WindowStyle);
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                var currentFold = CreateFold(i, asset);
                CreateRemoveButton(targets, asset);
                EditorGUILayout.EndHorizontal();
                if (currentFold) { CreateSubEditor(asset); }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
            }
        }

        private bool CreateFold(int i, T asset)
        {
            if (fold.Count <= i) fold.Add(new bool());
            string title = asset.GetType().ToString();
            fold[i] = EditorGUILayout.Foldout(fold[i], title, true);
            return fold[i];
        }

        private static void CreateRemoveButton(List<T> targets, T asset)
        {
            if (GUILayout.Button("X", GUILayout.MaxWidth(20f)))
            {
                targets.Remove(asset);
            }
        }

        private void CreateSubEditor(T asset)
        {
            if (!editors.ContainsKey(asset))
            {
                editors.Add(asset, CreateEditor(asset));
            }

            EditorGUILayout.Space();
            editors[asset].OnInspectorGUI();
        }

        protected void CleanupEditors()
        {
            if (editors == null) { return; }

            for (int i = 0; i < editors.Count; i++)
            {
                DestroyImmediate(editors.ElementAt(i).Value);
            }

            // Null the array so it's garbage collected.
            editors = null;
        }
    }
#endif
}