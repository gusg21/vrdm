using System.Collections.Generic;
using Melanchall.DryWetMidi.Multimedia;
using UnityEditor;
using UnityEngine;

namespace Unity.Template.VR
{
    [CustomEditor(typeof(MidiInfo))]
    public class MidiInfoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUILayout.Separator();
            
            MidiInfo midi = (MidiInfo) target;

            int id = 0;
            foreach (var device in OutputDevice.GetAll())
            {
                GUIStyle style = id == midi.MidiDeviceInternalId ? EditorStyles.boldLabel : EditorStyles.label;
                EditorGUILayout.LabelField($"{id}: {device.Name}", style);
                if (id == midi.MidiDeviceInternalId)
                {
                    midi.MidiDeviceName = device.Name;
                }
                
                id++;
                device.Dispose();
            }

            // base.OnInspectorGUI();
        }
    }
}