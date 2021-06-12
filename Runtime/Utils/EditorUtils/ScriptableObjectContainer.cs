using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Elysium.Utils
{
    [System.Serializable]
    public abstract class ScriptableObjectContainer<T> : ScriptableObject where T : ScriptableObject
    {
        [HideInInspector] public List<T> Nodes;

        private void Awake()
        {
            Nodes = new List<T>();
        }
    }

#if UNITY_EDITOR
    public class ScriptableObjectContainerEditor<T> : Editor where T : ScriptableObject
    {
        private ScriptableObjectContainer<T> container = default;
        private List<bool> fold = default;
        private Dictionary<T, Editor> editor = default;
        private int childIndex = 0;

        private List<Type> Children => typeof(T).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();

        private GUIStyle ContainerStyle
        {
            get
            {
                return GUI.skin.box;
            }
        }

        private GUIStyle WindowStyle
        {
            get
            {
                GUIStyle style = GUI.skin.window;
                style.padding.top = 5;
                return style;
            }
        }

        private void OnEnable()
        {
            container = (ScriptableObjectContainer<T>)target;
            fold = new List<bool>();
            editor = new Dictionary<T, Editor>();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw default inspector
            DrawDefaultInspector();
            EditorGUILayout.Space();

            // Draw container
            EditorGUILayout.BeginVertical(ContainerStyle);
            EditorGUI.indentLevel++;

            DrawNodes();
            EditorGUILayout.Space();

            if (container.Nodes.Count > 0)
            {
                EditorGUILayout.Space();
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Components: " + container.Nodes.Count, EditorStyles.boldLabel);
            DrawComponentOptionsPopup();
            DrawAddComponentButton();
            DrawRefreshButton();
            DrawClearButton();
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawClearButton()
        {
            if (GUILayout.Button("Clear")) { ClearNodes(); }
        }

        private void DrawRefreshButton()
        {
            if (GUILayout.Button("Refresh")) { RefreshNodes(); }
        }

        private void DrawAddComponentButton()
        {
            if (GUILayout.Button("Add"))
            {
                AddNewAsset(Children[childIndex]);
            }
        }

        private void DrawComponentOptionsPopup()
        {
            string[] options = Children.Select(s => s.ToString()).ToArray();
            childIndex = EditorGUILayout.Popup(childIndex, options);
        }

        private void DrawNodes()
        {
            for (int i = 0; i < container.Nodes.Count; i++)
            {
                T asset = container.Nodes[i];
                EditorGUILayout.BeginVertical(WindowStyle);
                EditorGUI.indentLevel++;
                GUILayout.BeginHorizontal();

                // Draw Fold
                if (fold.Count <= i) fold.Add(new bool());
                string title = asset.GetType().ToString();
                fold[i] = EditorGUILayout.Foldout(fold[i], title, true);

                // Draw Remove Button
                if (GUILayout.Button("X", GUILayout.MaxWidth(20f)))
                {
                    RemoveAsset(asset);
                    i--;
                    continue;
                }

                GUILayout.EndHorizontal();

                // Draw Sub-Inspector
                if (fold[i])
                {
                    if (!editor.ContainsKey(asset))
                    {
                        editor.Add(asset, Editor.CreateEditor(asset));
                    }

                    editor[asset].OnInspectorGUI();
                    EditorGUILayout.Space();
                }

                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
            }
        }        

        private void ClearNodes()
        {
            for (int i = container.Nodes.Count - 1; i >= 0; i--)
            {
                T asset = container.Nodes[i];
                RemoveAsset(asset);
            }
        }

        private void RefreshNodes()
        {
            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(container));
            container.Nodes = assets.Where(x => x is T && x != container).Select(x => x as T).ToList();
            RemoveUnrelatedChildren(assets);
        }

        private void AddNewAsset(Type assetType)
        {
            T newAsset = (T)CreateInstance(assetType);
            newAsset.name = $"{assetType}";
            AssetDatabase.AddObjectToAsset(newAsset, container);
            container.Nodes.Add(newAsset);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset), ImportAssetOptions.Default);
            Save();
        }

        private void RemoveAsset(T asset)
        {
            container.Nodes.Remove(asset);
            DestroyImmediate(asset, true);
            Save();
        }

        private void RemoveUnrelatedChildren(UnityEngine.Object[] assets)
        {
            foreach (var asset in assets)
            {
                if (!(asset is T) && asset != container)
                {
                    Debug.Log("Asset Destroyed");
                    DestroyImmediate(asset, true);
                }
            }

            Save();
        }

        private void Save()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}