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

        Renderer render = agent.GetComponent<Renderer>();

        if (render == null)
        {
            render = agent.GetComponentInChildren<Renderer>();
        }

        if (render == null)
        {
            EndAction(false);
            return;
        }

        render.material.color = color.value;

        EndAction(true);
    }
}