using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private float cameraSpeed;

    void Update()
    {
        Vector3 target = new Vector3();    
         target.x = character.transform.position.x;
         target.y = character.transform.position.y + 4;
         target.z = transform.position.x - 10;

        Vector3 pos = Vector3.Lerp(transform.position, target, cameraSpeed * 2.5f * Time.deltaTime);
        transform.position = pos;       
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3f, 3f), transform.position.y, transform.position.z);
    }

    public void StartPosition()
    {
        transform.position = new Vector3(0, 4, -10);
    }

    public void CameraSpeed(float speed)
    {
        cameraSpeed = speed;
    }
}
