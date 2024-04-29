using Unity.Template.VR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class DMConfigurable : MonoBehaviour
{
    public static DMConfigurable ConfigOpen;

    public bool Hovered;
    public bool EditPanelVisible = false;
    public DMConfigPanel ConfigPanel;

    private DMConfigPanel _panel;

    private bool _isTrigger => GetComponent<DMTrigger>()?.enabled ?? false;

    XRSimpleInteractable interactible;

    // Start is called before the first frame update
    void Start()
    {
        GameInfo.I.ConfigureAction.started += ConfigurableClicked;

        interactible = GetComponent<XRSimpleInteractable>();

        interactible.hoverEntered.AddListener(OnHoverEntered);
        interactible.hoverExited.AddListener(OnHoverExited);
    }

    private void ToggleEditPanel()
    {
        if (EditPanelVisible && ConfigOpen == this)
        {
            EditPanelVisible = false;
            ConfigOpen = null;
            Destroy(_panel);
            GameInfo.I.Unpause();
        }
        else if (!EditPanelVisible && ConfigOpen == null)
        {
            // Spawn panel
            _panel = Instantiate(ConfigPanel, transform.root);
            _panel.Configurable = this;

        }
    }

    private void ConfigurableClicked(InputAction.CallbackContext obj)
    {
        if (Hovered)
        {
            ToggleEditPanel();
        }
    }

    public bool IsTrigger() => _isTrigger;

    public void ToggleTrigger()
    {
        if (_isTrigger) GetComponent<DMTrigger>().enabled = false;
        else
        {
            var trigger = GetComponent<DMTrigger>();
            if (trigger == null)
            {
                trigger = gameObject.AddComponent<DMTrigger>();
            }

            trigger.enabled = true;
        }
    }

    public void OnHoverEntered(HoverEnterEventArgs evt) => Hovered = true;

    public void OnHoverExited(HoverExitEventArgs evt) => Hovered = false;

    // Update is called once per frame
    void Update()
    {
        
    }
}
