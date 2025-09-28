using UnityEngine;

public class HiddenButtonTrigger : MonoBehaviour
{
    public GameObject lazer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (lazer)
                lazer.SetActive(false);
            
            gameObject.SetActive(false);
        }
    }
}