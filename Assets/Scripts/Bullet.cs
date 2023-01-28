using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

	void Start()
	{

	
		UnityEngine.Object.Destroy(gameObject, 4f);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().damagedByPlayer();
            Destroy(gameObject);
            //bulletActive = false;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            //bulletActive = false;
        }
        else if (collision.gameObject.tag == "Door")
        {
            Destroy(gameObject);
            //bulletActive = false;
        }
    }

    public void setBulletColor(ArrayList elementsAvailable)
    {
        int sum = 0;
        foreach (string element in elementsAvailable)
        {
            if (element == "Fire")
            {
                sum = sum + 1;
            }
            else if (element == "Water")
            {
                sum = sum + 10;

            }
            else if (element == "Earth")
            {
                sum = sum + 100;

            }
            else if (element == "Cloud")
            {
                sum = sum + 1000;

            }
        }
        if (sum == 1)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else if (sum == 10)
        {
            GetComponent<SpriteRenderer>().material.color = Color.blue;
        }
        else if (sum == 100)
        {
            GetComponent<SpriteRenderer>().material.color = Color.green;
        }
        else if (sum == 1000)
        {
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        else if (sum == 11)
        {
            GetComponent<SpriteRenderer>().material.color = Color.magenta;
        }
        else if (sum == 110)
        {
            GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }
        else if (sum == 1001)
        {
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
        }
        else if (sum == 1100)
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
        }
        else
        {
            GetComponent<SpriteRenderer>().material.color = Color.black;
        }
    }
}
