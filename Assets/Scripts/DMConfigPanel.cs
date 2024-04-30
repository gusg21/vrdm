using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DMConfigPanel : MonoBehaviour
{
    public TMP_Text TriggerStatusDisplay;
    public DMConfigurable Configurable;
    public UIButton TriggerToggleButton;

    // Start is called before the first frame update
    void Start()
    {
        TriggerToggleButton.ButtonEvent.AddListener(ToggleTrigger);
    }

    private void ToggleTrigger()
    {
        Configurable.ToggleTrigger();
    }

    public void ClosePanel()
    {
        Configurable.ClosePanel();
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = (transform.position - Camera.main.transform.position).normalized;

        TriggerStatusDisplay.text = Configurable.IsTrigger() ? "YES" : "NO";


    }
}
