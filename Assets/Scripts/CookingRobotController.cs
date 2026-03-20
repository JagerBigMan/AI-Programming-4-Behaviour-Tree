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
        Debug.Log("CookingRobotController Start on " + gameObject.name);
        SetIdleState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pressed 1 -> Idle");
            SetIdleState();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Pressed 2 -> Waiting");
            SetWaitingState();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Pressed 3 -> Cooking");
            SetCookingState();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Pressed 4 -> Fail");
            SetFailState();
        }
    }

    public void SetIdleState()
    {
        Debug.Log("SetIdleState called");
        isCooking = false;
        timedOut = false;
        SetColor(idleColor);
    }

    public void SetWaitingState()
    {
        Debug.Log("SetWaitingState called");
        SetColor(waitingColor);
    }

    public void SetCookingState()
    {
        Debug.Log("SetCookingState called");
        isCooking = true;
        SetColor(cookingColor);
    }

    public void SetFailState()
    {
        Debug.Log("SetFailState called");
        timedOut = true;
        isCooking = false;
        SetColor(failColor);
    }

    private void SetColor(Color color)
    {
        Debug.Log("SetColor called with " + color);

        if (robotRenderer != null)
        {
            robotRenderer.material.color = color;
        }
        else
        {
            Debug.LogWarning("robotRenderer is NULL");
        }
    }
}