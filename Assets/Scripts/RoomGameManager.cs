using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGameManager : MonoBehaviour
{
    private RoomBoardManager roomBoardManager;
    private int level = 3;


    void Awake()
    {
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

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
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        roomBoardManager.SetupScene(level);

    }
}
