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
}