using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.Template.VR
{
    public class DMObjectCreator : MonoBehaviour
    {
        [Header("Connections")]
        public XRRayInteractor RayInteractor;
        public TextMeshPro TextMesh;
        public MeshRenderer Reticle;
        
        [Header("Actions")]
        public InputAction CreateAction;
        public InputAction ObjectChangeAction;
        public float ObjectChangeTriggerPoint = 0.5f;
        private bool _prevObj;
        private bool _nextObj;

        [Header("Constants")]
        public float MoveLerpT = 0.1f;
        public float ReticleOffsetDistance = 0.2f;
        public float ObjectCreateOffsetDistance = 1f;

        [Header("Object Registry")] 
        public List<GameObject> Prefabs;
        public List<string> FriendlyNames;

        private int _creationIndex = 0;
        private bool _creating = false;

        private void Start()
        {
            Reticle.enabled = false;
            TextMesh.enabled = false;
            
            CreateAction.Enable();
            ObjectChangeAction.Enable();

            CreateAction.started += _ =>
            {
                _creationIndex = 0;
                _creating = true;
            };
            CreateAction.canceled += _ =>
            {
                _creating = false;
                SpawnObject();
            };
        }

        private void SpawnObject()
        {
            RaycastHit castResult;
            bool success = RayInteractor.TryGetCurrent3DRaycastHit(out castResult);
            if (success)
            {
                GameObject newObject = Instantiate(Prefabs[_creationIndex], transform.parent);
                newObject.transform.position = castResult.point + castResult.normal * ObjectCreateOffsetDistance;
            }
        }

        private void Update()
        {
            RaycastHit castResult;
            bool success = RayInteractor.TryGetCurrent3DRaycastHit(out castResult);
            if (success)
            {
                transform.position = Vector3.Lerp(transform.position, castResult.point, MoveLerpT);
                Reticle.transform.up = -castResult.normal.normalized;
                Reticle.transform.localPosition = Reticle.transform.up * -ReticleOffsetDistance;
                
                Reticle.enabled = _creating;
                TextMesh.enabled = _creating;

                if (_creating)
                {
                    var axis = ObjectChangeAction.ReadValue<float>();
                    if (axis > ObjectChangeTriggerPoint)
                    {
                        if (!_nextObj)
                        {
                            _nextObj = true;
                            _creationIndex++;
                        }
                    }
                    else
                    {
                        _nextObj = false;
                    }
                    if (axis < -ObjectChangeTriggerPoint)
                    {
                        if (!_prevObj)
                        {
                            _prevObj = true;
                            _creationIndex--;
                        }
                    }
                    else
                    {
                        _prevObj = false;
                    }
                    _creationIndex = (int) Mathf.Repeat(_creationIndex, FriendlyNames.Count);

                    TextMesh.text = $"Create:\n< {FriendlyNames[_creationIndex]} >";
                }
            }
        }
    }
}