using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private UnityUpdatePhase phase;
        private Camera cam;

        private enum UnityUpdatePhase
        {
            UPDATE = 0,
            FIXED_UPDATE = 1,
            LATE_UPDATE = 2
        }

        private void Update()
        {
            if (phase != UnityUpdatePhase.UPDATE) { return; }
            Align();
        }

        private void FixedUpdate()
        {
            if (phase != UnityUpdatePhase.FIXED_UPDATE) { return; }
            Align();
        }

        private void LateUpdate()
        {
            if (phase != UnityUpdatePhase.LATE_UPDATE) { return; }
            Align();
        }

        // private void LookAt() => transform.LookAt(cam.transform.position);
        private void LookAt() => transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);

        private void Align()
        {
            if (!cam || cam == null) { cam = Camera.main; }
            LookAt();
        }
    }
}