using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : Obstacle
{
   [SerializeField] private float rotateSpeed;



    
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.Rotate(0, 0, rotateSpeed * 360 * Time.deltaTime);
    }
}
