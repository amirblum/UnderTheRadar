using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Radarable : MonoBehaviour
{
	private MeshRenderer _mesh;

	private void Awake()
	{
		_mesh = GetComponent<MeshRenderer>();
		_mesh.enabled = false;
	}

	private void Start()
	{
		GameController.Instance.OnRadarEvent += ToggleVisibility;
	}

	private void ToggleVisibility()
	{
		StopAllCoroutines();
		StartCoroutine(ToggleVisibilityCoroutine());
	}

	private IEnumerator ToggleVisibilityCoroutine()
	{
		_mesh.enabled = true;
		yield return new WaitForSeconds(GameController.Instance.radarDuration);
		_mesh.enabled = false;
	}
}
