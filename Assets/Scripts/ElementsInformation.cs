using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ElementsInformation : MonoBehaviour
{
    private string apiUrl = "https://your_api_url.com/shop/purchase/{idUser}";
    private string token = "your_token";

    void Start()
    {
        StartCoroutine(GetDeviceInfo());
    }

    IEnumerator GetDeviceInfo()
    {
        // Create a new UnityWebRequest object to make the GET request
        UnityWebRequest www = UnityWebRequest.Get(apiUrl);

        // Set the headers on the request
        www.SetRequestHeader("Authorization", "Bearer " + token);
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            // Handle any errors that occurred during the request
            Debug.Log(www.error);
        }
        else
        {
            // Parse the JSON response
            string jsonResponse = www.downloadHandler.text;
            DeviceData deviceData = JsonUtility.FromJson<DeviceData>(jsonResponse);

            // Use the device data to determine which devices have been purchased
            foreach (Device device in deviceData.devices)
            {
                if (device.isPurchased)
                {
                    Debug.Log("Device " + device.id + " is purchased");
                }
            }
        }
    }
}

[System.Serializable]
public class DeviceData
{
    public Device[] devices;
}

[System.Serializable]
public class Device
{
    public string id;
    public bool isPurchased;
}

