using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bulletPrefab;

    public float bulletSpeed = 1000f; // the speed of the bullet

    public int ammoCount = 10;
    public bool bulletActive = false;

    public SpriteRenderer bulletSprite;
    public ArrayList elementsAvailable;

    void Start()
    {
        elementsAvailable = new ArrayList();
        //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        elementsAvailable.Add("Fire");
        elementsAvailable.Add("Cloud");
        int result = this.setBulletColor(elementsAvailable);

        if (result == 1)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else if (result == 10)
        {
            GetComponent<SpriteRenderer>().material.color = Color.blue;
        }
        else if (result == 100)
        {
            GetComponent<SpriteRenderer>().material.color = Color.green;
        }
        else if (result == 1000)
        {
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        else if (result == 11)
        {
            GetComponent<SpriteRenderer>().material.color = Color.magenta;
        }
        else if (result == 110)
        {
            GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }
        else if (result == 1001)
        {
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
        }
        else if (result == 1100)
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
        }
        else{
            GetComponent<SpriteRenderer>().material.color = Color.black;


        }


    }
    int setBulletColor(ArrayList elementsAvailable)
    {
        int sum = 0;
        foreach (string element in elementsAvailable)
        {
            if (element == "Fire")
            {
                sum = sum + 1;
            }
            else if(element=="Water"){
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
        return sum; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammoCount > 0)
        {
            // Instantiate a bullet at the position of the camera
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Get the direction of the mouse pointer
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = (mousePosition - transform.position).normalized;

            // Add force to the bullet in the direction of the mouse pointer
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
            ammoCount--;
            //bulletActive = true;
        }
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

    public void IncreaseAmo()
    {
        ammoCount = ammoCount + 4;
    }
}
