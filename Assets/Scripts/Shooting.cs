using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 1000f; // the speed of the bullet

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate a bullet at the position of the camera
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Get the direction of the mouse pointer
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = (mousePosition - transform.position).normalized;

            // Add force to the bullet in the direction of the mouse pointer
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
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
    /*
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           Shoot();
        }

    }*/

    void Shoot()
    {
        
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
    }
}
