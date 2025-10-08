using System;
using DialogueScripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Actions : MonoBehaviour
{
    public Button talkButton;
    public Button hackButton;
    public GameObject hiddenButton;
    public Dialogue dialogue;

    public GridMovement npcMovement;                 // set by PanelToggleUI.SetTarget(...)
    [SerializeField] private PanelToggleUI panel;    // drag your PanelToggleUI here

    void Start()
    {
        if (hackButton) hackButton.onClick.AddListener(OnHack);
        if (talkButton) talkButton.onClick.AddListener(OnTalk);
    }

    public void SetTarget(GridMovement npc)
    {
        npcMovement = npc;
    }
    
    private void OnTalk()
    {
        if (hiddenButton)
            hiddenButton.SetActive(true);
        
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Debug.Log("[Actions] Talk");
        panel?.ClosePanel("[Actions] Close after Talk");
    }

    private void OnHack()
    {
        if (!HackManager.Instance) { Debug.LogWarning("[Actions] HackManager missing."); return; }
        if (!npcMovement) { Debug.LogWarning("[Actions] No npcMovement set for hack."); return; }

        panel?.ClosePanel("[Actions] Close before Hack");
        HackManager.Instance.BeginHack(npcMovement);
    }

    // === Space bar submit support ===
    public void SubmitCurrentSelection()
    {
        var es = EventSystem.current;
        if (es && es.currentSelectedGameObject)
        {
            var btn = es.currentSelectedGameObject.GetComponent<Button>();
            if (btn)
            {
                btn.onClick.Invoke();     // triggers either Talk or Hack depending on selection
                return;
            }
        }

        // Fallback (if nothing is selected): prefer Hack; otherwise Talk
        if (hackButton) { hackButton.onClick.Invoke(); return; }
        if (talkButton) { talkButton.onClick.Invoke(); return; }
    }
}
