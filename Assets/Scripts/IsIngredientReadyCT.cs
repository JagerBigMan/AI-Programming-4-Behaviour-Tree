using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("CookingRobot")]
[Description("Returns true if the ingredient is within detection range.")]
public class IsIngredientReadyCT : ConditionTask
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

    protected override bool OnCheck()
    {
        bool result = robot.IsIngredientInRange();
        return result;
    }
}