using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class RepairNodeDetection : ConditionTask<Transform>
{
    public BBParameter<float> searchRange = 20f;
    public BBParameter<LayerMask> nodeLayer;
    public BBParameter<Transform> targetNode;

    protected override bool OnCheck()
    {
        Collider[] hits = Physics.OverlapSphere(agent.position, searchRange.value, nodeLayer.value);

        float closestDistance = Mathf.Infinity;     //by starting off with a very large number (infinity) so the first valid node will be selected 
        RepairNode closestNode = null;                  //stores the closest valid repair node found so far

        foreach (Collider hit in hits)
        {
            RepairNode node = hit.GetComponent<RepairNode>();

            if (node == null || node.IsDestroyed())
                continue;

            float distance = Vector3.Distance(agent.position, node.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        if (closestNode != null)
        {
            targetNode.value = closestNode.transform;
            return true;
        }

        targetNode.value = null;
        return false;
    }
}