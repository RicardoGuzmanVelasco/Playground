using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    CircularIndex currentView = new(maxIndex: 3);

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
        ToggleViewOf<Canvas>(to: currentView == 0);
        ToggleViewOf<WorldViewSnake>(to: currentView == 1);
        ToggleViewOf<GuiRendererSnake>(to: currentView == 2);
    }
    
    void ToggleViewOf<T>(bool to) where T : Component
        => FindObjectOfType<T>(includeInactive: true).gameObject.SetActive(to); 
}