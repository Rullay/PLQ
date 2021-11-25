using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleStraight : Obstacle
{
    [SerializeField] private float borderOfMovement;
    [SerializeField] private float ObstacleSpeed;
 
  
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (transform.position.x > borderOfMovement)
        {
            transform.position = new Vector2(-borderOfMovement, transform.position.y);
        }
        transform.Translate(Vector2.right * ObstacleSpeed * Time.deltaTime);
    }
}
