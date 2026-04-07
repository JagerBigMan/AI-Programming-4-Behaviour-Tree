using UnityEngine;

public class PlayerRepairInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionRange = 3f;
    public LayerMask repairLayer;
    public KeyCode interactKey = KeyCode.E;

    [Header("Optional")]
    public Transform interactionCenter;
    public bool drawDebugSphere = true;

    private RepairNode currentNode;
    private RepairNode repairingNode;
    private float repairTimer = 0f;

    private void Update()
    {
        DetectClosestRepairNode();

        bool canRepairCurrentNode =
            currentNode != null &&
            !currentNode.IsDestroyed() &&
            !currentNode.IsFullyRepaired();

        if (canRepairCurrentNode && Input.GetKey(interactKey))
        {
            if (repairingNode != currentNode)
            {
                repairingNode = currentNode;
                repairTimer = 0f;
            }

            repairTimer += Time.deltaTime;

            if (repairTimer >= repairingNode.repairDuration)
            {
                repairingNode.FullyRepair();
                repairingNode = null;
                repairTimer = 0f;
            }
        }
        else
        {
            repairingNode = null;
            repairTimer = 0f;
        }
    }

    private void DetectClosestRepairNode()
    {
        Transform center = interactionCenter != null ? interactionCenter : transform;

        Collider[] hits = Physics.OverlapSphere(center.position, interactionRange, repairLayer);

        currentNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            RepairNode node = hit.GetComponent<RepairNode>();

            if (node == null || node.IsDestroyed())
                continue;

            float distance = Vector3.Distance(center.position, node.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentNode = node;
            }
        }
    }

    public RepairNode GetCurrentNode()
    {
        return currentNode;
    }

    public float GetRepairProgress01()
    {
        if (repairingNode == null) return 0f;
        return Mathf.Clamp01(repairTimer / repairingNode.repairDuration);
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawDebugSphere) return;

        Transform center = interactionCenter != null ? interactionCenter : transform;

        Gizmos.color = currentNode != null ? Color.green : Color.red;
        Gizmos.DrawWireSphere(center.position, interactionRange);
    }
}