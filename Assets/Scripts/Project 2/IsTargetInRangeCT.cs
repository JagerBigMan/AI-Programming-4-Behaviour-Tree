using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class IsTargetInRange : ConditionTask<Transform>
{
    public BBParameter<Transform> target;

    public BBParameter<float> range = 5f;

    protected override bool OnCheck()
    {
        if (agent == null || target.value == null)
            return false;

        float distance = Vector3.Distance(agent.position, target.value.position);        // Calculate distance between enemy and player

        return distance <= range.value;
    }
}