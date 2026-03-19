using UnityEngine;

public class CookingRobotController : MonoBehaviour
{
    [Header("Scene References")]
    public Transform ingredient;
    public Transform cookingStation;

    [Header("Cooking Settings")]
    public float moveSpeed = 2f;
    public bool ingredientReady = false;
    public bool isCooking = false;
    public bool timedOut = false;

    [Header("Optional Visuals")]
    public Renderer robotRenderer;
    public Color idleColor = Color.white;
    public Color waitingColor = Color.yellow;
    public Color cookingColor = Color.green;
    public Color failColor = Color.red;

    private void Start()
    {
        SetIdleState();
    }

    public void SetIdleState()
    {
        isCooking = false;
        timedOut = false;
        SetColor(idleColor);
    }

    public void SetWaitingState()
    {
        SetColor(waitingColor);
    }

    public void SetCookingState()
    {
        isCooking = true;
        SetColor(cookingColor);
    }

    public void SetFailState()
    {
        timedOut = true;
        isCooking = false;
        SetColor(failColor);
    }

    private void SetColor(Color color)
    {
        if (robotRenderer != null)
        {
            robotRenderer.material.color = color;
        }
    }
}