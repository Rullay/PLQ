using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject deathMenuPanel;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject rewardButton;
    [SerializeField] private GameObject ADSManager;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject tapToContinion;
    [SerializeField] private List<CharacterModel> Models;
    [SerializeField] private List<GameObject> buyButtons;
    [SerializeField] private GameObject targetImage;

    //таблица рекордов .........................................
    [SerializeField] private List<Text> scoreTexts;
    [SerializeField] private List<int> scoreInts = new List<int>();
    private bool isContinue;
    private int scoreIndexCont;
    private int ScoreCount;

    private CharacterModel actualTarget;
    private bool isRewardContinion;


    void Start()
    {
        actualTarget = Models[0];
        isContinue = false;
        LoadScore();
        loadSkinEquiped();
        LoadSkins();
    }


    void Update()
    {
        TapToContinoin();
    }

    public void Score(float score)
    {
        scoreText.text = "" + score;
    }

    // Mine Menu
    public void StartButton()
    {
        gameManager.GetComponent<GameManager>().Restart();
        deathMenuPanel.SetActive(false);
        MenuPanel.SetActive(false);
        ADSManager.GetComponent<ADSManager>().ReStart();
    }

    public void Menu()
    {
        deathMenuPanel.SetActive(false);
        MenuPanel.SetActive(true);
        gameManager.GetComponent<GameManager>().MineMenu();
    }

    public void Record()
    {
        scorePanel.SetActive(true);
    }


    // Death Menu
    public void DeathMenu()
    {
        deathMenuPanel.SetActive(true);
    }

    public void TapToContinoin()
    {
        if (Input.GetButtonDown("Fire1") && isRewardContinion == true)
        {
            tapToContinion.SetActive(false);
            gameManager.GetComponent<GameManager>().ContinionGame();
            isRewardContinion = false;
        }

    }

    public void RewardContinion()
    {
        deathMenuPanel.SetActive(false);
        tapToContinion.SetActive(true);
        isRewardContinion = true;
        isContinue = true;
    }


    //Shop Menu


    public void ShopMenu()
    {
        shopMenu.SetActive(true);
    }
    public void BackToMineMenu()
    {
        shopMenu.SetActive(false);
        scorePanel.SetActive(false);
    }
    public void Equiped(GameObject button)
    {
        for (int i = 0; i < buyButtons.Count; i++)
        {
            if (buyButtons[i] == button)
            {
                gameManager.GetComponent<GameManager>().ParametersShips(Models[i]);
                check.transform.position = new Vector2(249, buyButtons[i].transform.position.y);
                PlayerPrefs.SetInt("Skin", i);
            }
        }
    }

    public void RemoveTarget()
    {
        for (int i = 0; i < buyButtons.Count; i++)
        {
            targetImage.SetActive(false);
        }
    }

    public void CheckForAvailability(GameObject target)
    {
        for (int i = 0; i < Models.Count; i++)
        {
            if (buyButtons[i] == target)
            {
                actualTarget = Models[i];
                if (Models[i].Availability == true)
                {
                    rewardButton.SetActive(false);
                }
                else
                {
                    rewardButton.SetActive(true);
                }
            }
        }
    }

    public void UnlockingModel()
    {
        actualTarget.Unlocking();
        rewardButton.SetActive(false);
        SaveSkins();
    }

    // Records Menu

    public void NewScore(float fScore)
    {
        int score = (int)fScore;
        if (isContinue == true)
        {
            scoreInts[scoreIndexCont] = score;
            RecordText();
            isContinue = false;
        }
        else
        {
            if (scoreInts.Count < scoreTexts.Count)
            {
                scoreInts.Add(score);
                for (int i = 0; i < scoreInts.Count; i++)
                {
                    if (scoreInts[i] == score)
                    {
                        scoreIndexCont = i;
                    }
                }
                RecordText();
            }
            else
            {
                int minScore = scoreInts[0];
                int minIndex = 0;
                for (int i = 0; i < scoreInts.Count; i++)
                {
                    if (scoreInts[i] < minScore)
                    {
                        minScore = scoreInts[i];
                        minIndex = i;
                    }
                }

                if (score > minScore)
                {
                    scoreInts[minIndex] = score;
                    scoreIndexCont = minIndex;
                    RecordText();
                }

            }
        }
        SaveScore();
    }

    void RecordText()
    {
        int minUsed = 1000000000;
        for (int j = 0; j < scoreInts.Count; j++)
        {
            int maxScore = 0;
            for (int i = 0; i < scoreInts.Count; i++)
            {
                if (scoreInts[i] > maxScore && scoreInts[i] < minUsed)
                {
                    maxScore = scoreInts[i];
                }

            }
            minUsed = maxScore;
            scoreTexts[j].text = "Score : " + maxScore;
        }
       

    }


    // Save & Load..................................
    void SaveScore()
    {
        for (int i = 0; i < scoreInts.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i, scoreInts[i]);
        }
        ScoreCount = scoreInts.Count;
        PlayerPrefs.SetInt("ScoreCount", ScoreCount);
    }
    void LoadScore()
    {
        ScoreCount = PlayerPrefs.GetInt("ScoreCount");
        if (PlayerPrefs.HasKey("Score0"))
        {
            for (int i = 0; i < ScoreCount; i++)
            {
                scoreInts.Add(PlayerPrefs.GetInt("Score" + i));
            }           
        }
        RecordText();
    }

    void loadSkinEquiped()
    {
        if (PlayerPrefs.HasKey("Skin"))
        {
            shopMenu.SetActive(true);
            Equiped(buyButtons[PlayerPrefs.GetInt("Skin")]);
            shopMenu.SetActive(false); 
        }
        else
        {
            shopMenu.SetActive(true);
            Equiped(buyButtons[0]);
            shopMenu.SetActive(false);
        }

    }


    void SaveSkins()
    {
        for (int i = 0; i < Models.Count; i++)
        {
            if (Models[i].Availability == true)
            {
                PlayerPrefs.SetInt("Unlock" + i, i);
            }
        }
    }

    void LoadSkins()
    {
        if (PlayerPrefs.HasKey("Unlock0"))
        {
            for (int i = 0; i < Models.Count; i++)
            {
                if(i == (PlayerPrefs.GetInt("Unlock" + i)))
                {
                    Models[i].Unlocking();
                }
            }
        }
    }

}
