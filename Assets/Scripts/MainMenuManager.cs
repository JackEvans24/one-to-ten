using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.PassAndPlay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
