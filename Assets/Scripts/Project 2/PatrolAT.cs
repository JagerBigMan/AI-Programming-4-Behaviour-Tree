using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTask : ActionTask<Transform>
{
    public Transform[] patrolPoints;

    public BBParameter<Transform> playerTarget;
    public BBParameter<float> detectionRange = 5f;

    public float waitTime = 1.5f;

    public float rushSpeed = 7f;   // added a rushSpeed when the drone hits the third patrol point and returns to the first point.

    private NavMeshAgent navAgent;
    private int currentIndex = 0;
    private float timer = 0f;

    private float normalSpeed;
    private bool isRushingToFirst = false;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || patrolPoints == null || patrolPoints.Length == 0)
        {
            EndAction(false);
            return;
        }

        normalSpeed = navAgent.speed;

        navAgent.isStopped = false;
        navAgent.speed = normalSpeed;
        navAgent.SetDestination(patrolPoints[currentIndex].position);
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || patrolPoints == null || patrolPoints.Length == 0)
        {
            EndAction(false);
            return;
        }

        if (playerTarget.value != null)
        {
            float distanceToPlayer = Vector3.Distance(agent.position, playerTarget.value.position);

            if (distanceToPlayer <= detectionRange.value)
            {
                navAgent.isStopped = true;
                navAgent.speed = normalSpeed;
                EndAction(false);
                return;
            }
        }

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                int previousIndex = currentIndex;
                currentIndex = (currentIndex + 1) % patrolPoints.Length;

                // If leaving the 3rd patrol point (index 2) and going to the 1st patrol point (index 0),
                // use rush speed for this move only
                if (previousIndex == 2 && currentIndex == 0)
                {
                    navAgent.speed = rushSpeed;
                    isRushingToFirst = true;
                }
                else
                {
                    navAgent.speed = normalSpeed;
                    isRushingToFirst = false;
                }

                navAgent.SetDestination(patrolPoints[currentIndex].position);
                timer = 0f;
            }
        }

        if (isRushingToFirst && !navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)         // Once the rush trip is done, restore normal speed
        {
            navAgent.speed = normalSpeed;
            isRushingToFirst = false;
        }
    }

    protected override void OnStop()
    {
        if (navAgent != null)
        {
            navAgent.isStopped = false;
            navAgent.speed = normalSpeed;
        }
    }
}