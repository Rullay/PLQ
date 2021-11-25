using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject menuManager;
    [SerializeField] private Transform charTransform;
    [SerializeField] private List<GameObject> backGrounds;
    [SerializeField] private List<GameObject> backGroundsType;
    [SerializeField] private List<GameObject> Obstacles;
    [SerializeField] private List<GameObject> BigObstacles;
    [SerializeField] private List<GameObject> createdObstacle;
    [SerializeField] private List<GameObject> tunelObstacle;
    [SerializeField] private List<GameObject> actualObstacle = new List<GameObject>();
    [SerializeField] private int chanceObstacle;
    [SerializeField] private int chanceBigObstacle;


    private GameObject firstbackGround;
    private GameObject lastbackGround;
    [SerializeField] private float score;
    [SerializeField] private int actualBackGround;

    private List<float> blockPosition = new List<float>();
    private int numberObstacle;
    private float bottomBound;
    private float upperBound;
    private bool isTunel;
    private int quantityTunel;
    private int tunnelCounter = 0;
    private int random;
    private float interval;

    // параметры кораблей
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject spriteModel;
 


    void Start()
    {
        StartBackGround();
        StartGenerator();
    }

       
    void Update()
    {
        CreateBackGround();
        score = Mathf.Round(charTransform.position.y * 50);
        menuManager.GetComponent<MenuManager>().Score(score);
        character.GetComponent<Character>().MoveSpeed(score);
        Generator(); 
    }



    // Back Ground

    void StartBackGround()
    {
        backGrounds.Add(Instantiate(backGround, new Vector3(0, 0, 1), Quaternion.identity));
        backGrounds.Add(Instantiate(backGround, new Vector3(0, backGrounds[0].transform.position.y + backGrounds[0].transform.localScale.y, 1), Quaternion.identity));
        backGrounds.Add(Instantiate(backGround, new Vector3(0, backGrounds[0].transform.position.y - backGrounds[0].transform.localScale.y, 1), Quaternion.identity));
        firstbackGround = backGrounds[1];
        lastbackGround = backGrounds[2];
    }

    void CreateBackGround()
    {
        if (charTransform.position.y >= firstbackGround.transform.position.y - backGround.transform.localScale.y * 0.5f)
        {
            lastbackGround.transform.position = new Vector3(transform.position.x, firstbackGround.transform.position.y + firstbackGround.transform.localScale.y * 0.5f + lastbackGround.transform.localScale.y * 0.5f, 1);
            firstbackGround = lastbackGround;
            SearchLastBackGround();
        }
    }

    void SearchLastBackGround()
    {
        for (int i = 0; i < backGrounds.Count; i++)
        {
            if (lastbackGround.transform.position.y > backGrounds[i].transform.position.y)
            {
                lastbackGround = backGrounds[i];
            }
        }
    }

    public void DestroyBackGround()
    {
        for (int i = 0; i < backGrounds.Count; i++)
        {
            Destroy(backGrounds[i]);
        }
        backGrounds.Clear();
    }

    // Generator

    void StartGenerator()
    {
        upperBound = bottomBound = 10;
        blockPosition.Add(0);
        for (int i = 0; i < 9; i++)
        {
            random = Random.Range(0, 100);

            if (random > 65)
            {
                actualObstacle = BigObstacles;
                ObstacleGenerator();
            }
            else
            {
                actualObstacle = Obstacles;
                ObstacleGenerator();
            }
        }
    }

    void ObstacleGenerator()
    {

        numberObstacle = Random.Range(0, actualObstacle.Count);

        if (isTunel == false)
        {
            interval = Random.Range(1f, 2f);
        }
        else
        {
            if (tunnelCounter == 1)
            {
                interval = Random.Range(1f, 2f);
            }
            else
            {
                interval = 0;
            }

        }

        float PosX = Random.Range(-5 + actualObstacle[numberObstacle].transform.localScale.x / 2, 5 - actualObstacle[numberObstacle].transform.localScale.x / 2);
        float PosY = upperBound + interval + actualObstacle[numberObstacle].transform.localScale.y / 2;

        createdObstacle.Add(Instantiate(actualObstacle[numberObstacle], new Vector3(PosX, PosY, 0), Quaternion.identity));
        upperBound = PosY + actualObstacle[numberObstacle].transform.localScale.y / 2;
        blockPosition.Add(PosY);

        // доп Obstacle слева
        if (PosX - actualObstacle[numberObstacle].transform.localScale.x / 2 + 5 >= 2)
        {
            random = Random.Range(0, 100);
            if (random < chanceObstacle)
            {
                int ObstacleIndex = Random.Range(0, Obstacles.Count);
                float PosX_Dop = Random.Range(-5 + Obstacles[ObstacleIndex].transform.localScale.x / 2, PosX - actualObstacle[numberObstacle].transform.localScale.x / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2);
                float PosY_Dop;
                if (actualObstacle[numberObstacle].transform.localScale.y > 1 + PosX)
                {
                    PosY_Dop = Random.Range(PosY + actualObstacle[numberObstacle].transform.localScale.y / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2, PosY - actualObstacle[numberObstacle].transform.localScale.y / 2 + Obstacles[ObstacleIndex].transform.localScale.x / 2);
                }
                else
                {
                    PosY_Dop = PosY;
                }
                createdObstacle.Add(Instantiate(Obstacles[ObstacleIndex], new Vector3(PosX_Dop, PosY_Dop, 0), Quaternion.identity));

                // доп Obstacle сверху
                if (PosY + actualObstacle[numberObstacle].transform.localScale.y / 2 - PosY_Dop - Obstacles[ObstacleIndex].transform.localScale.y / 2 >= 2)
                {
                    random = Random.Range(0, 100);
                    if (random < chanceObstacle)
                    {
                        float PosX_Dop_dop = Random.Range(-5 + Obstacles[ObstacleIndex].transform.localScale.x / 2, PosX - actualObstacle[numberObstacle].transform.localScale.x / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2);
                        float PosY_Dop_dop = Random.Range(PosY_Dop + Obstacles[ObstacleIndex].transform.localScale.y / 2, PosY + actualObstacle[numberObstacle].transform.localScale.y / 2 - Obstacles[ObstacleIndex].transform.localScale.y / 2);
                        createdObstacle.Add(Instantiate(Obstacles[ObstacleIndex], new Vector3(PosX_Dop_dop, PosY_Dop_dop, 0), Quaternion.identity));
                    }
                }

                // доп Obstacle снизу
                if (PosY_Dop - Obstacles[ObstacleIndex].transform.localScale.y / 2 - (PosY - actualObstacle[numberObstacle].transform.localScale.y / 2) >= 2)
                {
                    random = Random.Range(0, 100);
                    if (random < chanceObstacle)
                    {
                        float PosX_Dop_dop = Random.Range(-5 + Obstacles[ObstacleIndex].transform.localScale.x / 2, PosX - actualObstacle[numberObstacle].transform.localScale.x / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2);
                        float PosY_Dop_dop = Random.Range((PosY - actualObstacle[numberObstacle].transform.localScale.y / 2) + Obstacles[ObstacleIndex].transform.localScale.y / 2, PosY_Dop - Obstacles[ObstacleIndex].transform.localScale.y / 2);
                        createdObstacle.Add(Instantiate(Obstacles[ObstacleIndex], new Vector3(PosX_Dop_dop, PosY_Dop_dop, 0), Quaternion.identity));
                    }
                }


            }
        }

        // доп Obstacle справа
        if (Mathf.Abs(PosX + actualObstacle[numberObstacle].transform.localScale.x / 2 - 5) >= 2)
        {
            random = Random.Range(0, 100);
            if (random < chanceObstacle)
            {
                int ObstacleIndex = Random.Range(0, Obstacles.Count);
                float PosX_Dop = Random.Range(5 - Obstacles[ObstacleIndex].transform.localScale.x / 2, PosX + actualObstacle[numberObstacle].transform.localScale.x / 2 + Obstacles[ObstacleIndex].transform.localScale.x / 2);
                float PosY_Dop;
                if (actualObstacle[numberObstacle].transform.localScale.y > 1 + PosX)
                {
                    PosY_Dop = Random.Range(PosY + actualObstacle[numberObstacle].transform.localScale.y / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2, PosY - actualObstacle[numberObstacle].transform.localScale.y / 2 + Obstacles[ObstacleIndex].transform.localScale.x / 2);
                }
                else
                {
                    PosY_Dop = PosY;
                }
                createdObstacle.Add(Instantiate(Obstacles[ObstacleIndex], new Vector3(PosX_Dop, PosY_Dop, 0), Quaternion.identity));

                // доп Obstacle сверху
                if (PosY + actualObstacle[numberObstacle].transform.localScale.y / 2 - PosY_Dop - Obstacles[ObstacleIndex].transform.localScale.y / 2 >= 2)
                {

                    random = Random.Range(0, 100);
                    if (random < chanceObstacle)
                    {
                        float PosX_Dop_dop = Random.Range(-5 + Obstacles[ObstacleIndex].transform.localScale.x / 2, PosX - actualObstacle[numberObstacle].transform.localScale.x / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2);
                        float PosY_Dop_dop = Random.Range(PosY_Dop + Obstacles[ObstacleIndex].transform.localScale.y / 2, PosY + actualObstacle[numberObstacle].transform.localScale.y / 2 - Obstacles[ObstacleIndex].transform.localScale.y / 2);
                        createdObstacle.Add(Instantiate(Obstacles[ObstacleIndex], new Vector3(PosX_Dop_dop, PosY_Dop_dop, 0), Quaternion.identity));
                    }
                }

                // доп Obstacle снизу
                if (PosY_Dop - Obstacles[ObstacleIndex].transform.localScale.y / 2 - (PosY - actualObstacle[numberObstacle].transform.localScale.y / 2) >= 2)
                {

                    random = Random.Range(0, 100);
                    if (random < chanceObstacle)
                    {
                        float PosX_Dop_dop = Random.Range(-5 + Obstacles[ObstacleIndex].transform.localScale.x / 2, PosX - actualObstacle[numberObstacle].transform.localScale.x / 2 - Obstacles[ObstacleIndex].transform.localScale.x / 2);
                        float PosY_Dop_dop = Random.Range((PosY - actualObstacle[numberObstacle].transform.localScale.y / 2) + Obstacles[ObstacleIndex].transform.localScale.y / 2, PosY_Dop - Obstacles[ObstacleIndex].transform.localScale.y / 2);
                        createdObstacle.Add(Instantiate(Obstacles[ObstacleIndex], new Vector3(PosX_Dop_dop, PosY_Dop_dop, 0), Quaternion.identity));
                    }
                }
            }
        }
    }

    void Generator()
    {

        if (charTransform.position.y > bottomBound)
        {

            for (int i = 0; i < blockPosition.Count; i++)
            {
                if (blockPosition[i] == bottomBound)
                {
                    blockPosition.RemoveAt(i);
                }
            }
            bottomBound = blockPosition[0];
            for (int i = 0; i < blockPosition.Count; i++)
            {
                if (blockPosition[i] < bottomBound)
                {
                    bottomBound = blockPosition[i];
                }
            }

            if (isTunel == false)
            {
                random = Random.Range(0, 100);
               // менахика тунелей 
               /* if (random > 97)                          
                {
                    actualObstacle = tunelObstacle;
                    quantityTunel = Random.Range(4, 8);
                    isTunel = true;
                }*/
                //else
                if (random < chanceBigObstacle)
                {
                    actualObstacle = BigObstacles;
                    ObstacleGenerator();
                }
                else
                {
                    actualObstacle = Obstacles;
                    ObstacleGenerator();
                }
            }

            if (isTunel == true)
            {
                tunnelCounter++;
                ObstacleGenerator();
                if (tunnelCounter == quantityTunel)
                {
                    isTunel = false;
                    tunnelCounter = 0;
                }
            }
            DestroyObstacle();
        }
    }

    void DestroyObstacle()
    {
        for (int i = 0; i < createdObstacle.Count; i++)
        {
            if (createdObstacle[i].transform.position.y < bottomBound - 10)
            {
                Destroy(createdObstacle[i]);
                createdObstacle.RemoveAt(i);
            }
        }
    }

    // Game

    public void Restart()
    {
        character.GetComponent<Character>().StartPosition();
        for (int i = 0; i < createdObstacle.Count; i++)
        {
            Destroy(createdObstacle[i]);          
        }
        createdObstacle.Clear();
        DestroyBackGround();
        score = 0;
        StartBackGround();
        StartGenerator();
        character.GetComponent<Character>().isAlive();
    }

    public void MineMenu()
    {
        character.GetComponent<Character>().StartPosition();
        for (int i = 0; i < createdObstacle.Count; i++)
        {
            Destroy(createdObstacle[i]);
        }
        createdObstacle.Clear();
        DestroyBackGround();
        score = 0;
        StartBackGround();
    }

    public void Dead()
    {
        menuManager.GetComponent<MenuManager>().DeathMenu();
    }


    public void ContinionGame()
    {
        character.GetComponent<BoxCollider2D>().enabled = false;
        character.GetComponent<Character>().isAlive();
        StartCoroutine(NoCollision());        
    }


    IEnumerator NoCollision()
    {
        yield return new WaitForSeconds(2);
        character.GetComponent<BoxCollider2D>().enabled = true;
    }

    // UI

    public void LeftButton()
    {
        actualBackGround--;
        if (actualBackGround < 0)
        {
            actualBackGround = backGroundsType.Count - 1;
        }
        DestroyBackGround();
        backGround = backGroundsType[actualBackGround];
        StartBackGround();
    }

    public void RightButton()
    {
        actualBackGround++;
        if (actualBackGround > backGroundsType.Count - 1)
        {
           actualBackGround = 0;
        }
        DestroyBackGround();
        backGround = backGroundsType[actualBackGround];
        StartBackGround();
    }



    // Character Model
    public void ParametersShips(CharacterModel charModel)
    {
        description = charModel.Description;
        icon = charModel.Icon;
        rotateSpeed = charModel.RotateSpeed;
        moveSpeed = charModel.MoveSpeed;
        spriteModel.GetComponent<SpriteRenderer>().sprite = charModel.Icon;
    }
    

}
