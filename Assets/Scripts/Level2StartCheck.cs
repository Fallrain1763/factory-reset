using DialogueScripts;
using UnityEngine;

public class Level2StartCheck : MonoBehaviour
{
    public GameObject robot;

    private void Start()
    {
        GlobalGameState.isLevel2 = true;
        
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
                DialogueHolder dialHol = robot.GetComponentInChildren<DialogueHolder>();
                dialHol.dialogue.hacked = true; 
            }
        }
    }
}