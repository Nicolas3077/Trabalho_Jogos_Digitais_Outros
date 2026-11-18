
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float initialSpeed = 5f; //Controla a velocidade inicial
    [SerializeField] private Vector2 initialDirection = new Vector2(0.5f, 1f); //Controla a direcao inicial

    [Header("Paddle Bounce Settings")]
    [SerializeField, Range(0f, 80f)]
    private float maxBounceAngle = 60f;

    private Rigidbody2D rb; //Controla a fisica do objeto

    private Vector2 direction; //Controla a direcao X e Y

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LaunchBall();
    }

    private void FixedUpdate()
    {
        // Mantem a velocidade constante
        rb.linearVelocity = direction * initialSpeed;
    }

    private void LaunchBall()
    {
        // Inicializa a direcao e normaliza o vetor
        direction = initialDirection.normalized;

        // Garante que a bola comece subindo
        if (direction.y < 0f)
        {
            direction.y *= -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            CalculatePaddleBounce(collision);
        }
        else
        {
            // Calcula a reflexao da bola
            CalculateWallBounce(collision);

            // Verifica se o objeto atingido e um bloco
            BrickController brick =
                collision.collider.GetComponentInParent<BrickController>();

            if (brick != null)
            {
                brick.DestroyBrick();
            }
        };
    }

    private void CalculateWallBounce(Collision2D collision)
    {
        // Obtem a normal da superficie atingida
        Vector2 normal = collision.GetContact(0).normal;

        // Calcula a reflexao da direcao
        direction = Vector2.Reflect(direction, normal).normalized;
    }

    private void CalculatePaddleBounce(Collision2D collision)
    {
        // Obtem o collider da raquete
        Collider2D paddleCollider = collision.collider;

        // Obtem o ponto exato de contato
        Vector2 contactPoint = collision.GetContact(0).point;

        // Calcula a posicao relativa do impacto
        float hitPoint = contactPoint.x - paddleCollider.bounds.center.x;

        // Normaliza o impacto entre -1 e 1
        float normalizedHitPoint =
            hitPoint / paddleCollider.bounds.extents.x;

        normalizedHitPoint = Mathf.Clamp(
            normalizedHitPoint, -1f, 1f
        );

        // Converte a posicao do impacto em um angulo
        float bounceAngle = normalizedHitPoint * maxBounceAngle;

        // Converte graus para radianos
        float angleInRadians = bounceAngle * Mathf.Deg2Rad;

        // Calcula a nova direcao da bola
        direction = new Vector2(
            Mathf.Sin(angleInRadians),
            Mathf.Cos(angleInRadians)
        ).normalized;
    }
}
