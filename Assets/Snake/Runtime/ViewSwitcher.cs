using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    const int TotalViews = 3;
    int currentViewIndex = 3;
    
    bool CanvasView => currentViewIndex % TotalViews == 0;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SwitchView();
    }

    void SwitchView()
    {
        currentViewIndex++;
        
        FindObjectOfType<Canvas>(includeInactive: true).gameObject.SetActive(CanvasView);
    }
}