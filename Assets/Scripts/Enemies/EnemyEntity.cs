using UnityEngine;
using System.Collections;

public class EnemyEntity : SpawnableEntity
{
	public int Health = 100;
	public override EntityType EntityType => EntityType.Enemy;


	private void Awake()
	{
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
