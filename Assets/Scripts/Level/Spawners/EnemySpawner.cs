using UnityEngine;
using System.Collections;

public class EnemySpawner : SpawnerHotspotBase<EnemyType>
{
	public int MaxEnemyCount = 15;
	[ReadOnly] public int CurrentEnemyCount;

	private new void Awake()
	{
		base.Awake();
		CurrentEnemyCount = 0;
	}

	public override SpawnableEntity Spawn(Vector3 spawnLocation)
	{
		if (SpawnableEntities != null && SpawnableEntities.Length > 0)
		{
			int index = Random.Range(0, SpawnableEntities.Length);
			EnemyType enemyType = SpawnableEntities[index];
			return SpawnEnemy(enemyType, spawnLocation);
		}

		return null;
	}

	private SpawnableEntity SpawnEnemy(EnemyType enemyType, Vector3 pos)
	{
		if (CurrentEnemyCount >= MaxEnemyCount)
		{
			return null;
		}

		GameObject enemy = Instantiate(Resources.Load($"Enemies/{enemyType}"), transform) as GameObject;

		var entity = enemy.GetComponent<EnemyEntity>();
		if (entity != null)
		{
			entity.OnDestroyed += () => { CurrentEnemyCount--; };
		}
		CurrentEnemyCount++;
		return entity;
	}
}
