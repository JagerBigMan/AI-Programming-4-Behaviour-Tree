using UnityEngine;

public class PlayerRepairInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionRange = 3f;          // how far player can interact with nodes
    public LayerMask repairLayer;                
    public KeyCode interactKey = KeyCode.E;      

    [Header("Repair Settings")]
    public float requiredHoldTime = 2f;          // must hold key for 2 seconds to repair

    private RepairNode currentNode;              
    private RepairNode repairingNode;            
    private float repairTimer = 0f;              // how long player has been holding key

    private void Update()
    {
        DetectClosestRepairNode();                              // constantly check for nearest repairable node
        bool canRepairCurrentNode =                                          // check if current node is valid for repair
            currentNode != null &&
            !currentNode.IsDestroyed() &&
            !currentNode.IsFullyRepaired();

        if (canRepairCurrentNode && Input.GetKey(interactKey))        // if player is holding the interact key and node is valid
        {
            if (repairingNode != currentNode)            // if we switched to a different node, reset timer
            {
                repairingNode = currentNode;
                repairTimer = 0f;
            }

            repairTimer += Time.deltaTime;            // increase timer while holding key

            if (repairTimer >= requiredHoldTime)            // once timer reaches required hold time, repair completes
            {
                repairingNode.FullyRepair();   // fully repair the node
                repairingNode = null;                // reset state
                repairTimer = 0f;
            }
        }
        else
        {
            repairingNode = null;            // if key released OR no valid node → reset progress
            repairTimer = 0f;
        }
    }

    private void DetectClosestRepairNode()
    {
        Vector3 center = transform.position;

        Collider[] hits = Physics.OverlapSphere(center, interactionRange, repairLayer);          // find all colliders within interaction range on repair layer

        currentNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            RepairNode node = hit.GetComponent<RepairNode>();

            if (node == null || node.IsDestroyed())            // skip invalid or destroyed nodes
                continue;

            float distance = Vector3.Distance(center, node.transform.position);    // check distance to player

            if (distance < closestDistance)     // keep track of closest valid node
            {
                closestDistance = distance;
                currentNode = node;
            }
        }
    }
    public RepairNode GetCurrentNode()          // returns the current node player is targeting
    {
        return currentNode;
    }

    public float GetRepairProgress01() 
    {
        if (repairingNode == null) return 0f;

        return Mathf.Clamp01(repairTimer / requiredHoldTime);
    }
}