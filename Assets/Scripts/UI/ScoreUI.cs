
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager nao encontrado!");
            return;
        }

        // Inscreve no evento de alteracao da pontuacao
        ScoreManager.Instance.OnScoreChanged += UpdateScore;

        // Atualiza o valor inicial
        UpdateScore(ScoreManager.Instance.CurrentScore);
    }

    private void OnDestroy()
    {
        // Remove a inscricao no evento
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
