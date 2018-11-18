using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelController : MonoBehaviour
{
	private CollectableItemSpawner[] _itemSpawners;
	private EnemySpawner[] _enemySpawners;

	private void Awake()
	{

	}

	private void Start()
	{
		_itemSpawners = FindObjectsOfType<CollectableItemSpawner>();
		_enemySpawners = FindObjectsOfType<EnemySpawner>();
	}

	public void PlaceStructure(ItemType type, Vector3 destination)
	{
		if (type == ItemType.Turret)
		{
			GameObject go = Instantiate(Resources.Load($"{type}Model")) as GameObject;
			go.transform.position = destination;
		}
	}


	public void StartSpawners()
	{
		foreach (EnemySpawner spawner in _enemySpawners)
		{
			spawner.StartSpawning();
		}
	}

	public void StopSpawners()
	{
		foreach (EnemySpawner spawner in _enemySpawners)
		{
			spawner.StopSpawning();
		}
	}
}
