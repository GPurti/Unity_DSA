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
    private bool mouseDown = false;
    float temps;

    void Start()
    {
        elementsAvailable = Player.elementsAvailable;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            temps = Time.time;
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            if ((Time.time - temps) < 0.2 && ammoCount > 0)
            {
                mouseDown = false;
                // Instantiate a bullet at the position of the camera
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                // Get the direction of the mouse pointer
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 direction = (mousePosition - transform.position).normalized;

                // Add force to the bullet in the direction of the mouse pointer
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);

                bullet.gameObject.GetComponent<Bullet>().setBulletColor(elementsAvailable);

                ammoCount--;
                //bulletActive = true;
            }
        }
    }

    public void IncreaseAmo()
    {
        ammoCount = ammoCount + 4;
    }
}
