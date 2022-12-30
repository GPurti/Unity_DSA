using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool neighbor = false;

    void Start()
    {
        Invoke("WallUpDoors", 2);
    }

    private void WallUpDoors()
    {
        if(neighbor == false)
        {
            if(this.transform.name == "BottomDoor")
            {
                GameObject wall1 = (GameObject)Instantiate(findSquare());
                GameObject wall2 = (GameObject)Instantiate(findSquare());
                wall1.transform.SetParent(findWalls().transform);
                wall2.transform.SetParent(findWalls().transform);
                wall1.transform.position = new Vector2(this.transform.parent.position.x + (float)-0.5, this.transform.parent.position.y + (float)-4.5);
                wall2.transform.position = new Vector2(this.transform.parent.position.x + (float)0.5, this.transform.parent.position.y + (float)-4.5);
            }
            if (this.transform.name == "TopDoor")
            {
                GameObject wall1 = (GameObject)Instantiate(findSquare());
                GameObject wall2 = (GameObject)Instantiate(findSquare());
                wall1.transform.SetParent(findWalls().transform);
                wall2.transform.SetParent(findWalls().transform);
                wall1.transform.position = new Vector2(this.transform.parent.position.x + (float)-0.5, this.transform.parent.position.y + (float)4.5);
                wall2.transform.position = new Vector2(this.transform.parent.position.x + (float)0.5, this.transform.parent.position.y + (float)4.5);
            }
            if (this.transform.name == "RightDoor")
            {
                GameObject wall1 = (GameObject)Instantiate(findSquare());
                GameObject wall2 = (GameObject)Instantiate(findSquare());
                wall1.transform.SetParent(findWalls().transform);
                wall2.transform.SetParent(findWalls().transform);
                wall1.transform.position = new Vector2(this.transform.parent.position.x + (float)4.5, this.transform.parent.position.y + (float)-0.5);
                wall2.transform.position = new Vector2(this.transform.parent.position.x + (float)4.5, this.transform.parent.position.y + (float)0.5);
            }
            if (this.transform.name == "LeftDoor")
            {
                GameObject wall1 = (GameObject)Instantiate(findSquare());
                GameObject wall2 = (GameObject)Instantiate(findSquare());
                wall1.transform.SetParent(findWalls().transform);
                wall2.transform.SetParent(findWalls().transform);
                wall1.transform.position = new Vector2(this.transform.parent.position.x + (float)-4.5, this.transform.parent.position.y + (float)-0.5);
                wall2.transform.position = new Vector2(this.transform.parent.position.x + (float)-4.5, this.transform.parent.position.y + (float)0.5);
            }
            Destroy(this.gameObject);
        }
    }

    private GameObject findSquare()
    {
        foreach(Transform child in this.transform.parent)
        {
            if(child.name == "Walls")
            {
                foreach (Transform child1 in child)
                {
                    if (child1.name == "Square")
                        return child1.gameObject;
                }
            }
        }
        return null;
    }

    private GameObject findWalls()
    {
        foreach (Transform child in this.transform.parent)
        {
            if (child.name == "Walls")
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
