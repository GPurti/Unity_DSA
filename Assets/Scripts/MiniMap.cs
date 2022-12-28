using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public List<GameObject> rooms;
    public GameObject square;
    public GameObject circle;
    private float height;
    private float width;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        rooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>().instantiatedRooms;
    }

    void Start()
    {
        Invoke("DrawMiniMap", 2);
    }


    private void DrawMiniMap()
    {
        float heightPositive = 0;
        float heightNegative = 0;
        float widthPositive = 0;
        float widthNegative = 0;

        foreach(GameObject room in rooms)
        {
            if (room.transform.position.y >= 0)
                if ((room.transform.position.y / 10 + (float)0.6) > heightPositive)
                    heightPositive = room.transform.position.y / 10 + (float)0.6;
            if (room.transform.position.y <= 0)
                if ((room.transform.position.y / 10 - (float)0.6) < heightNegative)
                    heightNegative = room.transform.position.y / 10 - (float)0.6;
            if (room.transform.position.x >= 0)
                if ((room.transform.position.x / 10 + (float)0.6) > widthPositive)
                    widthPositive = room.transform.position.x / 10 + (float)0.6;
            if (room.transform.position.x <= 0)
                if ((room.transform.position.x / 10 - (float)0.6) < widthNegative)
                    widthNegative = room.transform.position.x / 10 - (float)0.6;
        }
        if (heightPositive > -heightNegative)
            height = 2 * heightPositive;
        else
            height = -2 * heightNegative;
        if (widthPositive > -widthNegative)
            width = 2 * widthPositive;
        else
            width = -2 * widthNegative;
        if (height > width)
            width = height;
        else
            height = width;
        transform.localScale = new Vector3(width, height, 0);

        foreach (GameObject room in rooms)
        {
            if (room.tag == "CentralRoom")
            {
                GameObject circle1 = (GameObject)Instantiate(circle, new Vector3(0, 0, 1), circle.transform.rotation);
                circle1.transform.SetParent(transform);
            }

            DrawRoom(room.transform.position / 10);
        }
        transform.localScale = new Vector3(4, 4, 0);
        transform.position = new Vector3(7, -3, 0);
    }

    private void DrawRoom(Vector3 roomPosition)
    {
        for(float y = roomPosition.y - (float)0.45; y <= roomPosition.y + (float)0.46; y = y + (float)0.1)
        {
            GameObject square1 = (GameObject)Instantiate(square, new Vector3(roomPosition.x - (float)0.45, y, 1), square.transform.rotation);
            GameObject square2 = (GameObject)Instantiate(square, new Vector3(roomPosition.x + (float)0.45, y, 1), square.transform.rotation);
            square1.transform.SetParent(transform);
            square2.transform.SetParent(transform);
        }
        for(float x = roomPosition.x - (float)0.35; x <= roomPosition.x + (float)0.36; x = x + (float)0.1)
        {
            GameObject square1 = (GameObject)Instantiate(square, new Vector3(x, roomPosition.y - (float)0.45, 1), square.transform.rotation);
            GameObject square2 = (GameObject)Instantiate(square, new Vector3(x, roomPosition.y + (float)0.45, 1), square.transform.rotation);
            square1.transform.SetParent(transform);
            square2.transform.SetParent(transform);
        }
    }



}
