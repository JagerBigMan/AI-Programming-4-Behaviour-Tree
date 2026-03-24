using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTargetTask : ActionTask<Transform>
{
    public BBParameter<Transform> target;

    public BBParameter<float> stopDistance = 2f;

    public BBParameter<float> detectionRange = 5f;

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


        if (distance > detectionRange.value)                        // If the player leaves detection range, stop chasing
                                                                   // and end this action so the tree can return to patrol
        {
            navAgent.isStopped = true;
            EndAction(false);
            return;
        }


        if (distance > stopDistance.value)        // If still in detection range but not close enough, keep chasing
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(target.value.position);
        }
        else
        {

            navAgent.isStopped = true;            // Close enough to stop moving, but still keep this branch active
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