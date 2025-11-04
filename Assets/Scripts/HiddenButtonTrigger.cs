using UnityEngine;

public class HiddenButtonTrigger : MonoBehaviour
{
    public GameObject lazer;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();

            if (lazer)
                lazer.SetActive(false);
            
            gameObject.SetActive(false);
        }
    }
}