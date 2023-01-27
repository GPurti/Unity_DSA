using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 1000f; // the speed of the bullet

    public int ammoCount = 10;

    //public int elements;
    public SpriteRenderer bulletSprite;

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bulletSprite = GetComponent<SpriteRenderer>();
        player.elementsAvailable.Add("Fire");
        if ((player.elementsAvailable[0] == "Fire" || player.elementsAvailable[1] == "Fire" || player.elementsAvailable[2] == "Fire" || player.elementsAvailable[3] == "Fire") && player.elementsAvailable.Count == 1)
        {
            bulletSprite.color = Color.red;
        }


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
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().damagedByPlayer();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseAmo()
    {

        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        ammoCount = ammoCount + 4;
    }
}
