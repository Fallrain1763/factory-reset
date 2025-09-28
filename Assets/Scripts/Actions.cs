using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    public Button talkButton;
    public Button hackButton;
    
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
    }
}