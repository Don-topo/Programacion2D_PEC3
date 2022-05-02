using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    private GameInfo gameInfo;
    private AudioSource audioSource;

    public AudioClip winClip;
    public AudioClip loseClip;
    public TextMeshProUGUI endText;

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
            endText.text = "CONGRATULATIONS";
        }
        else
        {
            endText.text = "GAME OVER";
        }
    }

    private void PrepareAnimation()
    {

    }

    public void ContinueGame()
    {
        if (PlayerWon())
        {
            FileManager.DestroyData();            
        }
        SceneManager.LoadScene("MainMenu");
    }

    private bool PlayerWon()
    {
        return gameInfo.playerhealth > 0;
    }
}
