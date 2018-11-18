using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyEntityFactory))]
public class EnemySpawner : SpawnerHotspotBase<EnemyType>
{
	public int MaxEnemyCount = 15;
	[ReadOnly] public int CurrentEnemyCount;

	private EnemyEntityFactory _factory;

	private new void Awake()
	{
		base.Awake();
		_factory = gameObject.GetComponent<EnemyEntityFactory>();
		if (_factory == null)
		{
			_factory = gameObject.AddComponent<EnemyEntityFactory>();
		}
		CurrentEnemyCount = 0;
	}

	public override SpawnableEntity Spawn()
	{
		if (SpawnableEntities != null && SpawnableEntities.Length > 0)
		{
			int index = Random.Range(0, SpawnableEntities.Length);
			EnemyType enemyType = SpawnableEntities[index];
			return SpawnEnemy(enemyType, _spawnLocation);
		}

		return null;
	}

	private SpawnableEntity SpawnEnemy(EnemyType enemyType, Vector3 pos)
	{
		if (CurrentEnemyCount >= MaxEnemyCount)
		{
			return null;
		}

		SpawnableEntity enemy =  _factory.Create(enemyType, pos, transform);
		if (enemy != null)
		{
			enemy.OnDestroyed += () => { CurrentEnemyCount--; };
		}

		CurrentEnemyCount++;
		return enemy;
	}
}
