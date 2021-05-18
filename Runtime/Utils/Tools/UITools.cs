using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Elysium.Utils
{
    static class UITools
    {
        public static bool IsPointerOverUIObject()
        {
            if (EventSystem.current == null) { Debug.Log("current event system returned null"); return false; }

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

            float mx = Input.mousePosition.x;
            float my = Input.mousePosition.y;

            eventDataCurrentPosition.position = new Vector2(mx, my);
            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer == 5) //5 = UI layer
                {
                    return true;
                }
            }

            return false;
        }
    }

    static class CanvasTools
    {
        public enum CanvasMode { SCREEN_SPACE_CAMERA, SCREEN_SPACE_OVERLAY }

        public static Vector3 GetPositionInCanvas(CanvasMode mode, Vector2 targetPos, Canvas canvas)
        {
            if (mode == CanvasMode.SCREEN_SPACE_OVERLAY) { return GetPositionInScreenSpaceOverlayCanvas(targetPos, canvas); }
            else if (mode == CanvasMode.SCREEN_SPACE_CAMERA) { return GetPositionInScreenSpaceCameraCanvas(targetPos, canvas); }

            throw new System.NotImplementedException("canvas mode not implemented");
        }

        public static Vector3 GetPositionInScreenSpaceCameraCanvas(Vector2 targetPos, Canvas canvas)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, targetPos, canvas.worldCamera, out Vector2 pos);
            var posInCanvas = canvas.transform.TransformPoint(pos);
            return posInCanvas;
        }

        public static Vector3 GetPositionInScreenSpaceOverlayCanvas(Vector2 targetPos, Canvas canvas)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, targetPos, canvas.worldCamera, out Vector2 pos);
            var posInCanvas = canvas.transform.TransformPoint(pos);
            return posInCanvas;
        }
    }
}