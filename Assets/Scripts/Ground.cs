using System;
using UnityEngine;

namespace Unity.Template.VR
{
    public class Ground : MonoBehaviour
    {
        public string[] DeleteTags = {"Apple"};
        private void OnCollisionEnter(Collision collision)
        {
            foreach (var tag in DeleteTags)
            {
                if (collision.gameObject.CompareTag(tag))
                {
                    Destroy(collision.gameObject);
                    return;
                }
            }
        }
    }
}