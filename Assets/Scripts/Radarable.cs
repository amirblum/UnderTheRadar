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
		
		ShowMesh();
		
		GetComponent<AudioSource>().Play();

		var currentTime = 0f;
		while (currentTime <= duration)
		{
			currentTime += Time.deltaTime;
			var currentAlpha = Mathf.SmoothStep(1f, 0f, currentTime / duration);
			
			var color = _mesh.material.GetColor("_Color");
			color.a = currentAlpha;
			_mesh.material.SetColor("_Color", color);
			
			yield return null;
		}
		
		HideMesh();
		
		if (OnRadarEndEvent != null)
		{
			OnRadarEndEvent();
		}
	}

	public void ShowMesh()
	{
		_mesh.enabled = true;
	}

	public void HideMesh()
	{
		_mesh.enabled = false;
	}
}
