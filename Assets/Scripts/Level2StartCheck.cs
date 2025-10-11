using DialogueScripts;
using UnityEngine;

public class Level2StartCheck : MonoBehaviour
{
    public GameObject robot;

    private void Start()
    {
        if (GlobalGameState.lazerHitRobot)
        {
            if (robot)
            {
                Destroy(robot);
            }
        }

        if (GlobalGameState.isRobotHacked)
        {
            if (robot)
            {
                Actions actions = robot.GetComponentInChildren<Actions>();
                actions.dialogue.hacked = true; 
            }
        }
    }
}