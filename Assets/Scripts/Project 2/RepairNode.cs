using UnityEngine;
using UnityEngine.UI;

public class RepairNode : MonoBehaviour
{
    [Header("HP")]
    public float maxHP = 100f;
    public float currentHP = 100f;

    [Header("Repair")]
    public float repairDuration = 3f; 

    [Header("Decay")]
    public float passiveDecayPerSecond = 5f;

    [Header("UI")]
    public Image healthBar;

    private bool hasTriggeredLose = false;  //prevents multiple lose triggers

    private void Start()
    {
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        RefreshHPBar();
    }

    private void Update()
    {
        if (IsDestroyed()) return;

        currentHP = Mathf.Clamp(currentHP - passiveDecayPerSecond * Time.deltaTime, 0f, maxHP);
        RefreshHPBar();

        if (currentHP <= 0f && !hasTriggeredLose)
        {
            hasTriggeredLose = true;
            Debug.Log(gameObject.name + " destroyed.");
            GameManager.Instance.LoseGame();
        }
    }

    public void TakeDamage(float amount)
    {
        if (IsDestroyed()) return;

        currentHP = Mathf.Clamp(currentHP - amount, 0f, maxHP);
        RefreshHPBar();

        if (currentHP <= 0f && !hasTriggeredLose)
        {
            hasTriggeredLose = true;
            Debug.Log(gameObject.name + " destroyed.");
            GameManager.Instance.LoseGame();
        }
    }

    public void FullyRepair()
    {
        if (IsDestroyed()) return;

        currentHP = maxHP;
        RefreshHPBar();
        Debug.Log(gameObject.name + " fully repaired.");
    }

    public bool IsDestroyed()
    {
        return currentHP <= 0f;
    }

    public bool IsFullyRepaired()
    {
        return currentHP >= maxHP;
    }

    public float GetHPPercent()
    {
        if (maxHP <= 0f) return 0f;
        return currentHP / maxHP;
    }

    private void RefreshHPBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = GetHPPercent();
        }
    }
}