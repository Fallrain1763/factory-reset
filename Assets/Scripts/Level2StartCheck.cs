using DialogueScripts;
using UnityEngine;

public class Level2StartCheck : MonoBehaviour
{
    public GameObject robot;

    private void Start()
    {
        GlobalGameState.isLevel2 = true;
        GlobalGameState.lazerHitRobot = GlobalGameState.stateSaver.Dequeue();
        GlobalGameState.isRobotHacked = GlobalGameState.stateSaver.Dequeue();
        
        if (GlobalGameState.lazerHitRobot)
        {
            if (robot)
            {
                Destroy(robot);
            }
        }

        if (GlobalGameState.isRobotHacked)
        {
            if (GlobalGameState.isLowBranching)
            {
                if (robot)
                {
                    Destroy(robot);
                }
            }
            
            if (robot)
            {
                DialogueHolder dialHol = robot.GetComponentInChildren<DialogueHolder>();
                dialHol.dialogue.hacked = true; 
            }
        }
    }
}