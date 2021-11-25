using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject deathMenuPanel;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject rewardButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject tapToContinion;
    [SerializeField] private List<CharacterModel> Models;
    [SerializeField] private List<GameObject> buyButtons;
    [SerializeField] private GameObject targetImage;

    private CharacterModel actualTarget;
    private bool isRewardContinion;


    void Start()
    {
        actualTarget = Models[0];
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
    }

    public void Menu()
    {
        deathMenuPanel.SetActive(false);
        MenuPanel.SetActive(true);
        gameManager.GetComponent<GameManager>().MineMenu();
    }


    // Death Menu
    public void DeathMenu()
    {
        deathMenuPanel.SetActive(true);
    }


    public void ContinionButton()
    {
       // gameManager.GetComponent<GameManager>().ContinionGame();
       // deathMenuPanel.SetActive(false);
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
    }


    //Shop Menu


    public void ShopMenu()
    {
        shopMenu.SetActive(true);
    }
    public void BackToMineMenu()
    {
        shopMenu.SetActive(false);
    }
    public void Equiped(GameObject button)
    {
        for (int i = 0; i < buyButtons.Count; i++)
        {
            if (buyButtons[i] == button)
            {
                gameManager.GetComponent<GameManager>().ParametersShips(Models[i]);
                check.transform.position = new Vector2(249, buyButtons[i].transform.position.y);
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
    }




}
