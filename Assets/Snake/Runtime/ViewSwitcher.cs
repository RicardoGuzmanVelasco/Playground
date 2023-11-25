using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    const int TotalViews = 3;
    int currentViewIndex = 3;
    
    bool CanvasView => currentViewIndex % TotalViews == 0;
    bool WorldView => currentViewIndex % TotalViews == 1;
    bool RendererView => currentViewIndex % TotalViews == 2;

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
        FindObjectOfType<Canvas>(includeInactive: true).gameObject.SetActive(CanvasView);
        FindObjectOfType<WorldViewSnake>(includeInactive: true).gameObject.SetActive(WorldView);
        FindObjectOfType<GuiRendererSnake>(includeInactive: true).gameObject.SetActive(RendererView);
    }
}