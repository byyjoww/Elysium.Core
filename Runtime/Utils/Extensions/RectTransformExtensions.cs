using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class RectTransformExtensions
    {
        public static float Left(this RectTransform _rect)
        {
            return _rect.position.x - _rect.rect.width * _rect.pivot.x;
        }

        public static float Right(this RectTransform _rect)
        {
            return _rect.position.x + _rect.rect.width * (1f - _rect.pivot.x);
        }

        public static float Top(this RectTransform _rect)
        {
            return _rect.position.y + _rect.rect.height * (1f - _rect.pivot.y);
        }

        public static float Bottom(this RectTransform _rect)
        {
            return _rect.position.y - _rect.rect.height * _rect.pivot.y;
        }

        public static void Stretch(this RectTransform _rect)
        {
            if (_rect.parent == null) 
            {
                Debug.LogError($"Unable to stretch rect transform {_rect.name} as it has no parent");
                return; 
            }

            RectTransform parent = _rect.parent as RectTransform;
            _rect.anchoredPosition = parent.position;
            _rect.anchorMin = new Vector2(0f, 0f);
            _rect.anchorMax = new Vector2(1f, 1f);
            _rect.pivot = new Vector2(0.5f, 0.5f);
            _rect.offsetMin = Vector2.zero;
            _rect.offsetMax = Vector2.zero;
        }
    }
}