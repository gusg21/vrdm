using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.Template.VR
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class DMEditable : MonoBehaviour
    {
        public Outline HoverOutline;
        public float MaxMoveDistance = 0.1f;
        public float MinMoveDistance = 10f;
        public float MoveLerpAmt = 0.2f;
        public float MoveDistLerpAmt = 0.2f;
        public float RotateLerpAmt = 0.2f;
        public XRRayInteractor Interactor;

        private bool _rotating = false;
        private bool _moving = false;
        private bool _hovered = false;
        public bool Manipulating => _rotating || _moving;
        public bool Hovered => _hovered;
        private Quaternion _initialRotation;
        private float _rayMoveDistance;

        public void Start()
        {
            if (Interactor == null)
            {
                Interactor = FindObjectOfType<XRRayInteractor>();
                if (Interactor == null)
                    Debug.LogError("Failed to find XRRayInteractor!");
            }
            
            HoverOutline.enabled = false;

            GameInfo.I.RotateAction.started += OnRotateStarted;
            GameInfo.I.RotateAction.canceled += OnRotateEnded;

            GameInfo.I.MoveAction.started += OnMoveStarted;
            GameInfo.I.MoveAction.canceled += OnMoveCanceled;

            GameInfo.I.DeleteAction.started += _ =>
            {
                if (_hovered) Destroy(gameObject);
            };
        }

        private void FixedUpdate()
        {
            HoverOutline.enabled = _hovered || _rotating || _moving;

            if (_rotating)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.Inverse(_initialRotation) * Interactor.transform.rotation,
                    RotateLerpAmt);
            }

            if (_moving)
            {
                Ray handRay = new(Interactor.rayOriginTransform.position, Interactor.rayOriginTransform.forward);

                var rawMoveDist = -GameInfo.I.MoveDistAxisAction.ReadValue<float>() / 2f + 0.5f; // [-1, 1] -> [0, 1]
                Debug.Log(rawMoveDist);
                rawMoveDist = Mathf.Lerp(MinMoveDistance, MaxMoveDistance, rawMoveDist); // [0, 1] -> [min, max]
                _rayMoveDistance = Mathf.Lerp(_rayMoveDistance, rawMoveDist, MoveDistLerpAmt);
                transform.position = Vector3.Lerp(transform.position, handRay.GetPoint(_rayMoveDistance), MoveLerpAmt);
            }
        }

        private void OnMoveStarted(InputAction.CallbackContext _)
        {
            if (_hovered) _moving = true;
        }

        private void OnMoveCanceled(InputAction.CallbackContext _)
        {
            if (_moving) _moving = false;
        }

        public void OnHoverEntered(HoverEnterEventArgs evt) => _hovered = true;

        public void OnHoverExited(HoverExitEventArgs evt) => _hovered = false;

        public void OnRotateStarted(InputAction.CallbackContext _)
        {
            if (_hovered)
            {
                _rotating = true;
                _initialRotation = Interactor.transform.rotation;
                Interactor.GetComponent<XRInteractorLineVisual>().enabled = false;
            }
        }

        public void OnRotateEnded(InputAction.CallbackContext _)
        {
            if (_rotating)
            {
                _rotating = false;
                Interactor.GetComponent<XRInteractorLineVisual>().enabled = true;
            }
        }
    }
}