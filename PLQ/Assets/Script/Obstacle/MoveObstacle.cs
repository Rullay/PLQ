using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : Obstacle
{

    [SerializeField] private float borderOfMovement;
    [SerializeField] private float ObstacleSpeed;
    private float startPosition;

    private void Start()
    {
        startPosition = transform.position.x;
    }

    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (transform.position.x > startPosition + borderOfMovement || transform.position.x < startPosition  - borderOfMovement)
        {
            transform.Rotate(0, 0, 180);
        }

        transform.Translate(Vector2.right * ObstacleSpeed * Time.deltaTime);
    }

}
