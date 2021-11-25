using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float speed;
    private float angle;
    private Quaternion _rotation;
    private Vector3 direction;
    [SerializeField] private float percentageOfSpeed;



    void Update()
    {
       /* direction = new Vector3(Mathf.Cos((90 + angle)/(2 * Mathf.PI)), Mathf.Sin((90 + angle) / (2 * Mathf.PI)));
        Debug.Log(direction);*/
       // transform.Translate(transform.up * speed * percentageOfSpeed/100 *  Time.deltaTime);
    }

    public void BackGroundSpeed(float _speed, float _angle)
    {
        speed = _speed;
        angle = _angle;
    }
}
