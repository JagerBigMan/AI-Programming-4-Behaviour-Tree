using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("CookingRobot")]
[Description("Sets robot to fail state.")]
public class SetFailStateAT : ActionTask
{
    private CookingRobotController robot;

    protected override string OnInit()
    {
        robot = agent.GetComponent<CookingRobotController>();

        if (robot == null)
        {
            return "CookingRobotController not found.";
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

        robot.SetFailState();
        EndAction(true);
    }
}