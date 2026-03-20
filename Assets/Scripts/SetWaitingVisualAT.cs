using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("CookingRobot")]
[Description("Sets the robot to waiting state.")]
public class SetWaitingVisualAT : ActionTask
{
    private CookingRobotController robot;

    protected override string OnInit()
    {
        robot = agent.GetComponent<CookingRobotController>();

        if (robot == null)
        {
            return "CookingRobotController not found on this GameObject.";
        }

        return null;
    }

    protected override void OnExecute()
    {
        if (robot == null)
        {
            EndAction(false);
            return;
        }

        robot.SetWaitingState();
        Debug.Log("Robot changed to waiting state.");
        EndAction(true);
    }
}