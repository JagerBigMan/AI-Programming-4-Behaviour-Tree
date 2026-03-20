using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("CookingRobot")]
[Description("Returns true if the ingredient is ready.")]
public class IsIngredientReadyCT : ConditionTask
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

    protected override bool OnCheck()
    {
        Debug.Log("Checking ingredientReady: " + robot.ingredientReady);
        return robot.ingredientReady;
    }
}