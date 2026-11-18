using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{

    [SerializeField] GameObject MenuContainer;
    [SerializeField] GameObject CreditsContainer;

    public void StartGame()
    {
        SceneManager.LoadScene(1);  
        Time.timeScale = 1;
        Cursor.visible = false;
                  
    }

    public void Credits()
    {
        MenuContainer.SetActive(false);
        CreditsContainer.SetActive(true);
    }

    public void Return()
    {
        MenuContainer.SetActive(true);
        CreditsContainer.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
