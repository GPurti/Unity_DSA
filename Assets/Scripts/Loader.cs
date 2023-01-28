using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loader : MonoBehaviour
{
    public GameObject centralRoom;            //CentralRoom prefab to instantiate.
    public GameObject miniMap;
    public ArrayList elementsAvailable;

    void Awake()
    {
        elementsAvailable = new ArrayList();
        

        
        Instantiate(centralRoom);
        Instantiate(miniMap);

        //Change color of the player
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.changePlayerColor(Color.red);


    }
}
