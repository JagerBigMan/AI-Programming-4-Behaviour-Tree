using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class ApplyComboDebuffAT : ActionTask<Transform>
{
    public BBParameter<Transform> playerTarget;
    public BBParameter<float> applyRange = 2f;
    public BBParameter<float> debuffDuration = 5f;

    protected override void OnExecute()
    {
        if (playerTarget.value == null)
        {
            EndAction(false);
            return;
        }

        float distance = Vector3.Distance(agent.position, playerTarget.value.position);

        if (distance > applyRange.value)
        {
            EndAction(false);
            return;
        }

        PlayerController playerController = playerTarget.value.GetComponent<PlayerController>();
        if (playerController == null)
        {
            EndAction(false);
            return;
        }

        playerController.ApplyHandcuffDebuff(debuffDuration.value);

        DroneComboSystem.UnfreezePlayer();
        DroneComboSystem.ClearAlert();

        EndAction(true);
    }
}