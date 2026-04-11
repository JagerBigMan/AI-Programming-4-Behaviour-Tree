using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class MoveToAlertPositionAT : ActionTask<Transform>
{
    public BBParameter<float> stopDistance = 2f;

    private NavMeshAgent navAgent;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || !DroneComboSystem.alertActive)
        {
            EndAction(false);
            return;
        }

        navAgent.isStopped = false;
        navAgent.stoppingDistance = stopDistance.value;
        navAgent.SetDestination(DroneComboSystem.alertPosition);
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || !DroneComboSystem.alertActive)
        {
            EndAction(false);
            return;
        }

        navAgent.SetDestination(DroneComboSystem.alertPosition);

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            EndAction(true);
        }
    }

    protected override void OnStop()
    {
        if (navAgent != null)
        {
            navAgent.isStopped = false;
        }
    }
}