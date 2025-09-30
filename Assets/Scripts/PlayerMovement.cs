using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private LayerMask blockingLayer; // <- assign in Inspector (walls, NPCs, etc.)

    private bool _isMoving = false;
    public bool IsPaused { get; private set; } = false;

    public void SetPaused(bool paused)
    {
        IsPaused = paused;
    }

    private void Update()
    {
        if (IsPaused) return;
        if (_isMoving) return;

        // Input handler: WASD + Arrow Keys
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            TryMove(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            TryMove(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            TryMove(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            TryMove(Vector2.right);
    }

    private void TryMove(Vector2 direction)
    {
        Vector2 targetPos = (Vector2)transform.position + direction * gridSize;

        // Check for collision
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.1f, blockingLayer);
        if (hit == null)
        {
            StartCoroutine(Move(targetPos));
        }
        else
        {
            Debug.Log("Blocked by: " + hit.name);
        }
    }

    private IEnumerator Move(Vector2 targetPos)
    {
        _isMoving = true;

        Vector2 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            transform.position = Vector2.Lerp(startPosition, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        _isMoving = false;
    }
}
