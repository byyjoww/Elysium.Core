using System;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "DateTimeValueSO", menuName = "Scriptable Objects/Scriptable Value/Complex/DateTime", order = 1)]
    public class DateTimeValueSO : GenericValueSO<DateTime>
    {
        void Awake()
        {
            Value = DateTime.UtcNow;
        }
    }
}