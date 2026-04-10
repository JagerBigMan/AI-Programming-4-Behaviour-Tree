using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class StartCooldownVisual : ActionTask<Transform>
{
    public BBParameter<float> cooldownDuration = 2f;

    private Renderer droneRenderer;
    private Color originalColor;
    private Coroutine cooldownRoutine;

    protected override void OnExecute()
    {
        droneRenderer = agent.GetComponentInChildren<Renderer>();

        if (droneRenderer == null)
        {
            EndAction(false);
            return;
        }

        originalColor = droneRenderer.material.color;

        MonoManager monoManager = agent.GetComponent<MonoManager>();
        if (monoManager == null)
        {
            monoManager = agent.gameObject.AddComponent<MonoManager>();
        }

        if (cooldownRoutine != null)
        {
            monoManager.StopCoroutine(cooldownRoutine);
        }

        cooldownRoutine = monoManager.StartCoroutine(CooldownRoutine());
        EndAction(true);
    }

    private IEnumerator CooldownRoutine()
    {
        droneRenderer.material.color = Color.red;

        yield return new WaitForSeconds(cooldownDuration.value);

        if (droneRenderer != null)
        {
            droneRenderer.material.color = originalColor;
        }
    }
}

public class MonoManager : MonoBehaviour
{
}