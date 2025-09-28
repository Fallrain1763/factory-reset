using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float gridSize = 1f;

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

        System.Func<KeyCode, bool> input = Input.GetKeyDown;

        if (input(KeyCode.W)) StartCoroutine(Move(Vector2.up));
        else if (input(KeyCode.S)) StartCoroutine(Move(Vector2.down));
        else if (input(KeyCode.A)) StartCoroutine(Move(Vector2.left));
        else if (input(KeyCode.D)) StartCoroutine(Move(Vector2.right));
    }

    private IEnumerator Move(Vector2 direction)
    {
        _isMoving = true;

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        transform.position = endPosition;
        _isMoving = false;
    }
}