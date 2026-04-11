using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTargetAT : ActionTask<Transform>
{
    public BBParameter<Transform> target;
    public BBParameter<float> stopDistance = 2f;
    public BBParameter<float> maxChaseTime = 5f;
    public BBParameter<float> handcuffDuration = 5f;

    private NavMeshAgent navAgent;
    private float chaseTimer;

    protected override void OnExecute()
    {
        navAgent = agent.GetComponent<NavMeshAgent>();

        if (navAgent == null || target.value == null)
        {
            EndAction(false);
            return;
        }

        chaseTimer = 0f;
        navAgent.isStopped = false;
    }

    protected override void OnUpdate()
    {
        if (navAgent == null || target.value == null)
        {
            EndAction(false);
            return;
        }

        chaseTimer += Time.deltaTime;

        float distance = Vector3.Distance(agent.position, target.value.position);

        if (distance <= stopDistance.value)         // If bot catches the target, apply handcuff debuff
        {
            navAgent.isStopped = true;

            PlayerController playerController = target.value.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ApplyHandcuffDebuff(handcuffDuration.value);
            }

            EndAction(true);
            return;
        }

        if (chaseTimer >= maxChaseTime.value)         // Stop chase if max chase time is reached
        {
            navAgent.isStopped = true;
            EndAction(false);
            return;
        }

        navAgent.isStopped = false;         // Keep chasing
        navAgent.SetDestination(target.value.position);
    }

    protected override void OnStop()
    {
        if (navAgent != null)
        {
            navAgent.isStopped = false;
        }
    }
}