using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
	[SerializeField] float _maxSize;
	[SerializeField] float _growSpeed;
	[SerializeField] Vector3 _growDirection = new Vector3(1.0f, 1.0f, 1.0f);
	[SerializeField] float _hitDuration;
	[SerializeField] AnimationCurve _fadeOutCurve;

	private float _size;
	private Material _material;

	private void Awake()
	{
		transform.localScale = Vector3.zero;
		_material = GetComponent<Renderer>().material;
	}

	private void Update()
	{
		_size += _growSpeed * Time.deltaTime;

//		if (_size > _maxSize / 2)
//		{
//			var color = _material.color;
//			color.a = Mathf.Lerp(color.a, 0f, _size / _maxSize);
//			_material.color = color;
//		}
		
		var color = _material.color;
		color.a = Mathf.Lerp(0.3f, 0f, _size / _maxSize);
//		color.a = _fadeOutCurve.Evaluate(_size / _maxSize);
		_material.color = color;
		
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

	public float GetDuration()
	{
		return _maxSize / _growSpeed;
	}
}
