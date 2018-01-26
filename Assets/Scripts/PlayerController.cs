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

	private bool _paused;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * _moveSpeed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_paused)
			{
				TogglePause();
				return;
			}
			
			Instantiate(_radar, transform.position, Quaternion.identity);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
	}

	private void TogglePause()
	{
		Time.timeScale = _paused ? 1f : 0f;
		_paused = !_paused;
	}

	private void OnGUI()
	{
		if (!_paused)
		{
			return;
		}

		var middleRect = new Rect(Screen.width / 2 - 175, Screen.height / 2 - 60, 350, 120);
		var middleStyle =
			new GUIStyle {fontSize = 60, alignment = TextAnchor.MiddleCenter, normal = {textColor = Color.white}}; 
		
		GUI.Label(middleRect, "PAUSED", middleStyle);

		middleRect.y += 120;

		if (GUI.Button(middleRect, "Back to Menu", new GUIStyle(GUI.skin.button) { fontSize = 50}))
		{
			SceneManager.LoadScene(0);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			GetComponent<AudioSource>().PlayOneShot(_loseClip);
			Instantiate(_loseUI);
			Time.timeScale = 0f;
			return;
		}

		if (collision.gameObject.CompareTag("Goal"))
		{
			GetComponent<AudioSource>().PlayOneShot(_winClip);
			Instantiate(_winUI);
			Time.timeScale = 0f;
		}
	}
	
}
