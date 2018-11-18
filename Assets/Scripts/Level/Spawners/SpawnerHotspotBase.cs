using UnityEngine;
using System.Collections;

public abstract class SpawnerHotspotBase<T> : HotspotBase
{
	public T[] SpawnableEntities;
	public bool SpawnOnStart = true;
	public bool RespawnOnEntityDestroy = true;
	public bool RespawnOnInterval = false;
	public float RespawnTimeoutSeconds = 5f;

	internal Vector3 _spawnLocation;
	private bool _shouldSpawn = false;

	public abstract SpawnableEntity Spawn();

	public void StartSpawning()
	{
		_shouldSpawn = true;
		SpawnHelper(true);
	}

	public void StopSpawning()
	{
		_shouldSpawn = false;
	}

	internal void Awake()
	{
		_spawnLocation = transform.position + GetComponent<BoxCollider>().center;
	}

	internal void Start()
	{
		if (SpawnOnStart)
		{
			_shouldSpawn = true;
			if (RespawnOnInterval)
			{
				StartSpawning();
			}
			else
			{
				SpawnHelper(false);
			}
		}
	}

	private void SpawnHelper(bool respawn)
	{
		if (!_shouldSpawn)
		{
			return;
		}

		SpawnableEntity entity = Spawn();

		if (RespawnOnEntityDestroy)
		{
			if (entity != null)
			{
				entity.transform.SetParent(transform);
				entity.OnDestroyed += () => { SpawnAfterTimeout(RespawnTimeoutSeconds, false); };
			}
		}

		if (respawn)
		{
			SpawnAfterTimeout(RespawnTimeoutSeconds, respawn);
		}
	}

	private void SpawnAfterTimeout(float timeout, bool respawn)
	{
		if (gameObject.activeSelf)
		{
			StartCoroutine(SpawnAfterTimeoutCoroutine(timeout, respawn));
		}
	}

	private IEnumerator SpawnAfterTimeoutCoroutine(float timeout, bool respawn)
	{
		yield return new WaitForSeconds(timeout);
		SpawnHelper(respawn);
	}
}
