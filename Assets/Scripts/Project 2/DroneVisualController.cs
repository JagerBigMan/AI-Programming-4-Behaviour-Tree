using UnityEngine;

public class DroneVisualController : MonoBehaviour
{
    private Renderer[] droneRenderers;
    private Color[] originalColors;

    public bool isOverrideActive = false;

    private void Awake()
    {
        droneRenderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[droneRenderers.Length];          //Selecting all the components on that obejct and change the colors for all

        for (int i = 0; i < droneRenderers.Length; i++)
        {
            originalColors[i] = droneRenderers[i].material.color;
        }
    }

    public void SetOverrideColor(Color color)
    {
        isOverrideActive = true;

        for (int i = 0; i < droneRenderers.Length; i++)
        {
            droneRenderers[i].material.color = color;
        }
    }

    public void ClearOverride()
    {
        isOverrideActive = false;

        for (int i = 0; i < droneRenderers.Length; i++)
        {
            droneRenderers[i].material.color = originalColors[i];
        }
    }

    public void SetPulseColor(Color color)
    {
        if (isOverrideActive)
            return;

        for (int i = 0; i < droneRenderers.Length; i++)
        {
            droneRenderers[i].material.color = color;
        }
    }

    public Color GetOriginalColor(int index = 0)
    {
        if (originalColors.Length == 0)
            return Color.white;

        return originalColors[Mathf.Clamp(index, 0, originalColors.Length - 1)];
    }
}