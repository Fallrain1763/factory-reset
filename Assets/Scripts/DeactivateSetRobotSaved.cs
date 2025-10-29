using UnityEngine;

public class DeactivateSetRobotSaved : MonoBehaviour
{
    private bool isQuitting = false;

    private void OnApplicationQuit()
    {
        // When quitting the whole app, mark this flag so we don't change values on exit
        isQuitting = true;
    }
    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded || isQuitting) return;
            GlobalGameState.isRobotSaved = true;
            
        Debug.Log("✅ Object deactivated → GlobalGameState.isRobotSaved = true");

    }
}
