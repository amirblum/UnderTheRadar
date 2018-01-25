using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
	[SerializeField] float _duration;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider triggered)
	{
		var radarable = triggered.GetComponent<Radarable>();

		if (radarable == null)
		{
			return;
		}

		radarable.OnRadarHit(_duration);
	}
	
}
