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

        navAgent.isStopped = false;     //making sure movement is enabled
        nextRepathTime = 0f;                //it can repath immediately on first update if needed

        if (!TryFindFleePoint())        //looking for valid flee destination
        {
            EndAction(false);           //if it can't, it fails
            return;
        }

        navAgent.SetDestination(fleeDestination);       //starts moving there if a valid flee point is found
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || playerTarget.value == null)
        {
            EndAction(false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(agent.position, playerTarget.value.position);

        if (distanceToPlayer > fleeDistance.value)         // stop fleeing when far enough away
        {
            EndAction(true);
            return;
        }

        if (Time.time >= nextRepathTime)          // refresh flee target repeatedly using current player position
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
        Vector3 awayDir = (agent.position - playerTarget.value.position).normalized;        //creates a unit vector pointing, if the player is south of the drone, the awayDir would point north.

        for (int i = 0; i < maxAttempts.value; i++)                 //trying random directions
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            Vector3 randomDir = new Vector3(randomCircle.x, 0f, randomCircle.y);

            float dot = Vector3.Dot(randomDir, awayDir);             // reject directions that go too much toward the player
            if (dot < 0.3f)
                continue;                               //It would almost always choose the direction in the half/front arc from the player, this prevents moving into the player

            Vector3 candidate = agent.position + randomDir * fleeDistance.value;        //This creates a point fleeDistance away from the current position in that random direction

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2f, NavMesh.AllAreas))        //snap it to the navmesh
            {
                fleeDestination = hit.position;
                return true;
            }
        }

        Vector3 fallback = agent.position + awayDir * fleeDistance.value;         // if all attempts fail, then create a point directly opposite to the player, last line of defense

        if (NavMesh.SamplePosition(fallback, out NavMeshHit fallbackHit, 2f, NavMesh.AllAreas))
        {
            fleeDestination = fallbackHit.position;
            return true;
        }

        return false;
    }
}