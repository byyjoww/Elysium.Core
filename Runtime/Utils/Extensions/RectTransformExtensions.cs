using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class RectTransformExtensions
    {
        public static float Left(this RectTransform _e)
        {
            return _e.position.x - _e.rect.width * _e.pivot.x;
        }

        public static float Right(this RectTransform _e)
        {
            return _e.position.x + _e.rect.width * (1f - _e.pivot.x);
        }

        public static float Top(this RectTransform _e)
        {
            return _e.position.y + _e.rect.height * (1f - _e.pivot.y);
        }

        public static float Bottom(this RectTransform _e)
        {
            return _e.position.y - _e.rect.height * _e.pivot.y;
        }
    }
}