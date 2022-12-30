using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomScript : MonoBehaviour
{
    float lerpDuration = (float)0.5;
    Vector3 endValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (this.transform.parent.gameObject.tag == "CentralRoom")
            {
                other.GetComponent<Player>().roomGameManager = this.transform.parent.gameObject.GetComponent<RoomGameManager>();
            }
            else {
                if (this.transform.parent.gameObject.GetComponent<RoomGameManager>().initiated == false)
                {
                    this.transform.parent.gameObject.GetComponent<RoomGameManager>().InitGame();
                    other.GetComponent<Player>().roomGameManager = this.transform.parent.gameObject.GetComponent<RoomGameManager>();
                }
            }

            MiniMap miniMap = GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>();
            miniMap.UpdateMap(this.transform.parent.gameObject);

            endValue = new Vector3(this.transform.parent.position.x, this.transform.parent.position.y, -10);
            Component camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Component>();
            StartCoroutine(updateCamera(camera));
        }
    }

    IEnumerator updateCamera(Component camera)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        camera.transform.position = endValue;
    }
}
