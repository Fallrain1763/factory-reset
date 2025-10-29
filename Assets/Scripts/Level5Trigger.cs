using UnityEngine;
using UnityEngine.SceneManagement; 

public class Level5Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameState.isLevel4 = false;
            SceneManager.LoadScene("PrototypeLevel5");
        }
    }
}