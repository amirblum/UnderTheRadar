using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginLevel : MonoBehaviour {
	
	void Start ()
	{
		Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Time.timeScale = 1f;
			gameObject.SetActive(false);
		}
	}
}
