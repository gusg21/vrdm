using System;
using System.Collections;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using TMPro;
using Unity.Template.VR;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    public TextMeshPro TextMesh;
    public Rigidbody Body;
    public DMEditable Editable;

    [Tooltip("The amount of time to wait between collision triggers (in seconds)")]
    public float TriggerDeadTime = 0.1f;

    public NoteName NoteName;
    [Range(-1, 8)]
    public int Octave = 1;

    private float _lastTriggerTime = 0f;
    private OutputDevice _myDevice;
    private MidiZone Zone = MidiZone.A;

    private void Start()
    {
        TextMesh.text = $"{NoteName}-{Octave}";
    }

    private void Update()
    {
        if (Editable.Manipulating) Body.Sleep();
        else Body.WakeUp();
    }

    public void SetZone(MidiZone zone) { Zone = zone; }

    public IEnumerator OnCollisionEnter(Collision colData)
    {
        // Don't count anything but the triggers (AHPlanes) as triggers
        if (!colData.collider.CompareTag("Trigger")) yield break;

        // Don't trigger again too fast
        if (Time.time - _lastTriggerTime > TriggerDeadTime)
        {
            _lastTriggerTime = Time.time;

            colData.gameObject.GetComponent<DMTrigger>()?.SendTrigger();

            yield return MidiInfo.I.Trigger(NoteName, Octave,
                MidiInfo.I.SpeedToMidiVelocity(colData.relativeVelocity.magnitude), Zone);
        }
    }
}