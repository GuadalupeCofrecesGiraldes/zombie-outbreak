using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void InitGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowCredits ()
    {
        Debug.Log("Mostrando créditos...");
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Saliendo del juego");
    }

    public void ShowOptions()
    {
        Debug.Log("Mostrar opciones");
    }
}
