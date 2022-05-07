using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject collectibles;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI rubiesText;
    public GameObject[] health;
    public GameObject player;
    public Sprite fullHearth;
    public Sprite emptyHeart;
    public Canvas pauseCanvas;
    public Canvas confirmCanvas;


    private GameInfo gameInfo;
    private bool isGamePaused = false;
    private bool isGameOnConfirmedPage = false;
    private static GameManager gmInstance;
    private bool playerCanPlay = true;

    public static GameManager Instance { get { return gmInstance; } }
    

    // Start is called before the first frame update
    private void Awake()
    {
        if (gmInstance != null && gmInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gmInstance = this;
        }
    }

    private void Start()
    {
        gameInfo = FileManager.LoadGameInfo();
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                if (isGameOnConfirmedPage)
                {
                    ExitConfirmMenu();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }       
    }

    public void PauseGame()
    {
        pauseCanvas.gameObject.SetActive(true);
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void ConfirmExitMenu()
    {
        pauseCanvas.gameObject.SetActive(false);
        confirmCanvas.gameObject.SetActive(true);
        isGameOnConfirmedPage = true;
    }

    public void ExitConfirmMenu()
    {
        isGameOnConfirmedPage = false;
        confirmCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }


    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }



    public void PickUpCoin(int coinValue)
    {

        gameInfo.coins += coinValue;
        coinsText.SetText(gameInfo.coins.ToString());
        var animatorState = collectibles.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("HideCollectibles") && animatorState.normalizedTime >= 1.0f)
        {
            collectibles.GetComponent<Animator>().SetTrigger("Show");
        }
    }

    public void PickUpPotion()
    {
        gameInfo.playerhealth = gameInfo.playerMaxhealth;
        UpdateHealthUI();
    }

    public void PickUpRuby(int rubyValue)
    {
        gameInfo.rubies += rubyValue;
        rubiesText.SetText(gameInfo.rubies.ToString());
        var animatorState = collectibles.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("HideCollectibles") && animatorState.normalizedTime >= 1.0f)
        {
            collectibles.GetComponent<Animator>().SetTrigger("Show");
        }        
    }

    private void UpdateHealthUI()
    {
        for(int i = 0; i < gameInfo.playerMaxhealth; i++)
        {
            if(i < gameInfo.playerhealth)
            {
                health[i].GetComponent<SpriteRenderer>().sprite = fullHearth;
            }
            else
            {
                health[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
            
        }
    }

    public void PlayerHit(int damage)
    {
        gameInfo.playerhealth -= damage;
        UpdateHealthUI();
        if(gameInfo.playerhealth <= 0)
        {
            FileManager.SaveData(gameInfo);
            EndGame();
        }
    }

    public void EndGame()
    {
        player.GetComponent<PlayerController>().Death();        
        SceneManager.LoadScene("EndGame");
    }

    public void LevelCompleted()
    {
        gameInfo.currentLevel++;       
        FileManager.SaveData(gameInfo);
        SceneManager.LoadScene(gameInfo.currentLevel);
    }

    public void StopPlayer()
    {
        playerCanPlay = !playerCanPlay;
    }

    public bool PlayerCanMove()
    {
        return playerCanPlay;
    }
  
}
