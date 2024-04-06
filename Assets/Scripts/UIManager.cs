using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int MidiChannel = 1;
    public TMP_Text MidiChannelText;

    private void Start()
    {
        UpdateMidiChannelText();
    }

    public void UpdateMidiChannelText()
    {
        MidiChannelText.text = "Midi Channel #" + MidiChannel.ToString();
    }

    public void IncrementMidiChannel()
    {
        MidiChannel++;
        MidiChannel = Mathf.Clamp(MidiChannel, 1, 16);
        UpdateMidiChannelText();
    }
}
