
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        // Garante que exista apenas uma instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore(int amount)
    {
        if (amount <= 0)
            return;

        CurrentScore += amount;

        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;

        OnScoreChanged?.Invoke(CurrentScore);
    }
}