using UnityEngine;
using UnityEngine.UIElements;

public class UIEvents : MonoBehaviour
{
    private PanelRenderer _panelRenderer;
    private Label _sailsRotation;
    private Label _sailsUnfurling;

    private void OnEnable()
    {
        _panelRenderer = GetComponent<PanelRenderer>();
        _panelRenderer.RegisterUIReloadCallback(OnUIReload);
    }

    private void OnDisable()
    {
        _panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement root, int version)
    {
        var container = root.Q<VisualElement>("SailsUnfurling");
        _sailsUnfurling = container.Q<Label>("Value");
        container = root.Q<VisualElement>("SailsRotation");
        _sailsRotation = container.Q<Label>("Value");
    }

    public void SetSailsRotation(string value)
    {
        if(_sailsRotation != null)
            _sailsRotation.text = value;
    }

    public void SetSailsUnfurling(string value)
    {
        if (_sailsUnfurling != null)
            _sailsUnfurling.text = value;
    }
}
