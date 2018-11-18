using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
	public UnityAction<ItemType, int> OnItemAdded;
	public UnityAction<ItemType> OnItemRemoved;

	private Dictionary<ItemType, int> _items;

	private void Awake()
	{
		_items = new Dictionary<ItemType, int>();
	}

	public void Add(ItemType item, int count = 1)
	{
		if (_items.ContainsKey(item))
		{
			_items[item] += count;
		}
		else
		{
			_items.Add(item, count);
		}

		if (_items[item] > 0)
		{
			OnItemAdded?.Invoke(item, count);
		}
		else
		{
			Remove(item);
		}
	}

	public void Remove(ItemType item)
	{
		_items.Remove(item);
		OnItemRemoved?.Invoke(item);
	}
}