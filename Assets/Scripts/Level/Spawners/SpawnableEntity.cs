using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpawnableEntity : MonoBehaviour
{
	public UnityAction OnDestroyed;
	private bool _quitting;

	private void OnDestroy()
	{
		if (!_quitting)
		{
			OnDestroyed?.Invoke();
		}
	}

	void OnApplicationQuit()
	{
		_quitting = true;
	}
}
