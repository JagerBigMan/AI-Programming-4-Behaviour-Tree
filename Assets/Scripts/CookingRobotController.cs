using UnityEngine;

public class CookingRobotController : MonoBehaviour
{
    [Header("Scene References")]
    public Transform ingredient;
    public Transform cookingStation;
    public Renderer robotRenderer;

    [Header("Detection Settings")]
    public float ingredientDetectRange = 3f;

    [Header("Cooking Settings")]
    public float moveSpeed = 2f;
    public bool isCooking = false;
    public bool timedOut = false;

    [Header("Optional Visuals")]
    public Color idleColor = Color.white;
    public Color waitingColor = Color.yellow;
    public Color cookingColor = Color.green;
    public Color failColor = Color.red;

    private void Start()
    {
        Debug.Log("CookingRobotController started on " + gameObject.name);
        SetIdleState();
    }

    private void Update()
    {
        // Manual state switch for testing
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

    public bool IsIngredientInRange()
    {
        if (ingredient == null)
        {
            Debug.LogWarning("Ingredient is not assigned.");
            return false;
        }

        float distance = Vector3.Distance(transform.position, ingredient.position);
        Debug.Log("Distance to ingredient: " + distance);

        return distance <= ingredientDetectRange;
    }

    public void SetIdleState()
    {
        isCooking = false;
        timedOut = false;
        SetColor(idleColor);
    }

    public void SetWaitingState()
    {
        isCooking = false;
        timedOut = false;
        SetColor(waitingColor);
    }

    public void SetCookingState()
    {
        isCooking = true;
        timedOut = false;
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
        else
        {
            Debug.LogWarning("robotRenderer is NULL");
        }
    }
}