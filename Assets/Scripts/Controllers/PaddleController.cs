using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Camera mainCamera;

    [Header("Movement Limits")]
    [SerializeField] private BoxCollider2D leftLimit;
    [SerializeField] private BoxCollider2D rightLimit;

    private Rigidbody2D rb;
    private BoxCollider2D paddleCollider;

    private float targetX;
    private float fixedY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleCollider = GetComponent<BoxCollider2D>();

        // Armazena a posicao vertical inicial
        fixedY = rb.position.y;
        targetX = rb.position.x;

        // Configura a raquete para movimentacao cinematica
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        GetMousePosition();
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MovePaddle();
    }

    private void GetMousePosition()
    {
        // Verifica se existe um mouse conectado
        if (Mouse.current == null)
            return;

        // Captura a posicao do mouse na tela
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Converte para coordenadas do mundo
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(
                mousePosition.x,
                mousePosition.y,
                -mainCamera.transform.position.z
            )
        );

        // Armazena somente o eixo horizontal
        targetX = worldPosition.x;
    }

    private void MovePaddle()
    {
        // Calcula os limites considerando o tamanho da raquete
        float minX = leftLimit.bounds.max.x +
                     paddleCollider.bounds.extents.x;

        float maxX = rightLimit.bounds.min.x -
                     paddleCollider.bounds.extents.x;

        // Impede que a raquete ultrapasse os limites
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        // Movimenta somente no eixo X
        Vector2 newPosition = new Vector2(clampedX, fixedY);

        rb.MovePosition(newPosition);
    }
}