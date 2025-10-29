using UnityEngine;

public class ControlCursorFollower : MonoBehaviour
{
    
    void LateUpdate()
    {

        GridMovement target = HackManager.Instance
            ? HackManager.Instance.GetCurrentControlled()
            : null;

        if (target)
        {

            transform.position = target.transform.position;

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
        else
        {
  
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}