using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class PlayerTooClose : ConditionTask<Transform>
{
    public BBParameter<Transform> playerTarget;
    public BBParameter<float> fleeDistance = 5f;

    protected override bool OnCheck()
    {
        if (playerTarget.value == null) return false;

        float distance = Vector3.Distance(agent.position, playerTarget.value.position);
        return distance <= fleeDistance.value;
    }
}