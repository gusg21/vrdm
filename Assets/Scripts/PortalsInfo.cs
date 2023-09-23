using System;
using UnityEngine;

namespace Unity.Template.VR
{
    public class PortalsInfo : MonoBehaviour
    {
        public static PortalsInfo I;
        
        public Material GreenPortalMaterial;
        public Material RedPortalMaterial;
        public float TeleportDistance = 0.5f;

        public void Awake() => I = this;
    }
}