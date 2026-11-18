
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private int totalBlocks = 44;

    [Header("Game Over Menus")]
    [SerializeField] private GameObject defeatMenu;
    [SerializeField] private GameObject victoryMenu;

    private int destroyedBlocks;

    private bool gameEnded;

    private void Awake()
    {
        // Garante uma unica instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Garante que o tempo esteja normal ao iniciar
        Time.timeScale = 1f;

        // Esconde os menus inicialmente
        defeatMenu.SetActive(false);
        victoryMenu.SetActive(false);
    }

    public void RegisterBrickDestroyed()
    {
        if (gameEnded)
            return;

        destroyedBlocks++;

        Debug.Log("Blocos destruidos: " + destroyedBlocks + " / " + totalBlocks);

        // Verifica se todos os blocos foram destruidos
        if (destroyedBlocks >= totalBlocks)
        {
            Debug.Log("TODOS OS BLOCOS FORAM DESTRUIDOS!");
            WinGame();
        }
    }

    public void WinGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        victoryMenu.SetActive(true);

        Cursor.visible = true;

        // Pausa a partida
        Time.timeScale = 0f;

        Debug.Log("VITORIA!");
    }

    public void LoseGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        defeatMenu.SetActive(true);

        Cursor.visible = true;

        // Pausa a partida
        Time.timeScale = 0f;

        Debug.Log("DERROTA!");
    }
}
