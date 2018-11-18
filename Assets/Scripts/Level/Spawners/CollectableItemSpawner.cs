using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CollectableItemEntityFactory))]
public class CollectableItemSpawner : SpawnerHotspotBase<ItemType>
{
	private CollectableItemEntityFactory _factory;

	private new void Awake()
	{
		base.Awake();
		_factory = gameObject.GetComponent<CollectableItemEntityFactory>();
		if (_factory == null)
		{
			_factory = gameObject.AddComponent<CollectableItemEntityFactory>();
		}
	}

	public override SpawnableEntity Spawn()
	{
		if (SpawnableEntities != null && SpawnableEntities.Length > 0)
		{
			int index = Random.Range(0, SpawnableEntities.Length);
			ItemType itemType = SpawnableEntities[index];
			return SpawnCollectableItem(itemType, _spawnLocation);
		}

		return null;
	}

	private SpawnableEntity SpawnCollectableItem(ItemType itemType, Vector3 pos)
	{
		SpawnableEntity go = _factory.Create(itemType, pos, transform);
		ICollectableItem item = go.GetComponent<CollectableItem>();
		if (item != null)
		{
			item.OnCollected += () => { };
		}
		return go;
	}
}
