using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
	public float speed = 10f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;


	void Start()
	{
		UnityEngine.Object.Destroy(gameObject, 4f);
	}
}
