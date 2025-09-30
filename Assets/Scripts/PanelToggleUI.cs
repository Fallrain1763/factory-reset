using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PanelToggleUI : MonoBehaviour
{
    public GameObject panel;                 
    public Selectable firstSelectable;       
    public GridMovement playerMovement;      
    
    public KeyCode toggleKey = KeyCode.Space;

    private bool _isOpen = false;

    void Update()
    {
        // NEW: while hacking, ignore the Space toggle here
        if (HackManager.IsHacking) return;

        if (Input.GetKeyDown(toggleKey))
        {
            if (!_isOpen) OpenPanel();
            else { SubmitCurrentSelection(); ClosePanel(); }
        }
    }

    void OpenPanel()
    {
        if (!panel) return;

        panel.SetActive(true);
        _isOpen = true;
        
        if (playerMovement) playerMovement.SetPaused(true);
        
        StartCoroutine(FocusNextFrame());
    }

    IEnumerator FocusNextFrame()
    {
        yield return null; 
        yield return new WaitForEndOfFrame();

        if (!EventSystem.current) yield break;
        EventSystem.current.sendNavigationEvents = true;

        EventSystem.current.SetSelectedGameObject(null);
        if (firstSelectable && firstSelectable.IsActive() && firstSelectable.interactable)
        {
            firstSelectable.Select(); 
        }
    }

    void ClosePanel()
    {
        if (!panel) return;

        panel.SetActive(false);
        _isOpen = false;


        if (EventSystem.current)
            EventSystem.current.SetSelectedGameObject(null);


        if (playerMovement) playerMovement.SetPaused(false);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void SubmitCurrentSelection()
    {
        if (!EventSystem.current) return;

        GameObject current = EventSystem.current.currentSelectedGameObject;
        
        if (!current && firstSelectable)
            current = firstSelectable.gameObject;

        if (!current) return;
        
        var btn = current.GetComponent<Button>();
        if (btn && btn.interactable)
        {
            btn.onClick?.Invoke();
            return;
        }
        
        if (current)
            ExecuteEvents.Execute<ISubmitHandler>(
                current,
                new BaseEventData(EventSystem.current),
                ExecuteEvents.submitHandler
            );
    }
}