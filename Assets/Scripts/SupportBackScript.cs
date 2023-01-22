using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBackScript : MonoBehaviour
{
    bool done = false;

    void Start()
    {
        Invoke("FirstUpdate", 5);
    }

    void Update()
    {
        if(done == true)
        {
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().anchoredPosition = new Vector3(-250, -250, -5);
                GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().localPosition = new Vector3(GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().localPosition.x, GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().localPosition.y, -5);
            }
        }
    }

    void FirstUpdate()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().anchoredPosition = new Vector3(-250, -250, -5);
            GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().localPosition = new Vector3(GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().localPosition.x, GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().GetComponent<RectTransform>().localPosition.y, -5);
        }
        done = true;
    }

    void FixedUpdate()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
