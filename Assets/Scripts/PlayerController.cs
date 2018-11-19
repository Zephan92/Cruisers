using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {
	public float RotationSpeed = 2.5f;

	private Player _player;
	private Camera _playerCamera;

	//BuildTarget
	private Vector3 _currentSnap;
	private GameObject _currentBuildTarget;
	private PlayerMenu _playerMenu;

	private void Awake()
	{
		_player = GetComponent<Player>();
		_playerCamera = Camera.main;
		_playerMenu = FindObjectOfType<PlayerMenu>();
	}

	private void Start()
	{
		
	}

	private void FixedUpdate()
	{
		Rotate();
		Move();
		BuildInput();
		ShowBuildTarget();
	}

	private void Rotate()
	{
		var x = Input.GetAxis("Horizontal") * RotationSpeed;
		_playerCamera.transform.parent.Rotate(transform.up, x);
		_player.Rotate(x);
	}

	private void Move()
	{
		var z = Input.GetAxis("Vertical");
		Vector3 newPos = new Vector3(0, 0, z);
		newPos = _playerCamera.transform.parent.TransformDirection(newPos);
		_player.Move(newPos);
	}

	private void BuildInput()
	{
		if (Input.GetButtonDown("Previous"))
		{
			_playerMenu.Option1();
		}

		if (Input.GetButtonDown("Place"))
		{
			_playerMenu.Option2();
		}

		if (Input.GetButtonDown("Next"))
		{
			_playerMenu.Option3();
		}
	}

	private void ShowBuildTarget()
	{
		var buildTargetPosition = _player.BuildTargetPosition;

		if (_currentSnap != buildTargetPosition)
		{
			if (_currentBuildTarget != null)
			{
				Destroy(_currentBuildTarget);
			}

			_currentBuildTarget = DrawingHelper.DrawCube(buildTargetPosition, new Vector3(2, 4, 2), Color.red, _player.transform);
			_currentSnap = buildTargetPosition;
		}
	}
}
