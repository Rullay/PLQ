using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipedButton : MonoBehaviour
{

    private GameObject actualTarget;
    [SerializeField] private GameObject menuManager;


    void Start()
    {

    }

    void Update()
    {   
        
    }

    public void Target(GameObject target)
    {
        actualTarget = target;
    }
    public void EquipedClick()
    {
        menuManager.GetComponent<MenuManager>().Equiped(actualTarget);
        menuManager.GetComponent<MenuManager>().RemoveTarget();
    }



}
