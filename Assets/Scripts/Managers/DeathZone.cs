using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    private void Awake()
    {
        // Garante que o collider seja um Trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto atingido possui o BallController
        BallController ball = other.GetComponentInParent<BallController>();

        if (ball == null)
            return;

        // Chama a derrota
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseGame();
        }
    }
}
