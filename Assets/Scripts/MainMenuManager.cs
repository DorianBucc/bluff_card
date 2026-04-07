using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject confirmationExitUI;
    public void LoadingGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGameWithConfirmation()
    {
        Application.Quit();
    }

    public void ConfirmQuit()
    {
        confirmationExitUI.SetActive(true);
    }
    public void ConcelConfirmQuit()
    {
        confirmationExitUI.SetActive(false);
    }


}
