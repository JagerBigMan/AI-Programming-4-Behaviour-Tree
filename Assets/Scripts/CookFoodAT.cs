using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("CookingRobot")]
[Description("Simulates cooking food for a short duration.")]
public class CookFoodAT : ActionTask
{
    public float cookDuration = 3f;

    private CookingRobotController robot;
    private float timer;

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

        timer = 0f;
        robot.SetCookingState();
        Debug.Log("Cooking started.");
    }

    protected override void OnUpdate()
    {
        if (robot == null)
        {
            EndAction(false);
            return;
        }

        timer += Time.deltaTime;

        if (timer >= cookDuration)
        {
            Debug.Log("Cooking finished.");
            EndAction(true);
        }
    }

    protected override void OnStop()
    {
        if (robot != null)
        {
            robot.isCooking = false;
        }
    }
}