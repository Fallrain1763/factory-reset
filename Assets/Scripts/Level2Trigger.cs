using UnityEngine;
using UnityEngine.SceneManagement; 
public class Level2Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             GlobalGameState.isLevel1 = false;
             GlobalGameState.stateSaver.Enqueue(GlobalGameState.lazerHitRobot);
             GlobalGameState.stateSaver.Enqueue(GlobalGameState.isRobotHacked);
             SceneManager.LoadScene("PrototypeLevel2");
        }
    }
}
