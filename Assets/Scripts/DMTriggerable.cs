using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Unity.Template.VR
{
    [RequireComponent(typeof(DMEditable))]
    public class DMTriggerable : MonoBehaviour
    {
        public UnityEvent OnTrigger;
        public Outline Outline;

        public static List<DMTriggerable> Triggerables = new();

        [FormerlySerializedAs("_editable")] public DMEditable Editable;
        private DMTrigger _potentialTrigger;
        private List<DMTrigger> _connectedTriggers = new();

        private void Start()
        {
            Triggerables.Add(this);

            GameInfo.I.ConnectAction.started += _ =>
            {
                if (Editable.Hovered) ClearConnectedTriggers();
            };
        }

        public void Trigger()
        {
            OnTrigger?.Invoke();   
        }
        public void SetHighlighted(bool highlighted) => Outline.enabled = highlighted;

        public void SetPotentialTrigger(DMTrigger trigger)
        {
            SetHighlighted(trigger != null);
            _potentialTrigger = trigger;
        }

        public void RemoveTrigger(DMTrigger trigger)
        {
            if (_connectedTriggers.Contains(trigger)) _connectedTriggers.Remove(trigger);
        }

        public void ClearConnectedTriggers()
        {
            foreach (var trigger in _connectedTriggers)
            {
                trigger.ClearConnectedTriggerable(this);
            }
            
            _potentialTrigger = null;
            _connectedTriggers.Clear();
        }

        private void Update()
        {
            if (_potentialTrigger != null && Editable.Hovered)
            {
                if (!_connectedTriggers.Contains(_potentialTrigger))
                {
                    _connectedTriggers.Add(_potentialTrigger);
                    _potentialTrigger.AddTriggerable(this);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var trigger in _connectedTriggers)
            {
                trigger.RemoveTriggerable(this);
            }
        }
    }
}