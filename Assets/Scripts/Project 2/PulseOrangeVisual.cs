using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class PulseOrangeVisual : ActionTask<Transform>
{
    public BBParameter<float> pulseSpeed = 2f;

    private DroneVisualController visualController;
    private Color baseColor;
    private Color orangeColor;

    protected override void OnExecute()
    {
        visualController = agent.GetComponent<DroneVisualController>();

        if (visualController == null)
        {
            EndAction(false);
            return;
        }

        baseColor = visualController.GetOriginalColor();
        orangeColor = new Color(1f, 0.5f, 0f);
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

        float t = (Mathf.Sin(Time.time * pulseSpeed.value) + 1f) / 2f;          //this creates a value that goes 1,0,1,0...
        Color pulseColor = Color.Lerp(baseColor, orangeColor, t);           //using lerp to blend between two colors

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