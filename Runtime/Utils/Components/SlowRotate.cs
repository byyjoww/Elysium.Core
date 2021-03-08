using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class SlowRotate : MonoBehaviour
    {
        [SerializeField] private Transform transformToRotate = null;
        [SerializeField] private Vector3 direction = new Vector3(0, 1, 0);
        [SerializeField] private float speed = 10f;

        public void SetRotation(Vector3 _direction, float _speed, Transform _transform = null)
        {
            transformToRotate = _transform;
            direction = _direction;
            speed = _speed;
        }

        private void Rotate()
        {
            transformToRotate.Rotate(direction * speed * Time.deltaTime);
        }

        private void Update()
        {
            if (transformToRotate == null) { transformToRotate = transform; }
            Rotate();
        }
    }
}