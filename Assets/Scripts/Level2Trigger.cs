using UnityEngine;
using UnityEngine.SceneManagement; 
public class Level2Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             GlobalGameState.isLevel1 = false;
             GlobalGameState.stateSaver[0] = GlobalGameState.lazerHitRobot;
             GlobalGameState.stateSaver[1] = GlobalGameState.isRobotHacked;
             SceneManager.LoadScene("PrototypeLevel2");
        }
    }
}
