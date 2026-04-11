using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class SetEnemyColor : ActionTask<Transform>
{
    public BBParameter<Color> color = Color.yellow;

    protected override void OnExecute()
    {
        if (agent == null)
        {
            EndAction(false);
            return;
        }

        Renderer[] renderers = agent.GetComponentsInChildren<Renderer>();

        if (renderers == null || renderers.Length == 0)
        {
            EndAction(false);
            return;
        }

        foreach (Renderer render in renderers)
        {
            render.material.color = color.value;
        }

        EndAction(true);
    }
}