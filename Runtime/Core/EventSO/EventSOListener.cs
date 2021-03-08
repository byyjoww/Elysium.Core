using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Core
{
    public class EventSOListener : MonoBehaviour
    {
        [SerializeField] protected EventSO Event;
        [SerializeField] protected UnityEvent Response;

        void OnEnable()
        {
            Event.OnRaise += OnEventRaised;
        }

        void OnDisable()
        {
            Event.OnRaise -= OnEventRaised;
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}