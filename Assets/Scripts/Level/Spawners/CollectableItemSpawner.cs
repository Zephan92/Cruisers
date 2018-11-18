using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class CollectableItemSpawner : SpawnerHotspotBase<ItemType>
{
	public override SpawnableEntity Spawn(Vector3 spawnLocation)
	{
		if (SpawnableEntities != null && SpawnableEntities.Length > 0)
		{
			int index = Random.Range(0, SpawnableEntities.Length);
			ItemType itemType = SpawnableEntities[index];
			return SpawnCollectableItem(itemType, spawnLocation);
		}

		return null;
	}

	private SpawnableEntity SpawnCollectableItem(ItemType type, Vector3 pos)
	{
		GameObject go = Instantiate(Resources.Load($"{type}Item"), transform) as GameObject;
		var entity = go.AddComponent<ItemEntity>();
		if (entity != null)
		{
			entity.OnCollected += () => { };
		}
		entity.Type = type;
		return entity;
	}
}
