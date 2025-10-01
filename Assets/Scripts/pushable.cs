using UnityEngine;
using System.Collections;
public class Pushable : MonoBehaviour
{
    [SerializeField] private float gridSize = 1f;
    public void Push(Vector2 direction)
    {
        // Convert to float Vector2
        Vector2 move = new Vector2(direction.x, direction.y) * gridSize;

        // Check if target space is blocked before moving
        Vector2 targetPos = (Vector2)transform.position + move;

        // Optional: add collision/physics check
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.1f, LayerMask.GetMask("Blocking"));
        if (hit == null || hit.CompareTag("Lazer"))
        {
            StartCoroutine(Move(targetPos));
        }
        else
        {
            Debug.Log("Blocked! Can't push into " + hit.name);
        }
    }

    private IEnumerator Move(Vector2 targetPos)
    {

        Vector2 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < 0.1f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.1f;
            transform.position = Vector2.Lerp(startPosition, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
    }
}