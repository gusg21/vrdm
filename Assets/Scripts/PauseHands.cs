using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Template.VR
{
    public class PauseHands : MonoBehaviour
    {
        public List<MeshRenderer> HandMeshes;
        public Material PauseMaterial;
        public Material DefaultMaterial;

        private void Update()
        {
            foreach (var mesh in HandMeshes)
            {
                mesh.material = GameInfo.I.Paused ? PauseMaterial : DefaultMaterial;
            }
        }
    }
}