using Elysium.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class SimulatedGravity : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float rayDistance = 5;
        [SerializeField, ReadOnly] private bool isGrounded = false;

        private void OnCollisionEnter(Collision collision)
        {
            isGrounded = true;
        }

        private void Update()
        {
            if (isGrounded) { return; }
            if (Physics.Raycast(transform.position, -Vector3.up, rayDistance))
            {
                isGrounded = true;
            }

            transform.Translate(-Vector3.up * Time.deltaTime * speed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -Vector3.up * rayDistance);
        }
    }
}