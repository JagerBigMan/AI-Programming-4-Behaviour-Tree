using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class FleeFromPlayer : ActionTask<Transform>
{
    public BBParameter<Transform> playerTarget;
    public BBParameter<float> fleeDistance = 8f;
    public BBParameter<int> maxAttempts = 10;
    public BBParameter<float> repathInterval = 0.2f;

    private NavMeshAgent navAgent;
    private Vector3 fleeDestination;
    private float nextRepathTime;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || playerTarget.value == null)
        {
            EndAction(false);
            return;
        }

        navAgent.isStopped = false;
        nextRepathTime = 0f;

        if (!TryFindFleePoint())
        {
            EndAction(false);
            return;
        }

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

        // stop fleeing when far enough away
        if (distanceToPlayer > fleeDistance.value)
        {
            EndAction(true);
            return;
        }

        // refresh flee target repeatedly using current player position
        if (Time.time >= nextRepathTime)
        {
            nextRepathTime = Time.time + repathInterval.value;

            if (TryFindFleePoint())
            {
                navAgent.SetDestination(fleeDestination);
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

    private bool TryFindFleePoint()
    {
        Vector3 awayDir = (agent.position - playerTarget.value.position).normalized;

        for (int i = 0; i < maxAttempts.value; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            Vector3 randomDir = new Vector3(randomCircle.x, 0f, randomCircle.y);

            // reject directions that go too much toward the player
            float dot = Vector3.Dot(randomDir, awayDir);
            if (dot < 0.3f)
                continue;

            Vector3 candidate = agent.position + randomDir * fleeDistance.value;

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                fleeDestination = hit.position;
                return true;
            }
        }

        // fallback: pure opposite direction
        Vector3 fallback = agent.position + awayDir * fleeDistance.value;

        if (NavMesh.SamplePosition(fallback, out NavMeshHit fallbackHit, 2f, NavMesh.AllAreas))
        {
            fleeDestination = fallbackHit.position;
            return true;
        }

        return false;
    }
}