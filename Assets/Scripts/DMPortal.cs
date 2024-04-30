using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Template.VR
{
    [RequireComponent(typeof(DMEditable))]
    [RequireComponent(typeof(Collider))]
    public class DMPortal : MonoBehaviour
    {
        public DMPortal OtherPortal;
        public bool IsGreen;
        public MeshRenderer PlaneMeshRenderer;

        [HideInInspector] public bool QueuedForDestruction = false;

        private void Start()
        {
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            if (IsGreen)
            {
                PlaneMeshRenderer.material = PortalsInfo.I.GreenPortalMaterial;
            }
            else
            {
                PlaneMeshRenderer.material = PortalsInfo.I.RedPortalMaterial;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherPortalTransform = OtherPortal.transform;
            var otherPortalUp = otherPortalTransform.up;
            other.transform.position = otherPortalTransform.position +
                                       otherPortalUp * PortalsInfo.I.TeleportDistance;
            other.attachedRigidbody.velocity = otherPortalUp * other.attachedRigidbody.velocity.magnitude;
        }

        private void OnDestroy()
        {
            QueuedForDestruction = true;
            if (!OtherPortal.QueuedForDestruction) Destroy(OtherPortal.gameObject);
        }
    }
}