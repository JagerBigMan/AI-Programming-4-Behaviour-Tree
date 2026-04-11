using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class PulseLightBlueVisual : ActionTask<Transform>
{
    public BBParameter<float> pulseSpeed = 2f;

    private DroneVisualController visualController;
    private Color baseColor;
    private Color lightBlueColor;

    protected override void OnExecute()
    {
        visualController = agent.GetComponent<DroneVisualController>();

        if (visualController == null)
        {
            EndAction(false);
            return;
        }

        baseColor = visualController.GetOriginalColor();
        lightBlueColor = new Color(0.4f, 0.8f, 1f);
    }

    protected override void OnUpdate()
    {
        if (visualController == null)
        {
            EndAction(false);
            return;
        }

        if (visualController.isOverrideActive)
            return;

        float t = (Mathf.Sin(Time.time * pulseSpeed.value) + 1f) / 2f;
        Color pulseColor = Color.Lerp(baseColor, lightBlueColor, t);

        visualController.SetPulseColor(pulseColor);
    }

    protected override void OnStop()
    {
        if (visualController != null && !visualController.isOverrideActive)
        {
            visualController.SetPulseColor(baseColor);
        }
    }
}