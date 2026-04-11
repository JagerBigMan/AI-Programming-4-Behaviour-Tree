using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAT : ActionTask<Transform>
{
    public Transform[] patrolPoints;

    public BBParameter<Transform> playerTarget;
    public BBParameter<float> detectionRange = 5f;

    public float waitTime = 1.5f;

    private NavMeshAgent navAgent;
    private int currentIndex = 0;
    private float timer = 0f;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || patrolPoints == null || patrolPoints.Length == 0)
        {
            EndAction(false);
            return;
        }

        navAgent.isStopped = false;
        navAgent.SetDestination(patrolPoints[currentIndex].position);
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || patrolPoints == null || patrolPoints.Length == 0)
        {
            EndAction(false);
            return;
        }

        // Check for player detection
        if (playerTarget.value != null)
        {
            float distanceToPlayer = Vector3.Distance(agent.position, playerTarget.value.position);

            if (distanceToPlayer <= detectionRange.value)
            {
                navAgent.isStopped = true;
                EndAction(false);
                return;
            }
        }

        // Reached patrol point
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                currentIndex = (currentIndex + 1) % patrolPoints.Length;

                navAgent.SetDestination(patrolPoints[currentIndex].position);
                timer = 0f;
            }
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