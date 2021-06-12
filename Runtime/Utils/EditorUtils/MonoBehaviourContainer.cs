using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Elysium.Utils
{
    public class MonoBehaviourContainer<T> : MonoBehaviour where T : ScriptableObject
    {
        public List<T> Nodes = new List<T>();
    }

#if UNITY_EDITOR
    public class MonoBehaviourContainerEditor<T> : EditorWithSubEditors<T> where T : ScriptableObject
    {
        private MonoBehaviourContainer<T> container;

        private Type[] assemblyTypes;
        private string[] effectsTypeNames;
        private int selectedIndex;

        private const float dropAreaHeight = 50f;
        private const float controlSpacing = 5f;

        private GUIStyle WindowStyle
        {
            get
            {
                return GUI.skin.box;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            container = (MonoBehaviourContainer<T>)target;
            CreateNameArrayFromAssemblyTypes();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical(WindowStyle);
            EditorGUI.indentLevel++;
            
            DrawSubInspectors(ref container.Nodes);

            if (container.Nodes.Count > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }

            Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(dropAreaHeight + EditorGUIUtility.standardVerticalSpacing));
            Rect leftAreaRect = CreateLeftAreaRect(fullWidthRect);
            Rect rightAreaRect = CreateRightAreaRect(leftAreaRect);

            TypeSelectionGUI(leftAreaRect);
            DragAndDropAreaGUI(rightAreaRect);
            DraggingAndDropping(rightAreaRect, container);

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

        private static Rect CreateRightAreaRect(Rect leftAreaRect)
        {
            Rect rightAreaRect = leftAreaRect;
            rightAreaRect.x += rightAreaRect.width + controlSpacing;
            return rightAreaRect;
        }

        private static Rect CreateLeftAreaRect(Rect fullWidthRect)
        {
            Rect leftAreaRect = fullWidthRect;
            leftAreaRect.y += EditorGUIUtility.standardVerticalSpacing * 0.5f;
            leftAreaRect.width *= 0.5f;
            leftAreaRect.width -= controlSpacing * 0.5f;
            leftAreaRect.height = dropAreaHeight;
            return leftAreaRect;
        }

        private void TypeSelectionGUI(Rect containingRect)
        {
            Rect topHalf = containingRect;
            topHalf.height *= 0.5f;

            Rect bottomHalf = topHalf;
            bottomHalf.y += bottomHalf.height;
            selectedIndex = EditorGUI.Popup(topHalf, selectedIndex, effectsTypeNames);

            if (GUI.Button(bottomHalf, "Add Selected Reaction"))
            {
                Type scriptableObjectType = assemblyTypes[selectedIndex];
                CreateNewObject(container, scriptableObjectType);
            }
        }

        private static void DragAndDropAreaGUI(Rect containingRect)
        {
            GUIStyle centredStyle = GUI.skin.box;
            centredStyle.alignment = TextAnchor.MiddleCenter;
            centredStyle.normal.textColor = GUI.skin.button.normal.textColor;
            GUI.Box(containingRect, "Drop new Reactions here", centredStyle);
        }

        private static void DraggingAndDropping(Rect dropArea, MonoBehaviourContainer<T> assetContainer)
        {
            Event currentEvent = Event.current;
            if (!dropArea.Contains(currentEvent.mousePosition))
                return;
            switch (currentEvent.type)
            {
                case EventType.DragUpdated:
                    DragAndDrop.visualMode = IsDragValid() ? DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected;
                    currentEvent.Use();
                    break;
                case EventType.DragPerform:
                    DragAndDrop.AcceptDrag();
                    for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                    {
                        MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
                        Type scriptableObjectType = script.GetClass();
                        CreateNewObject(assetContainer, scriptableObjectType);
                    }
                    currentEvent.Use();
                    break;
            }
        }

        private static void CreateNewObject(MonoBehaviourContainer<T> assetContainer, Type scriptableObjectType)
        {
            T newEffect = (T)CreateInstance(scriptableObjectType);
            assetContainer.Nodes.Add(newEffect);
        }

        private static bool IsDragValid()
        {
            for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
            {
                if (DragAndDrop.objectReferences[i].GetType() != typeof(MonoScript)) { return false; }

                MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
                Type scriptType = script.GetClass();
                if (!scriptType.IsSubclassOf(typeof(T))) { return false; }
                if (scriptType.IsAbstract) { return false; }
            }

            return true;
        }

        private void CreateNameArrayFromAssemblyTypes()
        {
            Type effectTypes = typeof(T);
            Type[] allTypes = effectTypes.Assembly.GetTypes();
            List<Type> reactionSubTypeList = new List<Type>();

            for (int i = 0; i < allTypes.Length; i++)
            {
                if (allTypes[i].IsSubclassOf(effectTypes) && !allTypes[i].IsAbstract)
                {
                    reactionSubTypeList.Add(allTypes[i]);
                }
            }

            assemblyTypes = reactionSubTypeList.ToArray();
            List<string> reactionTypeNameList = new List<string>();

            for (int i = 0; i < assemblyTypes.Length; i++)
            {
                reactionTypeNameList.Add(assemblyTypes[i].Name);
            }

            effectsTypeNames = reactionTypeNameList.ToArray();
        }
    }
#endif
}
