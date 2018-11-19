using UnityEngine;
using System.Collections;

public class ResourceEntity : SpawnableEntity
{
	public int Durability = 100;
	public override EntityType EntityType => EntityType.Resource;
	private Renderer[] _renderers;

	private void Awake()
	{
		_renderers = GetComponentsInChildren<Renderer>();
		OnDestroyed += DropLoot;
	}

	public void Drain(int drain = 1)
	{
		Durability -= drain;

		if (Durability <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("BuildTarget"))
		{
			other.transform.parent.parent.GetComponent<Player>().CurrentSelectedEntity = this;
			foreach (Renderer rend in _renderers)
			{
				rend.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals("BuildTarget"))
		{
			other.transform.parent.parent.GetComponent<Player>().CurrentSelectedEntity = null;
			foreach (Renderer rend in _renderers)
			{
				rend.material.shader = Shader.Find("Diffuse");
			}
		}
	}

	private void DropLoot()
	{
		GameObject loot = new GameObject();
		loot.transform.position = transform.position;
		loot.AddComponent<Loot>().LootTypes = new LootType[] { LootType.Common, LootType.Uncommon , LootType.Rare , LootType.Mythic , LootType.Legendary };
	}
}
