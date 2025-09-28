using UnityEngine;
public class LazerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            GlobalGameState.lazerHitRobot = true;
            
            other.gameObject.SetActive(false); 
            gameObject.SetActive(false);
            
        }
    }
}
