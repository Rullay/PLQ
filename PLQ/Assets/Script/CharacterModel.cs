using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterModel", menuName = "Character Model", order = 51)]
public class CharacterModel : ScriptableObject
{

    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool availability;


    public string Description
    {
        get
        {
            return description;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public float RotateSpeed
    {
        get
        {
            return rotateSpeed;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }

    public bool Availability
    {
        get
        {
            return availability;
        }
    }

    public void Unlocking()
    {
        availability = true;
    }
}
