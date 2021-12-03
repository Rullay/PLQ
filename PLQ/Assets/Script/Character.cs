using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject Direction;

    private float RotateSpeed = 10;
    [SerializeField]  private float moveSpeed = 3;
    private float actualMoveSpeed = 3;

    private Vector2 MausePosition_Vector;
    private Vector2 MausePosition;
   
    private float angleOfRotation;
    private float currentAngle;
    private bool isDead;

    
    void Start()
    {
        currentAngle = transform.rotation.eulerAngles.z;
        _camera.GetComponent<CameraManager>().CameraSpeed(moveSpeed);
        isDead = true;
    }


    void Update()
    {
        CharacterControll();
        Move();             
    }

    void CharacterControll()
    {
        if (isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MausePosition = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
                MausePosition_Vector = new Vector2(MausePosition.x * 5 / Screen.width, MausePosition.y * 5 / Screen.width);
                angleOfRotation = Vector3.Angle(Vector3.up, MausePosition_Vector) * -MausePosition.x / Mathf.Abs(MausePosition.x);
                _camera.GetComponent<CameraManager>().CameraSpeed(moveSpeed);
            }
            currentAngle = Mathf.Lerp(currentAngle, angleOfRotation, RotateSpeed * Time.deltaTime);
            currentAngle = Mathf.Clamp(currentAngle, -50, 50);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
        
    }
    
    void Move()
    {
        if(isDead == false)
        {
            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y);
        } 
    }

    public void MoveSpeed(float score)
    {
        //moveSpeed = actualMoveSpeed + (score / (1000 * moveSpeed));
        moveSpeed = (1 - Mathf.Pow(1 - 0.00003f, 8000 + score)) * 12;
            

    }

    public void StartPosition()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _camera.GetComponent<CameraManager>().StartPosition();
    }
    public void isAlive()
    {
        isDead = false;
        currentAngle = 0;
        angleOfRotation = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead == false)
        {
            isDead = true;
            gameManager.GetComponent<GameManager>().Dead();
        }
    }   
}

