using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour {

  [SerializeField] private float moveDuration = 0.1f;
  [SerializeField] private float gridSize = 1f;

  private bool _isMoving = false;

  // Update is called once per frame
  private void Update() {
    // Only process on move at a time.
    if (!_isMoving) {
      System.Func<KeyCode, bool> inputFunction = Input.GetKeyDown;
      
      if (inputFunction(KeyCode.W)) {
        StartCoroutine(Move(Vector2.up));
      } else if (inputFunction(KeyCode.S)) {
        StartCoroutine(Move(Vector2.down));
      } else if (inputFunction(KeyCode.A)) {
        StartCoroutine(Move(Vector2.left));
      } else if (inputFunction(KeyCode.D)) {
        StartCoroutine(Move(Vector2.right));
      }
    }
  }

  // Movement between grid positions.
  private IEnumerator Move(Vector2 direction) {
    _isMoving = true;
    
    Vector2 startPosition = transform.position;
    Vector2 endPosition = startPosition + (direction * gridSize);
    
    float elapsedTime = 0;
    while (elapsedTime < moveDuration) {
      elapsedTime += Time.deltaTime;
      float percent = elapsedTime / moveDuration;
      transform.position = Vector2.Lerp(startPosition, endPosition, percent);
      yield return null;
    }
    
    transform.position = endPosition;
    
    _isMoving = false;
  }
}