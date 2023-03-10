using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int doorSide;
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (doorSide == 1)
            {
                //Need Bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                GameObject bottomRoom = (GameObject)Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                templates.instantiatedRooms.Add(bottomRoom);

                foreach(Transform child in this.transform.parent)
                {
                    if (child.name == "TopDoor")
                        child.GetComponent<Door>().neighbor=true;
                }

                foreach (Transform child in bottomRoom.transform)
                {
                    if (child.name == "BottomDoor")
                    {
                        GameObject.Destroy(child.gameObject);
                        return;
                    }
                }
            }
            else if (doorSide == 2)
            {
                //Need Top door
                rand = Random.Range(0, templates.topRooms.Length);
                GameObject topRoom = (GameObject)Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                templates.instantiatedRooms.Add(topRoom);

                foreach (Transform child in this.transform.parent)
                {
                    if (child.name == "BottomDoor")
                        child.GetComponent<Door>().neighbor = true;
                }

                foreach (Transform child in topRoom.transform)
                {
                    if (child.name == "TopDoor")
                    {
                        GameObject.Destroy(child.gameObject);
                        return;
                    }
                }
            }
            else if (doorSide == 3)
            {
                //Need Right door
                rand = Random.Range(0, templates.rightRooms.Length);
                GameObject rightRoom = (GameObject)Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                templates.instantiatedRooms.Add(rightRoom);

                foreach (Transform child in this.transform.parent)
                {
                    if (child.name == "LeftDoor")
                        child.GetComponent<Door>().neighbor = true;
                }

                foreach (Transform child in rightRoom.transform)
                {
                    if (child.name == "RightDoor")
                    {
                        GameObject.Destroy(child.gameObject);
                        return;
                    }
                }
            }
            else if (doorSide == 4)
            {
                //Need Left door
                rand = Random.Range(0, templates.leftRooms.Length);
                GameObject leftRoom = (GameObject)Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                templates.instantiatedRooms.Add(leftRoom);

                foreach (Transform child in this.transform.parent)
                {
                    if (child.name == "RightDoor")
                        child.GetComponent<Door>().neighbor = true;
                }

                foreach (Transform child in leftRoom.transform)
                {
                    if (child.name == "LeftDoor")
                    {
                        GameObject.Destroy(child.gameObject);
                        return;
                    }
                }
            }
            spawned = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
