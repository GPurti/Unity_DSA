using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMap : MonoBehaviour
{
    public List<GameObject> rooms;
    private List<GameObject> circles;
    private List<GameObject> completedRooms;
    public GameObject square;
    public GameObject circle;
    public GameObject completed;
    private float height;
    private float width;


    void Awake()
    {
        circles = new List<GameObject>();
        completedRooms = new List<GameObject>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Invoke("DrawMiniMap", 4);
    }

    private void DrawMiniMap()
    {
        rooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>().instantiatedRooms;
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

        for(int i = 0; i < rooms.Count; i++)
        {
            GameObject circle1 = (GameObject)Instantiate(circle, new Vector3(rooms[i].transform.position.x / 10, rooms[i].transform.position.y / 10, 1), circle.transform.rotation);
            circle1.transform.SetParent(transform);
            circles.Insert(i, circle1);
            if(rooms[i].tag != "CentralRoom") 
                circle1.SetActive(false);

            GameObject completed1 = (GameObject)Instantiate(completed, new Vector3(rooms[i].transform.position.x / 10, rooms[i].transform.position.y / 10, 1), completed.transform.rotation);
            completed1.transform.SetParent(transform);
            completedRooms.Insert(i, completed1);
            completed1.SetActive(false);

            DrawRoom(rooms[i].transform.position / 10);
        }

        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        transform.localScale = new Vector3(400, 400, 0);
        gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-250, -250, -5);
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

    public void UpdateMap(GameObject room)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].GetInstanceID() == room.GetInstanceID() || (rooms[i].tag == "CentralRoom" && room.tag == "CentralRoom"))
            {
                for(int a = 0; a < circles.Count; a++)
                {
                    if (a == i)
                        circles[a].SetActive(true);
                    else
                    {
                        if (circles[a].activeSelf)
                            completedRooms[a].SetActive(true);
                        circles[a].SetActive(false);
                    }
                        
                }
            }
        }
    }

    public void GameOver()
    {
        List<GameObject> completedActiveRooms = new List<GameObject>();
        for(int i = 0; i<completedRooms.Count; i++)
        {
            if (completedRooms[i].active)
            {
               completedActiveRooms.Add(completedRooms[i]);
            }
        }
        if (completedActiveRooms.Count == rooms.Count-1)
        {

            SceneManager.LoadScene("GameOverScene");

#if UNITY_ANDROID
	saveGameInfoInAndroid();
#endif
        }
    }

    private void saveGameInfoInAndroid()
    {
        AndroidJavaObject unityActivity = new AndroidJavaObject("edu.upc.dsa.andoroid_dsa.Backend");

        object[] parameters = new object[3];
        parameters[0] = Player.playerId;
        parameters[1] = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().coins.ToString();
        parameters[2] = "true";
        unityActivity.Call("saveGameInfo", parameters);

        Invoke("closeGame", 5f);
    }

    private void closeGame()
    {
        AndroidJavaObject andClass = new AndroidJavaObject("edu.upc.dsa.andoroid_dsa.activities.GameActivity");
        andClass.Call("finishActivity");
    }
}
