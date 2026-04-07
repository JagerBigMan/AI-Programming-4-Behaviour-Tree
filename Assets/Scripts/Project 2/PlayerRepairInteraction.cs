using UnityEngine;

public class PlayerRepairInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionRange = 3f;
    public LayerMask repairLayer;
    public KeyCode interactKey = KeyCode.E;

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
        Vector3 center = transform.position;

        Collider[] hits = Physics.OverlapSphere(center, interactionRange, repairLayer);

        currentNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            RepairNode node = hit.GetComponent<RepairNode>();

            if (node == null || node.IsDestroyed())
                continue;

            float distance = Vector3.Distance(center, node.transform.position);

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
}