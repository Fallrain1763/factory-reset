using System;
using UnityEngine;

public class Level4StartCheck : MonoBehaviour
{
    public GameObject robot;
    public GameObject robot2;

    private void Start()
    {
        GlobalGameState.isLevel3 = false;
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
                DialogueHolder dialHol = robot.GetComponentInChildren<DialogueHolder>();
                dialHol.dialogue.hacked = true;
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
                Actions actions = robot2.GetComponentInChildren<Actions>();
                DialogueHolder dialHol = robot2.GetComponentInChildren<DialogueHolder>();
                dialHol.dialogue.hacked = true;
            }
        }
    }
}