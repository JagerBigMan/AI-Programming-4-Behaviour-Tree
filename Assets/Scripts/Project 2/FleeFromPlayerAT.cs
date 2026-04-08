using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class FleeFromPlayer : ActionTask<Transform>
{
    public BBParameter<Transform> playerTarget;
    public BBParameter<float> fleeDistance = 8f;

    private NavMeshAgent navAgent;
    private Vector3 fleeDestination;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || playerTarget.value == null)
        {
            EndAction(false);
            return;
        }

        Vector3 fleeDirection = (agent.position - playerTarget.value.position).normalized;
        fleeDestination = agent.position + fleeDirection * fleeDistance.value;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeDestination, out hit, 5f, NavMesh.AllAreas))
        {
            fleeDestination = hit.position;
        }

        navAgent.isStopped = false;
        navAgent.SetDestination(fleeDestination);
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || playerTarget.value == null)
        {
            EndAction(false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(agent.position, playerTarget.value.position);

        if (distanceToPlayer > fleeDistance.value)
        {
            EndAction(true);
            return;
        }

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