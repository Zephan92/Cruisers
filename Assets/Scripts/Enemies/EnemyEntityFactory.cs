using UnityEngine;
using System.Collections;

public class EnemyEntityFactory : LevelEntityFactory<EnemyType>
{
	public override SpawnableEntity Create(EnemyType type, Vector3 destination, Transform parent)
	{
		GameObject go = Instantiate(Resources.Load($"{type}"), parent) as GameObject;
		go.transform.position = destination;
		SpawnableEntity retval = go.AddComponent<SpawnableEntity>();
		return retval;
	}
}
