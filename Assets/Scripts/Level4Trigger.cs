using UnityEngine;
using UnityEngine.SceneManagement; 

public class Level4Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameState.stateSaver[2] = GlobalGameState.lazerHitRobot2;
            GlobalGameState.stateSaver[3] = (GlobalGameState.isRobotHacked2);
            GlobalGameState.stateSaver[4] = (GlobalGameState.isRobotSaved);
            GlobalGameState.isLevel3 = false;
            SceneManager.LoadScene("PrototypeLevel4");
        }
    }
}
