using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 10f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;

	// Use this for initialization
	void Start()
	{

		rb.velocity = transform.right * speed;

		/*// Check if the left arrow key was released during the current frame
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			rb.velocity = transform.left * speed;
		}*/

	}
	
	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.tag =="Enemy")
		{
			Instantiate(impactEffect, transform.position, transform.rotation);
			hitInfo.GetComponent<Enemy>().damagedByPlayer();
			Destroy(gameObject);
		}
		if (hitInfo.tag == "Wall")
		{
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

}
