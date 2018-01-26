using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class Radarable : MonoBehaviour
{
	private MeshRenderer _mesh;

	private float _alpha;

	public event Action OnRadarHitEvent;
	public event Action OnRadarEndEvent;

	private void Start()
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
		
		GetComponent<AudioSource>().Play();

		var currentTime = 0f;
		while (currentTime <= duration)
		{
			currentTime += Time.deltaTime;
			var currentAlpha = Mathf.SmoothStep(1f, 0f, currentTime / duration);

			Debug.Log(name + " setting alpha to " + currentAlpha);
			
			var color = _mesh.material.GetColor("_Color");
			color.a = currentAlpha;
			_mesh.material.SetColor("_Color", color);
			
			yield return null;
		}
		
		_mesh.enabled = false;
		
		if (OnRadarEndEvent != null)
		{
			OnRadarEndEvent();
		}
	}
}
