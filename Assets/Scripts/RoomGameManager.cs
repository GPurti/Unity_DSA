using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomGameManager : MonoBehaviour
{
    private RoomBoardManager roomBoardManager;
    private int level = 3;

    public float levelStartDelay = 2f;                        //Time to wait before starting level, in seconds.
    public float turnDelay = 0.1f;                            //Delay between each Player turn.
    public int playerCoinPoints = 0;                        //Starting value for Player coin points.
    public int playerHealthPoints = 100;

    [HideInInspector] public bool initiated = false;

    [HideInInspector] public bool playersTurn = true;        //Boolean to check if it's players turn, hidden in inspector but public.

    private List<Enemy> enemies;
    private int enemiesNumber;
    private bool enemiesMoving;

  

    void Awake()
    {
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        enemiesNumber = 0;
        enemies = new List<Enemy>();

        //Get a component reference to the attached BoardManager script
        roomBoardManager = GetComponent<RoomBoardManager>();

        if (gameObject.tag == "CentralRoom")
        {
            InitGame();
           

        }
    }

    //Initializes the game for each level.
    public void InitGame()
    {
        if (gameObject.tag == "CentralRoom")
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "GameOver")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        initiated = true;
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        roomBoardManager.SetupScene(level);
        
    }

    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
        MiniMap miniMap = GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>();
        miniMap.GameOver();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canOpenDoor = true;
    }

    void Update()
    {
        if (playersTurn || enemiesMoving)
            return;
        StartCoroutine(MoveEnemies());
    }

    public void CheckIfGameOver()
    {
        if(enemiesNumber == 0)
        {
            GameOver();
        }
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
        enemiesNumber++;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].name == enemy.name)
            {
                enemies.RemoveAt(i);
            }
        }
        enemiesNumber--;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }
}
