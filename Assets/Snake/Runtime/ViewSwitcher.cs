using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    CircularIndex currentView = new(maxIndex: 3);
    
    bool CanvasViewIsEnabled => currentView == 0;
    bool WorldViewIsEnabled => currentView == 1;
    bool RendererViewIsEnabled => currentView == 2;

    void Awake() => ToggleActiveView();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SwitchView();
    }

    void SwitchView()
    {
        currentView.Increment();
        ToggleActiveView();
    }

    void ToggleActiveView()
    {
        ToggleViewOf<Canvas>(to: CanvasViewIsEnabled);
        ToggleViewOf<WorldViewSnake>(to: WorldViewIsEnabled);
        ToggleViewOf<GuiRendererSnake>(to: RendererViewIsEnabled);
    }
    
    void ToggleViewOf<T>(bool to) where T : Component
        => FindObjectOfType<T>(includeInactive: true).gameObject.SetActive(to); 
}