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
    public Animator sceneTransitionAnimator;


    private GameInfo gameInfo;
    private bool isGamePaused = false;
    private bool isGameOnConfirmedPage = false;
    private static GameManager gmInstance;
    private bool playerCanPlay = true;
    private float transitionTime = 1f;


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
        player.GetComponent<PlayerController>().SetDamage(gameInfo.attackIncreased);
        UpdateHealthUI();
        PickUpRuby(0);
        PickUpCoin(0);
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
        StopPlayer();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        isGamePaused = false;
        StartPlayer();
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
        rubiesText.SetText(gameInfo.rubies.ToString() + " / " + (gameInfo.levelRange[gameInfo.playerLevel-1]).ToString());
        var animatorState = collectibles.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("HideCollectibles") && animatorState.normalizedTime >= 1.0f)
        {
            collectibles.GetComponent<Animator>().SetTrigger("Show");
        }
        if(gameInfo.rubies >= gameInfo.levelRange[gameInfo.playerLevel - 1])
        {
            LevelUp();
        }
    }

    private void UpdateHealthUI()
    {
        int maxHealth = 10;
        for(int i = 0; i < maxHealth; i++)
        {
            if(i < gameInfo.playerhealth)
            {
                health[i].GetComponent<SpriteRenderer>().sprite = fullHearth;
            }
            else
            {
                health[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
            health[i].SetActive(i<gameInfo.playerMaxhealth);
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
        StartCoroutine(LoadLevel(4));        
    }

    public void LevelCompleted(int nextLevel)
    {
        gameInfo.currentLevel = nextLevel;       
        FileManager.SaveData(gameInfo);
        StartCoroutine(LoadLevel(gameInfo.currentLevel));
    }

    IEnumerator LoadLevel(int level)
    {
        sceneTransitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }

    public void StopPlayer()
    {
        playerCanPlay = false;
    }

    public void StartPlayer()
    {
        playerCanPlay = true;
    }

    public bool PlayerCanMove()
    {
        return playerCanPlay;
    }

    public bool CanPurchaseItem(int price)
    {
        return price <= gameInfo.coins;
    }

    public void PurchaseItem(int price)
    {
        PickUpCoin(-price);
    }

    private void LevelUp()
    {
        UpdateLevelData();
        PickUpRuby(-gameInfo.rubies);
    }

    public void IncreaseLife()
    {
        UpdateLevelData();
    }


    private void UpdateLevelData()
    {
        gameInfo.playerMaxhealth++;
        gameInfo.playerhealth = gameInfo.playerMaxhealth;
        gameInfo.playerLevel++;
        player.GetComponent<PlayerController>().MakeLevelUp();
        UpdateHealthUI();
    }

    public void IncreasePlayerDamage(int amount)
    {
        gameInfo.attackIncreased += amount;
        player.GetComponent<PlayerController>().SetDamage(gameInfo.attackIncreased);
    }
  
}
