using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class Radarable : MonoBehaviour
{
	private MeshRenderer _mesh;
	private Material _material;

	private float _alpha;

	public event Action OnRadarHitEvent;
	public event Action OnRadarEndEvent;

	private void Awake()
	{
		_mesh = GetComponent<MeshRenderer>();
		_mesh.enabled = false;

		_material = _mesh.material;
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
		
		GetComponent<AudioSource>().Play();

		var currentAlpha = 1f;
		
		while (currentAlpha >= 0f)
		{
			currentAlpha -= Time.deltaTime * duration;

			var color = _material.color;
			color.a = currentAlpha;
			_material.color = color;
			
			yield return null;
		}
		
		_mesh.enabled = false;
		
		if (OnRadarEndEvent != null)
		{
			OnRadarEndEvent();
		}
	}
}
