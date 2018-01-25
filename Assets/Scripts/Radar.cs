using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
	[SerializeField] float _maxSize;
	[SerializeField] float _growSpeed;
	[SerializeField] Vector3 _growDirection = new Vector3(1.0f, 1.0f, 1.0f);
	[SerializeField] float _hitDuration;

	private float _size;

	private void Awake()
	{
		transform.localScale = Vector3.zero;
	}

	private void Update()
	{
		_size += _growSpeed * Time.deltaTime;

		if (_size > _maxSize)
		{
			Destroy(gameObject);
			return;
		}
		
		transform.localScale = _growDirection * _size;
	}

	private void OnTriggerEnter(Collider triggered)
	{
		var radarable = triggered.GetComponent<Radarable>();

		if (radarable == null)
		{
			return;
		}

		radarable.OnRadarHit(_hitDuration);
	}
}
