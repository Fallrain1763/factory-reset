using UnityEngine;

public class Level1StartCheck : MonoBehaviour
{
    private void Start()
    {
        GlobalGameState.lazerHitRobot = false;
        GlobalGameState.isLevel1 = true;
    }
}