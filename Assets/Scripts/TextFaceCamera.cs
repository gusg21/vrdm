using System;
using UnityEngine;

namespace Unity.Template.VR
{
    public class TextFaceCamera : MonoBehaviour
    {
        public float ExtraYRotation = 180f;

        private Camera _camera;

        public void Start()
        {
            _camera = Camera.main;
        }

        public void Update()
        {
            transform.LookAt(_camera.transform, Vector3.up);
            transform.Rotate(Vector3.up, ExtraYRotation);
        }
    }
}