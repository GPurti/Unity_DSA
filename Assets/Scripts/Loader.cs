using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject centralRoom;            //CentralRoom prefab to instantiate.
    public GameObject miniMap;                //Minimap

    void Awake()
    {
        Instantiate(centralRoom);
        Instantiate(miniMap);
    }
}
