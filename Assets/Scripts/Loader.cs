using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loader : MonoBehaviour
{
    public GameObject centralRoom;            //CentralRoom prefab to instantiate.
    public GameObject miniMap;
    public string info;
    public ArrayList elementsAvailable;
    public ArrayList infoExample;

    void Awake()
    {

#if UNITY_ANDROID
	ReloadGame();
#endif

        Instantiate(centralRoom);
        Instantiate(miniMap);

        //Change color of the player
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.changePlayerColor(Color.red);


        //Gets information of which gadgets are available for this user 
        //Available gadgets are the ones that has bought
        this.setAvailableElements();



    }

    private void ReloadGame()
    {
        AndroidJavaObject unityActivity = new AndroidJavaObject("com.unity3d.player.Backend");

        this.info = unityActivity.Call<string>("loadGame");
        Debug.Log(info);
    }

    public void setAvailableElements()
    {
        elementsAvailable = new ArrayList();
        infoExample = new ArrayList();

        infoExample.Add("water");
        infoExample.Add("fire");

        //This info must be extracted from info of the user send by android
        
        foreach (string element in infoExample)
        {
           
            if (element == "water")
            {
                elementsAvailable.Add("water");
                SelectGadget water = GameObject.FindGameObjectWithTag("Water").GetComponent<SelectGadget>();
                water.destroyMissing();
            }
            else if (element == "fire")
            {
                elementsAvailable.Add("fire");
                SelectGadget fire = GameObject.FindGameObjectWithTag("Fire").GetComponent<SelectGadget>();
                fire.destroyMissing();
            }
            else if (element == "earth")
            {
                elementsAvailable.Add("earth");
                SelectGadget earth = GameObject.FindGameObjectWithTag("Fire").GetComponent<SelectGadget>();
                earth.destroyMissing();
            }
            else if (element == "cloud")
            {
                elementsAvailable.Add("cloud");
                SelectGadget cloud = GameObject.FindGameObjectWithTag("Cloud").GetComponent<SelectGadget>();
                cloud.destroyMissing();
            }
        }
    }
}
