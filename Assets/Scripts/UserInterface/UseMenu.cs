using UnityEngine;
using System.Collections;

public class UseMenu : MenuBase
{
	private Player _player;
	private ResourceEntity _currentEntity;

	private void Awake()
	{
		_player = FindObjectOfType<Player>();
	}

	private void FixedUpdate()
	{
		if (_currentEntity != null)
		{
			_currentEntity.Drain();
		}
	}

	public override void Option1(){}

	public override void Option2()
	{
		_currentEntity = _player.CurrentSelectedEntity.GetComponent<ResourceEntity>();
	}

	public override void Option3(){}

	private void OnDisable()
	{
		_currentEntity = null;
	}
}
