using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    private GameInfo gameInfo;
    private AudioSource audioSource;
    private float transitionTime = 1f;

    public AudioClip winClip;
    public AudioClip loseClip;
    public TextMeshProUGUI endText;
    public LocalizedString winText;
    public LocalizedString loseText;
    public Animator sceneTransitionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameInfo = FileManager.LoadGameInfo();
        PrepareText();
        PrepareSound();
    }

    private void PrepareSound()
    {
        if (PlayerWon())
        {
            audioSource.clip = winClip;
        }
        else
        {
            audioSource.clip = loseClip;
        }
        audioSource.Play();
    }

    private void PrepareText()
    {
        if (PlayerWon())
        {
            endText.text = winText.GetLocalizedString();
        }
        else
        {
            endText.text = loseText.GetLocalizedString();
        }
    }

    public void ContinueGame()
    {
        if (PlayerWon())
        {
            FileManager.DestroyData();            
        }
        StartCoroutine(LoadLevel());        
    }

    private bool PlayerWon()
    {
        return gameInfo.playerhealth > 0;
    }

    IEnumerator LoadLevel()
    {
        sceneTransitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("MainMenu");
    }
}
