using UnityEngine;
public class LazerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            GlobalGameState.lazerHitRobot = true;

            // NEW: if this robot was hacked, give control back to player
            if (HackManager.Instance)
                HackManager.Instance.NotifyActorKilled(other.gameObject);

            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Box"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
