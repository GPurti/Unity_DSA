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
    }

    private void ReloadGame()
    {
        AndroidJavaObject unityActivity = new AndroidJavaObject("com.unity3d.player.Backend");

        this.info = unityActivity.Call<string>("loadGame");
        Debug.Log(info);
    }
}
