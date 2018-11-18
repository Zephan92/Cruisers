using UnityEngine;
using System.Collections;

public class ResourceSpawner : SpawnerHotspotBase<ResourceType>
{
	public override SpawnableEntity Spawn(Vector3 spawnLocation)
	{
		if (SpawnableEntities != null && SpawnableEntities.Length > 0)
		{
			int index = Random.Range(0, SpawnableEntities.Length);
			ResourceType resourceType = SpawnableEntities[index];
			return SpawnCollectableItem(resourceType, spawnLocation);
		}

		return null;
	}

	private SpawnableEntity SpawnCollectableItem(ResourceType type, Vector3 pos)
	{
		GameObject go = Instantiate(Resources.Load($"{type}Resource"), transform) as GameObject;
		var entity = go.GetComponent<ResourceEntity>();
		return entity;
	}
}
