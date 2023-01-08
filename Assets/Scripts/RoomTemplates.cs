using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject centerRoom;

    [HideInInspector] public List<GameObject> instantiatedRooms;

    void Awake()
    {
        instantiatedRooms = new List<GameObject>() { centerRoom };

    }

    public string SaveRooms()
    {
        string infoToSave = "";
        bool done0 = false;
        foreach (GameObject room in instantiatedRooms)
        {
            if (done0 == true)
                infoToSave += "/";
            bool done = false;
            infoToSave += room.transform.name + ":";
            infoToSave += room.transform.position.ToString() + ":";
            foreach (Transform child in room.transform)
            {
                if (child.name == "TopDoor"|| child.name == "BottomDoor" || child.name == "LeftDoor" || child.name == "RightDoor")
                {
                    if (done == false)
                    {
                        infoToSave += child.name;
                        done = true;
                    }
                    else
                        infoToSave += "," + child.name;
                }
            }
            done0 = true;
        }
        return infoToSave;
    }
}
