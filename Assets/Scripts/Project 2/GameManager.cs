using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Win Condition")]
    public float surviveTime = 120f;
    private float timer = 0f;

    [Header("State")]
    public bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= surviveTime)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;
        Debug.Log("YOU WIN - Survived 120 seconds.");
    }

    public void LoseGame()
    {
        if (gameEnded) return;
        gameEnded = true;
        Debug.Log("YOU LOSE - A node was destroyed.");
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0f, surviveTime - timer);
    }
}