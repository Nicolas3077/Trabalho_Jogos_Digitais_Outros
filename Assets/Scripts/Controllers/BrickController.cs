using UnityEngine;

public class BrickController : MonoBehaviour
{
    private bool isDestroyed;


    public void DestroyBrick()
    {
        if (isDestroyed)
            return;

        isDestroyed = true;

        // Adiciona pontuacao
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1);
        }

        // Registra a destruicao no GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterBrickDestroyed();
        }
        else
        {
            Debug.LogError("GameManager nao encontrado!");
        }

        Destroy(gameObject);
    }

}
