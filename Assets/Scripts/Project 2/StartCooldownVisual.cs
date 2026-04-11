using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class StartCooldownVisual : ActionTask<Transform>
{
    public BBParameter<float> cooldownDuration = 2f;

    private DroneVisualController visualController;
    private Coroutine cooldownRoutine;

    protected override void OnExecute()
    {
        visualController = agent.GetComponent<DroneVisualController>();
        if (visualController == null)
        {
            EndAction(false);
            return;
        }

        if (cooldownRoutine != null)
        {
            visualController.StopCoroutine(cooldownRoutine);                //using coroutine because I want the game to continue without building a whole timer system in update
        }

        cooldownRoutine = visualController.StartCoroutine(CooldownRoutine());
        EndAction(true);
    }

    private IEnumerator CooldownRoutine()
    {
        visualController.SetOverrideColor(Color.red);

        yield return new WaitForSeconds(cooldownDuration.value);

        visualController.ClearOverride();
        cooldownRoutine = null;
    }
}