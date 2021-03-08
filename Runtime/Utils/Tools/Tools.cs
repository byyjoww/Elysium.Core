using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Elysium.Utils
{
    static class Tools
    {
        public static void Invoke(MonoBehaviour _worker, Action _action, float _delay = 0)
        {
            _worker.StartCoroutine(DelayedExecution(_action, _delay));
        }

        public static void Invoke(MonoBehaviour _worker, Action _action, Func<bool> _when)
        {
            _worker.StartCoroutine(DelayedExecution(_action, _when));
        }

        public static IEnumerator DelayedExecution(Action _action, Func<bool> _conditionToExecute)
        {
            yield return new WaitUntil(_conditionToExecute);
            _action.Invoke();
        }

        public static IEnumerator DelayedExecution(Action _action, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _action.Invoke();
        }

        public static void Update(MonoBehaviour _worker, Action _action, float _delay, Func<bool> _stopCondition)
        {
            _worker.StartCoroutine(UpdateRoutine(_action, _delay, _stopCondition));
        }

        public static IEnumerator UpdateRoutine(Action _action, float _delay, Func<bool> _stopCondition)
        {
            do
            {
                yield return new WaitForSeconds(_delay);
                _action.Invoke();
            }
            while (!_stopCondition.Invoke());
        }
    }
}