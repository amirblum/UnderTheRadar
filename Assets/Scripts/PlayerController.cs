using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float _moveSpeed;
	private Rigidbody _rigidBody;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * _moveSpeed * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("You Lost!");
			return;
		}

		if (collision.gameObject.CompareTag("Goal"))
		{
			Debug.Log("You Won!");
			return;
		}
	}
	
}
