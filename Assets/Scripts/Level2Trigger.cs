using UnityEngine;
using UnityEngine.SceneManagement; 
public class Level2Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
             SceneManager.LoadScene("PrototypeLevel2");
        }
    }
}
