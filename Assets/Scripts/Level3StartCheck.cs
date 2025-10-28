using DialogueScripts;
using UnityEngine;

public class Level3StartCheck : MonoBehaviour
{
    public GameObject robot;

    private void Start()
    {
        GlobalGameState.isLevel3 = true;
        
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