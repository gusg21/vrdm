using System;
using System.Collections;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEditor;
using UnityEngine;

namespace Unity.Template.VR
{
    public class MidiInfo : MonoBehaviour
    {
        public static MidiInfo I;

        public OutputDevice Device;
        public string MidiDeviceName = "";
        public int MidiDeviceInternalId = 0;
        public float SpeedFor127Velocity = 10f;

        public SevenBitNumber SpeedToMidiVelocity(float speed)
        {
            float ratio = Mathf.Clamp01(Mathf.Abs(speed) / SpeedFor127Velocity);
            return (SevenBitNumber) Mathf.Lerp(0x00, 0x7F, ratio);
        }

        public NoteName IntToNoteName(int x)
        {
            return (NoteName) x;
        }

        public int NoteNameToInt(NoteName name)
        {
            return (int) name;
        }

        public IEnumerator Trigger(NoteName noteName, int octave, SevenBitNumber velocity)
        {
            Device.SendEvent(new NoteOnEvent(Note.Get(noteName, octave+1).NoteNumber, velocity));

            yield return new WaitForEndOfFrame();
            
            Device.SendEvent(new NoteOffEvent(Note.Get(noteName, octave+1).NoteNumber, velocity));
        }

        private void Awake()
        {
            I = this;
            Device = OutputDevice.GetByName(MidiDeviceName);
        } 

        private void OnApplicationQuit()
        {
            Debug.Log("Closing MIDI...");
            Device.TurnAllNotesOff();
            Device.Dispose();
        }
    }
}