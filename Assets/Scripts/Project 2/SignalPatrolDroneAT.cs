using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class SignalPatrolDroneAT : ActionTask<Transform>
{
    public BBParameter<Transform> playerTarget;
    public BBParameter<float> dangerDistance = 3f;

    protected override void OnExecute()
    {
        if (playerTarget.value == null)
        {
            EndAction(false);
            return;
        }

        float distance = Vector3.Distance(agent.position, playerTarget.value.position);

        if (distance <= dangerDistance.value)
        {
            DroneComboSystem.TriggerAlert(playerTarget.value.position);
            EndAction(true);
            return;
        }

        EndAction(false);
    }
}