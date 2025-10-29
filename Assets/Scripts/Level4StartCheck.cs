using UnityEngine;

public class Level4StartCheck : MonoBehaviour
{
    public GameObject robot;
    public GameObject robot2;

    private void Start()
    {
        GlobalGameState.isLevel4 = true;
        
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
        
        if (GlobalGameState.lazerHitRobot2)
        {
            if (robot2)
            {
                Destroy(robot2);
            }
        }
        
        if (!GlobalGameState.isRobotSaved)
        {
            if (robot2)
            {
                Destroy(robot2);
            }
        }

        if (GlobalGameState.isRobotHacked2)
        {
            if (robot2)
            {
                Actions2 actions = robot2.GetComponentInChildren<Actions2>();
                actions.dialogue.hacked = true; 
            }
        }
    }
}