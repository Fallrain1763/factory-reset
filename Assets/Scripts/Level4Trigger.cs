using UnityEngine;
using UnityEngine.SceneManagement; 

public class Level4Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameState.stateSaver.Enqueue(GlobalGameState.lazerHitRobot2);
            GlobalGameState.stateSaver.Enqueue(GlobalGameState.isRobotHacked2);
            GlobalGameState.stateSaver.Enqueue(GlobalGameState.isRobotSaved);
            GlobalGameState.isLevel3 = false;
            SceneManager.LoadScene("PrototypeLevel4");
        }
    }
}
