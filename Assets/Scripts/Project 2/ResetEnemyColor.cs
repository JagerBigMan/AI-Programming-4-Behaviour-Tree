using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class ResetEnemyColor : ActionTask<Transform>
{
    public BBParameter<Color> normalColor = Color.white;

    protected override void OnExecute()
    {
        if (agent == null)
        {
            EndAction(false);
            return;
        }

        Renderer[] renderers = agent.GetComponentsInChildren<Renderer>();       // Get all renderers on this object and its children

        if (renderers == null || renderers.Length == 0)
        {
            EndAction(false);
            return;
        }

        // Apply color to every mesh
        foreach (Renderer rend in renderers)
        {
            rend.material.color = normalColor.value;
        }

        EndAction(true);
    }
}