using UnityEngine;

public class PlayerFreezeVisual : MonoBehaviour
{
    private Renderer[] renderers;
    private Color[] originalColors;

    public Color frozenColor = new Color(0f, 0f, 0.5f); // dark blue

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    void Update()
    {
        if (DroneComboSystem.playerFrozen)
        {
            // Set dark blue when frozen
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = frozenColor;
            }
        }
        else
        {
            // Restore original color
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = originalColors[i];
            }
        }
    }
}