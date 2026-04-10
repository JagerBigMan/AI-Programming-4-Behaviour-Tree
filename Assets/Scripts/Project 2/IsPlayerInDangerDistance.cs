using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class IsPlayerInDangerDistance : ConditionTask<Transform>
{
    public BBParameter<Transform> playerTarget;     // player reference
    public BBParameter<float> dangerDistance = 3f;  // teleport trigger distance

    protected override bool OnCheck()
    {
        // fail if no player assigned
        if (playerTarget.value == null)
            return false;

        float distance = Vector3.Distance(agent.position, playerTarget.value.position);

        // return true if player is within danger distance
        return distance <= dangerDistance.value;
    }
}