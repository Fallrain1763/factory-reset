using UnityEngine;
using UnityEngine.SceneManagement; 

public class Level3Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameState.stateSaver[0] = GlobalGameState.lazerHitRobot;
            GlobalGameState.stateSaver[1] = GlobalGameState.isRobotHacked;
            GlobalGameState.isLevel2 = false;
            SceneManager.LoadScene("PrototypeLevel3");
        }
    }
}
