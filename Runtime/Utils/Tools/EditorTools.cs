using UnityEngine;

namespace Elysium.Utils
{
    public static class EditorTools
    {
        public static bool InEditMode()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (Time.frameCount != 0 && Time.renderedFrameCount != 0) //not loading scene
                {
                    return true;
                }
            }
#endif
            return false;
        }
    }
}
