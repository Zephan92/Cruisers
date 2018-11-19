using UnityEngine;
using System.Collections;

public enum LootType
{
	Common,
	Uncommon,
	Rare,
	Mythic,
	Legendary
}

public class Loot : MonoBehaviour
{
	public int DropCount = 5;
	public LootType[] LootTypes = new LootType[0];

	private void Start()
	{
		if (LootTypes != null && LootTypes.Length > 0)
		{
			for (int i = 0; i < DropCount; i++)
			{
				StartCoroutine(DropLootCoroutine(Random.Range(0, 0.25f)));
			}
		}
	}

	private IEnumerator DropLootCoroutine(float wait)
	{
		yield return new WaitForSeconds(wait);
		int index = Random.Range(0, LootTypes.Length);
		LootType lootType = LootTypes[index];
		GameObject go = Instantiate(Resources.Load($"Loot/{lootType}"), transform) as GameObject;
		go.transform.position += new Vector3(Random.Range(0f, 0.5f), 0.5f, Random.Range(0f, 0.5f));
	}
}
