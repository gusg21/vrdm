using System;
using Melanchall.DryWetMidi.MusicTheory;
using TMPro;
using UnityEngine;
using Note = Melanchall.DryWetMidi.Interaction.Note;

namespace Unity.Template.VR
{
    public class DMSpawner : MonoBehaviour
    {
        public GameObject ApplePrefab;
        public NoteName NoteName;
        public int Octave;
        public MidiZone Zone;
        public TextMeshPro Text;
        public DMEditable Editable;

        private void Update()
        {
            Text.text = $"Spawner\n{NoteName}-{Octave}";
        }

        public void SpawnApple()
        {
            GameObject apple = Instantiate(ApplePrefab, transform.parent);
            apple.transform.position = transform.position;
            apple.GetComponent<Apple>().NoteName = NoteName;
            apple.GetComponent<Apple>().Octave = Octave;
            apple.GetComponent<Apple>().Editable = Editable;
            apple.GetComponent<Apple>().SetZone(Zone);
        }

        public void IncrementNote()
        {
            // Debug.Log("Inc");

            int noteIndex = (int) NoteName + 1;

            if (noteIndex > (int) NoteName.B)
            {
                noteIndex = 0;
                Octave++;
            }
            
            NoteName = (NoteName) noteIndex;
        }
        
        public void DecrementNote()
        {
            int noteIndex = (int) NoteName - 1;
            
            if (noteIndex < 0)
            {
                noteIndex = (int) NoteName.B;
                Octave--;
            }
            
            NoteName = (NoteName) noteIndex;
        }
    }
}