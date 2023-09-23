using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Unity.Template.VR
{
    public class DMValueEdit : MonoBehaviour
    {
        public DMEditable Editable;

        public InputAction IncrementAxisAction;
        public float AxisTriggerPoint = 0.5f;
        private bool _decrementFlag;
        private bool _incrementFlag;
        
        public UnityEvent OnIncrement;
        public UnityEvent OnDecrement;

        private void Start() => IncrementAxisAction.Enable();

        private void Update()
        {
            if (Editable.Hovered)
            {
                var axis = IncrementAxisAction.ReadValue<float>();
                // Debug.Log(axis);
                if (axis > AxisTriggerPoint)
                {
                    if (!_incrementFlag)
                    {
                        _incrementFlag = true;
                        OnIncrement.Invoke();
                    }
                }
                else
                {
                    _incrementFlag = false;
                }
                if (axis < -AxisTriggerPoint)
                {
                    if (!_decrementFlag)
                    {
                        _decrementFlag = true;
                        OnDecrement.Invoke();
                    }
                }
                else
                {
                    _decrementFlag = false;
                }
            }
        }
    }
}