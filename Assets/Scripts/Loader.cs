using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject centralRoom;            //CentralRoom prefab to instantiate.
    public GameObject miniMap;
    public string info;

    void Awake()
    {

#if UNITY_ANDROID
	ReloadGame();
#endif

        Instantiate(centralRoom);
        Instantiate(miniMap);
        
        //Change color of the player
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SpriteRenderer m_Sprite = player.GetComponent<SpriteRenderer>();
        m_Sprite.color = Color.blue;
        
        //Select a gadget


    }

    private void ReloadGame()
    {
        AndroidJavaObject unityActivity = new AndroidJavaObject("com.unity3d.player.Backend");

        this.info = unityActivity.Call<string>("loadGame");
        Debug.Log(info);
    }

    //public void ChangeSpriteColor()
    //{
      //  GameObject player = (GameObject)FindObjectOfType(typeof(GameObject));
        //player.sprit
    //}
}
