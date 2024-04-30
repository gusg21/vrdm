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
        MidiChannelText.text = $"Midi Channel #{MidiChannel},{MidiChannel+1},{MidiChannel+2},{MidiChannel+3}";
    }

    public void IncrementMidiChannel()
    {
        MidiChannel++;
        if (MidiChannel >= 14)
        {
            MidiChannel = 1;
        } else if (MidiChannel <= 0)
        {
            MidiChannel = 13;
        }
        UpdateMidiChannelText();
    }

    public void Confirm()
    {
        Unity.Template.VR.MidiInfo.MidiChannel = MidiChannel;
    }
}
