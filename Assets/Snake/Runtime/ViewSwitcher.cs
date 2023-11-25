using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    const int TotalViews = 3;
    int currentViewIndex = 3;
    
    bool CanvasViewIsEnabled => currentViewIndex % TotalViews == 0;
    bool WorldViewIsEnabled => currentViewIndex % TotalViews == 1;
    bool RendererViewIsEnabled => currentViewIndex % TotalViews == 2;

    void Awake() => ToggleActiveView();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SwitchView();
    }

    void SwitchView()
    {
        currentViewIndex++;
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