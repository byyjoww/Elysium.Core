#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Elysium.Utils
{
    [InitializeOnLoad]
    public static class EditorPlayStateNotifier
    {
        public static event Action OnEnterEditMode;
        public static event Action OnEnterPlayMode;
        public static event Action OnExitEditMode;
        public static event Action OnExitPlayMode;

        private static List<UnityEngine.Object> registeredObjects = new List<UnityEngine.Object>();

        static EditorPlayStateNotifier()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
        }

        public static void RegisterOnExitPlayMode(UnityEngine.Object _savable, Action action)
        {
            if (registeredObjects.Contains(_savable)) { return; }

            // Debug.Log($"OnPlayModeExit: registered {_savable} to reset");
            OnEnterEditMode += action;
            registeredObjects.Add(_savable);
        }

        static void ModeChanged(PlayModeStateChange _stage)
        {
            switch (_stage)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnEnterEditMode?.Invoke();
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnEnterPlayMode?.Invoke();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    OnExitEditMode?.Invoke();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    OnExitPlayMode?.Invoke();
                    break;
            }
        }
    }
}
#endif