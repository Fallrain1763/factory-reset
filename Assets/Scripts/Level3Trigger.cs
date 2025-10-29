using UnityEngine;
using UnityEngine.SceneManagement; 

public class Level3Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameState.isLevel2 = false;
            SceneManager.LoadScene("PrototypeLevel3");
        }
    }
}
