using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTargetTask : ActionTask<Transform>
{
    public BBParameter<Transform> target;

    public BBParameter<float> stopDistance = 2f;

    private NavMeshAgent navAgent;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || target.value == null)
        {
            EndAction(false);
            return;
        }
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || target.value == null)
        {
            EndAction(false);
            return;
        }

        float distance = Vector3.Distance(agent.position, target.value.position);

        if (distance > stopDistance.value)
        {
            navAgent.isStopped = false;

            navAgent.SetDestination(target.value.position);
        }
        else
        {
            navAgent.isStopped = true;
        }
    }

    protected override void OnStop()
    {
        if (navAgent != null)
            navAgent.isStopped = false;           // Reset agent when leaving this task so it doesn't stay stuck
    }
}