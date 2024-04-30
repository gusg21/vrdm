using System.Collections;
using System.Collections.Generic;
using Unity.Template.VR;
using UnityEngine;

public class DMQuadrant : MonoBehaviour
{
    private BoxCollider box;
    private static Dictionary<MidiZone, Bounds> bounds = new();

    public MidiZone Zone = MidiZone.A;

    // Start is called before the first frame update
    void Start()
    {
        //box = GetComponent<BoxCollider>();
        //bounds[Zone] = box.bounds;
    }
    public static MidiZone GetZoneFromPosition(Vector3 position)
    {
        foreach (var offset in bounds.Keys)
        {
            var bound = bounds[offset];
            if (bound.Contains(position))
            {
                return offset;
            }
        }
        return 0;
    }
}
