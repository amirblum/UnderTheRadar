using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] AudioClip _winClip;
	[SerializeField] GameObject _winUI;
	[SerializeField] GameObject _loseUI;
	[SerializeField] AudioClip _loseClip;
	[SerializeField] Radar _radar;
	[SerializeField] float _moveSpeed;
	private Rigidbody _rigidBody;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * _moveSpeed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(_radar, transform.position, Quaternion.identity);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("You Lost!");
			GetComponent<AudioSource>().PlayOneShot(_loseClip);
//			SceneManager.LoadScene(0);
//			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
			Instantiate(_loseUI);
			Time.timeScale = 0f;
			return;
		}

		if (collision.gameObject.CompareTag("Goal"))
		{
			Debug.Log("You Won!");
			GetComponent<AudioSource>().PlayOneShot(_winClip);
			Instantiate(_winUI);
			Time.timeScale = 0f;
		}
	}
	
}
