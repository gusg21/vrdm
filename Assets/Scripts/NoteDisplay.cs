using System;
using TMPro;
using UnityEngine;

namespace Unity.Template.VR
{
    [RequireComponent(typeof(TextMeshPro))]
    public class NoteDisplay : MonoBehaviour
    {
        public TextMeshPro TextMesh;
        public Apple Apple;
        
        public void Start()
        {
            TextMesh.text = $"{Apple.NoteName}-{Apple.Octave}";
        }
    }
}