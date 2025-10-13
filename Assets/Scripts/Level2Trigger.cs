using UnityEngine;
using UnityEngine.SceneManagement; 
public class Level2Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             GlobalGameState.isLevel1 = false;
             SceneManager.LoadScene("PrototypeLevel2");
        }
    }
}
