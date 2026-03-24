using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

[Category("Custom/Actions")]
[Description("Stops movement, faces the target, waits briefly, then ends so chase can begin.")]
public class WaitAndFaceTarget : ActionTask<Transform>
{
    public BBParameter<Transform> target;

    public BBParameter<float> waitTime = 1f;

    public float rotateSpeed = 8f;

    private float timer;
    private NavMeshAgent navAgent;

    protected override void OnExecute()
    {
        timer = 0f;
        navAgent = agent.GetComponent<NavMeshAgent>();


        if (navAgent != null)               // Stop movement during the telegraphing phase
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
        }
    }

    protected override void OnUpdate()
    {
        if (agent == null || target.value == null)
        {
            EndAction(false);
            return;
        }

        Vector3 direction = (target.value.position - agent.position).normalized;        // Keep the enemy facing the player while waiting
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            agent.rotation = Quaternion.Slerp(agent.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        }

        timer += Time.deltaTime;

        if (timer >= waitTime.value)
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