using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private const string SCENE_MAIN = "Main";
    private const string SCENE_CREDITS = "Credits";
    private const string SCENE_MAIN_MENU = "MainMenu";

    public void InitGame()
    {
        SceneManager.LoadScene(SCENE_MAIN);
    }

    public void ShowCredits ()
    {
        SceneManager.LoadScene(SCENE_CREDITS);
        Debug.Log("Mostrando créditos...");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void ShowOptions()
    {
        Debug.Log("Mostrar opciones");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SCENE_MAIN_MENU);
    }
}
