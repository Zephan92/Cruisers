using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public enum EntityType
{
	Resource,
	Enemy,
	Item,
}

public abstract class SpawnableEntity : MonoBehaviour
{
	public UnityAction OnDestroyed;
	private bool _quitting;
	public abstract EntityType EntityType { get; }

	private void OnDestroy()
	{
		if (!_quitting)
		{
			OnDestroyed?.Invoke();
		}
	}

	private void OnApplicationQuit()
	{
		_quitting = true;
	}
}
