using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum PlayerMenuType
{
	Build,
	Edit,
	Use,
}

public class PlayerMenu : MenuBase
{
	private MenuBase _currentMenu;
	private BuildMenu _buildMenu;
	private UseMenu _useMenu;

	private PlayerMenuType _currentType;

	private Player _player;

	private void Awake()
	{
		_player = FindObjectOfType<Player>();
		_player.OnEntitySelected += UpdateMenuType;
		_player.OnEntityDeselected += () => { ChangeMenu(PlayerMenuType.Build); };
		_buildMenu = transform.Find("BuildMenu").GetComponent<BuildMenu>();
		_useMenu = transform.Find("UseMenu").GetComponent<UseMenu>();
		_currentType = PlayerMenuType.Build;
		_currentMenu = _buildMenu;
	}

	public void ChangeMenu(PlayerMenuType type)
	{
		if (type == _currentType)
		{
			return;
		}

		_currentMenu.gameObject.SetActive(false);
		switch (type)
		{
			case PlayerMenuType.Build:
				_currentMenu = _buildMenu;
				break;
			case PlayerMenuType.Edit:
				break;
			case PlayerMenuType.Use:
				_currentMenu = _useMenu;
				break;
		}
		_currentType = type;
		_currentMenu.gameObject.SetActive(true);
	}

	private void UpdateMenuType(SpawnableEntity entity)
	{
		switch (entity.EntityType)
		{
			case EntityType.Resource:
				ChangeMenu(PlayerMenuType.Use);
				break;
		}
	}

	public override void Option1()
	{
		_currentMenu.Option1();
	}

	public override void Option2()
	{
		_currentMenu.Option2();
	}

	public override void Option3()
	{
		_currentMenu.Option3();
	}
}
