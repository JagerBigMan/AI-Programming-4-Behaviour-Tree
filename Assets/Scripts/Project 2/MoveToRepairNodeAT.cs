using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class MoveToRepairNode : ActionTask<Transform>
{
    public BBParameter<Transform> targetNode;
    public float stoppingDistance = 1.5f;

    private NavMeshAgent navAgent;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || targetNode.value == null)
        {
            EndAction(false);
            return;
        }

        navAgent.isStopped = false;
        navAgent.stoppingDistance = stoppingDistance;
        navAgent.SetDestination(targetNode.value.position);     //sets the position of the node as the destination
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || targetNode.value == null)
        {
            EndAction(false);
            return;
        }

        navAgent.SetDestination(targetNode.value.position);

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)                   //stops when it arrives
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