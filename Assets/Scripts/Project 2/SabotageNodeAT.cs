using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class SabotageNode : ActionTask<Transform>
{
    public BBParameter<Transform> targetNode;                   //blackboard variable
    public BBParameter<float> sabotageRange = 2f;
    public BBParameter<float> sabotageDamagePerSecond = 15f;

    private RepairNode repairNode;

    protected override void OnExecute()
    {
        if (targetNode.value == null)
        {
            EndAction(false);
            return;
        }

        repairNode = targetNode.value.GetComponent<RepairNode>();

        if (repairNode == null)
        {
            EndAction(false);
            return;
        }
    }

    protected override void OnUpdate()
    {
        if (targetNode.value == null || repairNode == null)
        {
            EndAction(false);
            return;
        }

        float distance = Vector3.Distance(agent.position, targetNode.value.position);

        if (distance > sabotageRange.value)     //stops the agent if it's out of the sabotage range
        {
            EndAction(false);
            return;
        }

        repairNode.TakeDamage(sabotageDamagePerSecond.value * Time.deltaTime);      //apply continuous damage over time per second
    }
}