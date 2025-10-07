using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private LayerMask blockingLayer;

    private bool _isMoving = false;

    // NEW: who is currently allowed to read input?
    public bool HasControl = true;
    private Vector2 direction;

    public bool IsPaused { get; private set; } = false;

    public void SetPaused(bool paused)
    {
        IsPaused = paused;
    }

    private void Update()
    {
        if (IsPaused) return;
        if (!HasControl) return;         // NEW: gate input when not controlled
        if (_isMoving) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            direction = Vector2.up;
            TryMove(direction);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            direction = Vector2.down;
            TryMove(direction);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction = Vector2.left;
            TryMove(direction);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            direction = Vector2.right;
            TryMove(direction);
        }
    }

    private void TryMove(Vector2 direction)
    {
        Vector2 targetPos = (Vector2)transform.position + direction * gridSize;

        var hits = Physics2D.OverlapCircleAll(targetPos, 0.1f, blockingLayer);

        bool blockedBySolid = false;
        Pushable pushableInFront = null;

        foreach (var h in hits)
        {
            if (!h) continue;

            bool isLaser = h.CompareTag("Laser");
            bool iAmRobot = CompareTag("Robot");

            // --- LASER RULES ---
            // Robots can step into lasers; everyone else is blocked by lasers.
            if (isLaser)
            {
                if (iAmRobot)
                {
                    // allow robot to move into laser cell
                    continue;
                }
                else
                {
                    blockedBySolid = true;  // player (or other) is blocked by laser
                    break;
                }
            }

            // Non-laser triggers don't block
            if (h.isTrigger) continue;

            // Pushable in front? remember it; don't mark blocked yet
            var p = h.GetComponentInParent<Pushable>();
            if (p != null)
            {
                pushableInFront = p;
                continue;
            }

            // Anything else on Blocking is a wall/solid
            blockedBySolid = true;
            break;
        }

        if (blockedBySolid) return;

        // Handle pushing a box (if any)
        if (pushableInFront != null)
        {
            Vector2 boxTarget = (Vector2)pushableInFront.transform.position + direction * gridSize;
            var boxHits = Physics2D.OverlapCircleAll(boxTarget, 0.1f, blockingLayer);

            bool boxBlocked = false;
            foreach (var h in boxHits)
            {
                if (!h) continue;

                // Boxes may pass into lasers if you want that behavior; otherwise remove this line.
                if (h.CompareTag("Laser")) continue;

                if (h.isTrigger) continue;

                boxBlocked = true;
                break;
            }

            if (boxBlocked) return;

            // Push the box first
            pushableInFront.Push(direction);
        }

        // Move actor
        StartCoroutine(Move(targetPos));
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
