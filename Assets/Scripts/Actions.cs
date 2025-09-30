using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    public Button talkButton;
    public Button hackButton;

    // NEW: assign these in the inspector for the current interaction
    public GridMovement playerMovement;
    public GridMovement npcMovement;

    public GameObject hiddenButton;

    private void Start()
    {
        if (talkButton)
            talkButton.onClick.AddListener(OnTalk);

        if (hackButton)
            hackButton.onClick.AddListener(OnHack);
    }

    private void OnTalk()
    {
        Debug.Log("Talk button clicked!");
        if (hiddenButton)
            hiddenButton.SetActive(true);
    }

    private void OnHack()
    {
        Debug.Log("Hack button clicked!");
        if (npcMovement && HackManager.Instance)
        {
            HackManager.Instance.BeginHack(npcMovement);
        }
        else
        {
            Debug.LogWarning("Hack failed: npcMovement or HackManager missing.");
        }
    }
}
