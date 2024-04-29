using System.Collections;
using System.Collections.Generic;
using Unity.Template.VR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class UIButton : MonoBehaviour
{
    public XRRayInteractor Interactor;
    public Collider ButtonCollider;
    public UnityEvent ButtonEvent;

    public bool Hovered;

    // Start is called before the first frame update
    void Start()
    {
        if (ButtonCollider == null)
            ButtonCollider = GetComponent<Collider>();

        if (Interactor == null)
        {
            Interactor = FindObjectOfType<XRRayInteractor>();
            if (Interactor == null)
                Debug.LogError("Failed to find XRRayInteractor!");
        }

        GameInfo.I.UIClickAction.started += ButtonClicked;
    }

    private void ButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Hovered)
        {
            ButtonEvent?.Invoke();
        }
    }

    public void OnHoverEntered(HoverEnterEventArgs evt) => Hovered = true;

    public void OnHoverExited(HoverExitEventArgs evt) => Hovered = false;
}
