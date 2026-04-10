using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class ResetCooldownVisual : ActionTask<Transform>
{
    private Renderer droneRenderer;

    protected override void OnExecute()
    {
        droneRenderer = agent.GetComponentInChildren<Renderer>();

        if (droneRenderer == null)
        {
            EndAction(false);
            return;
        }

        // Reset to original color (assumes default was white or material default)
        droneRenderer.material.color = Color.gray;

        EndAction(true);
    }
}