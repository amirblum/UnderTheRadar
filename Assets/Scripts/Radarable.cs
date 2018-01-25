using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Radarable : MonoBehaviour
{
	private MeshRenderer _mesh;

	public event Action OnRadarHitEvent;
	public event Action OnRadarEndEvent;

	private void Awake()
	{
		_mesh = GetComponent<MeshRenderer>();
		_mesh.enabled = false;
	}

	public void OnRadarHit(float duration)
	{
		StopAllCoroutines();
		StartCoroutine(RadarCoroutine(duration));
	}

	private IEnumerator RadarCoroutine(float duration)
	{
		if (OnRadarHitEvent != null)
		{
			OnRadarHitEvent();
		}
		_mesh.enabled = true;
		
		yield return new WaitForSeconds(duration);
		
		_mesh.enabled = false;
		
		if (OnRadarEndEvent != null)
		{
			OnRadarEndEvent();
		}
	}
}
