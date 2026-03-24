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

        Renderer rend = agent.GetComponent<Renderer>();

        if (rend == null)
        {
            rend = agent.GetComponentInChildren<Renderer>();
        }

        if (rend == null)
        {
            EndAction(false);
            return;
        }

        rend.material.color = normalColor.value;
        EndAction(true);
    }
}