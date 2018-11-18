using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
	private LinkedListNode<ItemType> _current;

	private Text _previousText;
	private Text _currentText;
	private Text _nextText;

	private LevelController _levelController;
	private Player _player;
	private Inventory _inventory;
	private LinkedList<ItemType> _menuItems;

	private void Awake()
	{
		_levelController = FindObjectOfType<LevelController>();
		_player = FindObjectOfType<Player>();
		_menuItems = new LinkedList<ItemType>();
		_previousText = transform.Find("Previous").GetComponentInChildren<Text>();
		_currentText = transform.Find("Current").GetComponentInChildren<Text>();
		_nextText = transform.Find("Next").GetComponentInChildren<Text>();
	}

	private void Start()
	{
		_inventory = FindObjectOfType<Player>().Inventory;
		_inventory.OnItemAdded += (type, count) => { AddItem(type); };
		_inventory.OnItemRemoved += (type) => { RemoveItem(type); };
		UpdateBuildMenu();
	}

	public void Previous()
	{
		if (_current.Previous != null)
		{
			_current = _current.Previous;
		}
		else
		{
			_current = _menuItems.Last;
		}

		UpdateBuildMenu();
	}

	public void Place()
	{
		if (_current != null)
		{
			_levelController.PlaceStructure(_current.Value, _player.BuildDestination);
			_inventory.Add(_current.Value, -1);
		}
	}

	public void Next()
	{
		if (_current.Next != null)
		{
			_current = _current.Next;
		}
		else
		{
			_current = _menuItems.First;
		}

		UpdateBuildMenu();
	}

	private void AddItem(ItemType type)
	{
		if (!_menuItems.Contains(type))
		{
			_menuItems.AddLast(type);
		}

		if (_current == null)
		{
			_current = _menuItems.First;
		}

		UpdateBuildMenu();
	}

	private void RemoveItem(ItemType type)
	{
		_menuItems.Remove(type);
		if (_current.Value == type)
		{
			_current = _menuItems.First;
		}
		UpdateBuildMenu();
	}

	private void UpdateBuildMenu()
	{
		if (_menuItems.Count > 0)
		{
			if (_current.Previous != null)
			{
				_previousText.text = _current.Previous.Value.ToString();
			}
			else
			{
				_previousText.text = _menuItems.Last.Value.ToString();
			}

			_currentText.text = _current.Value.ToString();

			if (_current.Next != null)
			{
				_nextText.text = _current.Next.Value.ToString();
			}
			else
			{
				_nextText.text = _menuItems.First.Value.ToString();
			}
		}
		else
		{
			_previousText.text = "";
			_currentText.text = "";
			_nextText.text = "";
		}
	}
}
