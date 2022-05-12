using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas exitCanvas;
    public Canvas optionsCanvas;
    public GameObject continueButton;
    public Animator sceneTransitionAnimator;

    private GameInfo gameInfo;
    private float transitionTime = 1f;

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
        StartCoroutine(LoadLevel());
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

    public void Options()
    {
        mainCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
    }

    IEnumerator LoadLevel()
    {
        sceneTransitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Forest");
    }

}
