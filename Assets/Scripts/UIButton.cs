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

        GameInfo.I.UIClickAction.performed += UIClickAction_performed;
    }

    private void UIClickAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Hovered)
        {
            ButtonEvent?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray handRay = new(Interactor.rayOriginTransform.position, Interactor.rayOriginTransform.forward);
        RaycastHit hitInfo;
        Physics.Raycast(handRay, out hitInfo);
        Hovered = hitInfo.collider == ButtonCollider;
    }
}
