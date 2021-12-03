using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ADSManager : MonoBehaviour, IUnityAdsListener
{

    [SerializeField] private bool testMode = true;
    [SerializeField] private Button rewardButton;
    [SerializeField] private Button rewardButtonContinion;
    [SerializeField] private GameObject menuManager;
    private bool is_rewardRedy;
    private bool isAlreadyWatched;

    [SerializeField] private bool isModel;
    [SerializeField] private bool isMineMenu;
    private string gameId = "4455229";
    private string rewardedVideo = "Rewarded_Android";
    private string video = "Interstitial_Android";



    void Start()
    {
        Advertisement.AddListener(this);
        InitializeReward();

        rewardButton.interactable = Advertisement.IsReady(rewardedVideo);

        if (rewardButton)
        {
            rewardButton.onClick.AddListener(ShowRewardVideo);
        }
        if (rewardButtonContinion)
        {
            rewardButtonContinion.onClick.AddListener(ShowRewardVideoContinion);
        }
        
        rewardButton.interactable = true;
        isAlreadyWatched = false;
    }

    void InitializeReward()
    {

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode);
        }

        if (!Advertisement.isInitialized || !Advertisement.IsReady())
        {
            StartCoroutine(is_Initialize());
        }
    }

    IEnumerator is_Initialize()
    {
        yield return new WaitForSeconds(0.5f);
        InitializeReward();
    }

    public void ReStart()
    {
        isAlreadyWatched = false;
    }


    // Reward..........................................................................
    public void ShowRewardVideo()
    {
        isModel = true;
        if (Advertisement.IsReady())
        {
            Advertisement.Show(rewardedVideo);
        }
    }

    public void ShowRewardVideoContinion()
    {
        isModel = false;
        if (Advertisement.IsReady() && isAlreadyWatched == false )
        {
            Advertisement.Show(rewardedVideo);
        }
    }


    // Interstitial................................................................
    public void InterstitialReStart()
    {
        isMineMenu = false;
        int random = Random.Range(0, 100);
        if (random < 61 && Advertisement.IsReady())
        {       
           ShowInterstitialVideo(); 
        }
        else
        {
            menuManager.GetComponent<MenuManager>().StartButton();
        }
  
    }

    public void IinterstitialMineMenu()
    {
        isMineMenu = true;
        int random = Random.Range(0, 100);
        if (random < 61 && Advertisement.IsReady())
        {
           ShowInterstitialVideo();
        }
        else
        {
            menuManager.GetComponent<MenuManager>().Menu();
        }
       
    }

    public static void ShowInterstitialVideo()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
        }
        else
        {
            Debug.Log("Advertisement not ready!");
        }
    }




    // IUnityAdsListener ............................................................
    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("RewardError");
        // сделать окно с сообщение: Рекламма временно не доступна 
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == rewardedVideo)
        {

            if (showResult == ShowResult.Finished)
            {
                if (isModel == true)
                {
                    menuManager.GetComponent<MenuManager>().UnlockingModel();
                }
                else
                {
                    isAlreadyWatched = true;
                    menuManager.GetComponent<MenuManager>().RewardContinion();
                }


            }
            else if (showResult == ShowResult.Skipped)
            {
                
            }
            else if (showResult == ShowResult.Failed)
            {
                
            }
        }


        if (placementId == video)
        {
            if (isMineMenu == true)
            {
                if (showResult == ShowResult.Finished)
                {
                    menuManager.GetComponent<MenuManager>().Menu();
                }
                else if (showResult == ShowResult.Skipped)
                {
                    menuManager.GetComponent<MenuManager>().Menu();
                }
                else if (showResult == ShowResult.Failed)
                {
                    menuManager.GetComponent<MenuManager>().Menu();
                }
            }
            else
            {
                if (showResult == ShowResult.Finished)
                {
                    menuManager.GetComponent<MenuManager>().StartButton();
                }
                else if (showResult == ShowResult.Skipped)
                {
                    menuManager.GetComponent<MenuManager>().StartButton();
                }
                else if (showResult == ShowResult.Failed)
                {
                    menuManager.GetComponent<MenuManager>().StartButton();
                }
            }
 
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
       /*  if (placementId == rewardedVideo)
         {
             rewardButton.interactable = true;
         }*/
    }

}
