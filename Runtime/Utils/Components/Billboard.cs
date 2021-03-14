using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private UnityUpdatePhase phase;
        [SerializeField] private Vector3 _vAxis = Vector3.up;
        [SerializeField] private A3DBillboardType _billboard = A3DBillboardType.Sphere;
        [SerializeField] private bool _correctRoll = true;

        private Vector3 vLook;
        private Vector3 vRight;
        private Vector3 vUp;
        private Camera cam;

        private Transform billboardTransform => transform;

        private enum UnityUpdatePhase
        {
            UPDATE = 0,
            FIXED_UPDATE = 1,
            LATE_UPDATE = 2
        }

        public enum A3DBillboardType
        {
            Flat,      // Inverse camera rotation
            Sphere,    // Looks directly at camera
            Axial      // Swivels around defined axis
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

        // private void LookAt() => transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);

        private void Align()
        {
            if (!cam || cam == null) { cam = Camera.main; }

            if (_billboard == A3DBillboardType.Flat)
            {
                // Flat billboards opposite rotation to camera
                billboardTransform.rotation = Quaternion.LookRotation(cam.transform.forward, cam.transform.up);

                // Check if correct for camera roll
                if (_correctRoll)
                {
                    Vector3 CameraRotation = cam.transform.eulerAngles;
                    if (CameraRotation.z != 0) { billboardTransform.Rotate(0f, 0f, -CameraRotation.z); }
                }
            }
            else if (_billboard == A3DBillboardType.Sphere)
            {
                // Spherical billboards look at camera position
                //MyTransform.LookAt(MyCameraTransform.position, MyCameraTransform.up);
                //MyTransform.Rotate(new Vector3(0f, 180f, 0f));

                // create billboard look vector
                vLook = billboardTransform.position - cam.transform.position;
                vLook.Normalize();

                // create billboard right vector
                vRight = Vector3.Cross(cam.transform.up, vLook);

                // create billboard up vector
                vUp = Vector3.Cross(vLook, vRight);

                // spherical billboard with look direction towards camera
                billboardTransform.rotation = Quaternion.LookRotation(vLook, vUp);

                // Check if correct for camera roll
                if (_correctRoll)
                {
                    Vector3 CameraRotation = cam.transform.eulerAngles;
                    if (CameraRotation.z != 0) { billboardTransform.Rotate(0f, 0f, -CameraRotation.z); }
                }
            }
            else if (_billboard == A3DBillboardType.Axial)
            {
                //create temporary billboard look vector
                vLook = billboardTransform.position - cam.transform.position;
                vLook.Normalize();

                //create billboard right vector
                float visible = Mathf.Abs(Vector3.Dot(_vAxis, vLook));
                if (visible >= 1)
                {
                    // look vector is parallel to axis
                    vLook = _vAxis;
                }
                else
                {
                    // create and normalize right vector
                    vRight = Vector3.Cross(_vAxis, vLook);
                    vRight.Normalize();

                    // create final billboard look vector
                    vLook = Vector3.Cross(vRight, _vAxis);

                    // create billboard up vector
                    vUp = Vector3.Cross(vLook, vRight);

                    // axial billboard with look rotation axis aligned
                    billboardTransform.rotation = Quaternion.LookRotation(vLook, vUp);
                }
            }
        }

    }
}
