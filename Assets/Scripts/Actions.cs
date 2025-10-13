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
    [SerializeField] private DialogueManager dialogueManager;

    public GridMovement npcMovement;                 // set by PanelToggleUI.SetTarget(...)
    [SerializeField] private PanelToggleUI panel;    // drag your PanelToggleUI here
    private bool _hiddenActivated = false;
    private bool _goToPressurePlate= false;

    void Start()
    {
        if (hackButton) hackButton.onClick.AddListener(OnHack);
        if (talkButton) talkButton.onClick.AddListener(OnTalk);
    }

    public void SetTarget(GridMovement npc)
    {
        npcMovement = npc;
    }

    private void Update()
    {
        if (hiddenButton && !GlobalGameState.dialogueActive && _hiddenActivated && !GlobalGameState.isRobotHacked)
            hiddenButton.SetActive(true);
        
        if (!GlobalGameState.isRobotHacked && !GlobalGameState.dialogueActive && _goToPressurePlate )
            transform.position = new Vector3(5.5f, 3.5f, 0f);
    }

    private void OnTalk()
    {
        Debug.Log($"[Actions] Talk with dialogue: {dialogue.name}");
        dialogueManager.StartDialogue(dialogue);
        Debug.Log("[Actions] Talk");
        panel?.ClosePanel("[Actions] Close after Talk");
       // if (GlobalGameState.isLevel1) 
            _hiddenActivated = true;
        if (GlobalGameState.isLevel2) _goToPressurePlate = true;
    }

    private void OnHack()
    {
        dialogue.hacked = true;
        GlobalGameState.isRobotHacked = true;
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
