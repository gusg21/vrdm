using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Unity.Template.VR
{
    public class DMTrigger : MonoBehaviour
    {
        [FormerlySerializedAs("_editable")] public DMEditable Editable;

        [FormerlySerializedAs("_lineRenderer")]
        public LineRenderer LineRenderer;

        public Material DefaultLineMaterial;
        public Material TriggeredLineMaterial;
        private float _triggerTtl = 0f;

        public List<DMTriggerable> MyTriggerables = new();

        private bool _connecting = false;

        private void Start()
        {
            TriggeredLineMaterial = Resources.Load<Material>("LineMaterial Triggered.mat");
            DefaultLineMaterial = Resources.Load<Material>("LineMaterial.mat");

            LineRenderer.material = DefaultLineMaterial;
        }

        public void SendTrigger()
        {
            if (MyTriggerables.Count > 0)
            {
                foreach (var triggerable in MyTriggerables)
                {
                    triggerable.Trigger();
                }
                LineRenderer.material = TriggeredLineMaterial;
                _triggerTtl = 0.1f;
            }
        }

        public void ClearConnectedTriggerable(DMTriggerable triggerable)
        {
            MyTriggerables.Remove(triggerable);
        }

        public void AddTriggerable(DMTriggerable triggerable)
        {
            if (!MyTriggerables.Contains(triggerable))
                MyTriggerables.Add(triggerable);
        }
        
        public void RemoveTriggerable(DMTriggerable triggerable)
        {
            if (MyTriggerables.Contains(triggerable))
                MyTriggerables.Remove(triggerable);
        }
        
        private void Update()
        {
            if (_triggerTtl <= 0f)
                LineRenderer.material = DefaultLineMaterial;
            else
                _triggerTtl -= Time.deltaTime;

            // If we're hovered and the button is pressed...
            if (Editable.Hovered && GameInfo.I.ConnectAction.IsPressed())
            {
                // Debug.Log("CONNECTING");
                // Enter connecting mode
                _connecting = true;
            }

            // If we're in connecting mode
            if (_connecting)
            {
                // Show the triggers we can connect to and register this trigger
                // with them
                foreach (var triggerable in DMTriggerable.Triggerables)
                {
                    triggerable.SetPotentialTrigger(this);
                }
            }

            // If we just released the button
            if (_connecting && !GameInfo.I.ConnectAction.IsPressed())
            {
                // Stop connecting and hide the selections
                _connecting = false;

                foreach (var triggerable in DMTriggerable.Triggerables)
                {
                    triggerable.SetPotentialTrigger(null);
                }
            }
            
            if (MyTriggerables.Count > 0)
            {
                LineRenderer.enabled = true;
                LineRenderer.positionCount = 1;
                LineRenderer.SetPosition(0, transform.position);
                int positionIndex = 1;
                foreach (var triggerable in MyTriggerables)
                {
                    LineRenderer.positionCount += 1;
                    LineRenderer.SetPosition(positionIndex, triggerable.transform.position);
                    positionIndex++;
                }
            }
            else
            {
                LineRenderer.enabled = false;
            }
        }

        private void OnDestroy()
        {
            foreach (var triggerable in MyTriggerables)
            {
                triggerable.RemoveTrigger(this);
            }
        }
    }
}