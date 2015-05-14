using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetSortingLayer : MonoBehaviour {
	public string sortingLayerName;
	public int sortingOrder = 0;
	Renderer _renderer;
	ParticleSystem _particleSystem;
	TrailRenderer _trailRenderer;
	LineRenderer _lineRenderer;
	
	void Awake () {
		_renderer = GetComponent<Renderer> ();
		_particleSystem = GetComponent<ParticleSystem> ();
		_trailRenderer = GetComponent<TrailRenderer> ();
		_lineRenderer = GetComponent<LineRenderer> ();
		
		if(_renderer!=null)
		{
			_renderer.sortingLayerName = sortingLayerName;
			_renderer.sortingOrder = sortingOrder;
		}
		
		/*if (_particleSystem != null)
		{
			_particleSystem.renderer.sortingLayerName = sortingLayerName;
			_particleSystem.renderer.sortingOrder = sortingOrder;
		}*/
		
		if (_trailRenderer != null)
		{
			_trailRenderer.sortingLayerName = sortingLayerName;
			_trailRenderer.sortingOrder = sortingOrder;
		}

		if (_lineRenderer != null) {
			_lineRenderer.sortingLayerName = sortingLayerName;
			_lineRenderer.sortingOrder = sortingOrder;
		}
	}
	
}
