using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetButton : MonoBehaviour
{
    [SerializeField] private GameObject equipedButton;
    [SerializeField] private GameObject menuManager;
    [SerializeField] private GameObject targetImage;
    public void Click()
    {
        equipedButton.GetComponent<EquipedButton>().Target(gameObject);
        menuManager.GetComponent<MenuManager>().CheckForAvailability(gameObject);    
        menuManager.GetComponent<MenuManager>().RemoveTarget();
        targetImage.SetActive(true);
        targetImage.transform.position = transform.position;
    }

   
}