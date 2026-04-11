using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

public class TeleportToFurthestNode : ActionTask<Transform>
{
    public BBParameter<Transform> playerTarget;
    public BBParameter<Transform> nodeParent;
    public BBParameter<Transform> targetNode;

    public BBParameter<float> dangerDistance = 3f;
    public BBParameter<float> teleportCooldown = 10f;
    public BBParameter<float> teleportCooldownEndTime;

    private Renderer droneRenderer;
    private Color originalColor;

    protected override void OnExecute()
    {
        if (playerTarget.value == null || nodeParent.value == null)
        {
            EndAction(false);
            return;
        }

        float playerDistance = Vector3.Distance(agent.position, playerTarget.value.position);

        if (playerDistance > dangerDistance.value)
        {
            EndAction(false);
            return;
        }

        droneRenderer = agent.GetComponentInChildren<Renderer>();
        if (droneRenderer != null)
        {
            originalColor = droneRenderer.material.color;
        }

        float furthestDistance = 0f;
        Transform furthestNode = null;

        for (int i = 0; i < nodeParent.value.childCount; i++)                   //gets the child transform from the ParentNode, calculate the distance and then keep the one with the greatest distance
        {
            Transform nodeTransform = nodeParent.value.GetChild(i);

            RepairNode node = nodeTransform.GetComponent<RepairNode>();
            if (node == null || node.IsDestroyed())
                continue;

            float distance = Vector3.Distance(agent.position, nodeTransform.position);

            if (distance > furthestDistance)
            {
                furthestDistance = distance;
                furthestNode = nodeTransform;
            }
        }

        if (furthestNode == null)
        {
            EndAction(false);
            return;
        }

        targetNode.value = furthestNode;

        NavMeshAgent navAgent = agent.GetComponent<NavMeshAgent>();     //warp is safer than transform.position, the drone will instantly relocate to the chosen node
        if (navAgent != null)
        {
            navAgent.Warp(furthestNode.position); 
            navAgent.ResetPath();
            navAgent.isStopped = true;
        }
        else
        {
            agent.position = furthestNode.position;
        }

        teleportCooldownEndTime.value = Time.time + teleportCooldown.value;         // start cooldown

        if (droneRenderer != null)
        {
            droneRenderer.material.color = Color.red;       //turns red(indicating cooldown)
        }

        EndAction(true);
    }
}