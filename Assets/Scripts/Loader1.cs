using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loader1 : MonoBehaviour
{

    void Awake()
    {


	Invoke("closeGame", 5);

    }

	private void closeGame()
	{
#if UNITY_ANDROID
		AndroidJavaObject andClass = new AndroidJavaObject("edu.upc.dsa.andoroid_dsa.activities.GameActivity");
		andClass.Call("finishActivity");
#endif

	}
}
