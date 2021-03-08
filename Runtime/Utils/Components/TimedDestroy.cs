using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class TimedDestroy : MonoBehaviour
    {
        [SerializeField] private float delay = 10f;

        public float Delay => delay;            

        private void Start() => Destroy(gameObject, Delay);
    }
}