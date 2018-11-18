using UnityEngine;
using System.Collections;

public class CollectableItemEntityFactory : LevelEntityFactory<ItemType>
{
	public override SpawnableEntity Create(ItemType type, Vector3 destination, Transform parent)
	{
		GameObject go = new GameObject(type.ToString());
		go.transform.SetParent(parent);
		go.transform.position = destination;
		SpawnableEntity retval = go.AddComponent<SpawnableEntity>();
		ICollectableItem item = go.AddComponent<CollectableItem>();
		item.Type = type;
		return retval;
	}
}
