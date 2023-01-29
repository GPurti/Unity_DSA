using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Room" || other.tag == "Bullet")
            return;
        Destroy(other.gameObject);
    }
}
