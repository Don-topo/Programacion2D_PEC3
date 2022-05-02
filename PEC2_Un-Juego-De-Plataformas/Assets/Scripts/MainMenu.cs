using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas exitCanvas;
    public GameObject continueButton;

    private GameInfo gameInfo;

    private void Start()
    {
        if (FileManager.CheckIfExistSavedData())
        {
            gameInfo = FileManager.LoadGameInfo();
        }
        else
        {
            continueButton.GetComponent<Button>().interactable = false;            
        }
    }

    public void StartGame()
    {
        FileManager.DestroyData();
        SceneManager.LoadScene("Forest");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(gameInfo.currentLevel);
    }

    public void ConfirmExit()
    {
        mainCanvas.gameObject.SetActive(false);
        exitCanvas.gameObject.SetActive(true);
    }

    public void CancelExit()
    {
        exitCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
    }

}
