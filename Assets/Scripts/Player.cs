using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Inventory))]
public class Player : MonoBehaviour
{
	private SpawnableEntity _currentSelectedEntity;
	public SpawnableEntity CurrentSelectedEntity
	{
		get { return _currentSelectedEntity; }
		set
		{
			_currentSelectedEntity = value;
			if (value != null)
			{
				OnEntitySelected?.Invoke(value);
			}
			else
			{
				OnEntityDeselected?.Invoke();
			}
		}
	}
	public UnityAction<SpawnableEntity> OnEntitySelected;
	public UnityAction OnEntityDeselected;

	public Vector3 BuildTargetPosition => _box.transform.position;
	public Vector3 BuildDestination => _box.transform.position - new Vector3(0,2,0);
	public float Gravity = -9.81f;
	public Vector3 Drag;
	public float GroundDistance = 0.2f;
	public float MoveSpeed = 5f;
	public LayerMask GroundLayer;
	public Inventory Inventory;

	private CharacterController _controller;
	private Transform _model;
	private Transform _buildTarget;
	private Transform _box;

	private Vector3 _velocity;
	private bool _isGrounded = true;

	private void Awake()
	{
		Inventory = GetComponent<Inventory>();

		_controller = GetComponent<CharacterController>();
		_model = transform.Find("Model");
		_buildTarget = transform.Find("BuildTarget");
		_box = _buildTarget.Find("Box");
	}

	private void FixedUpdate()
	{
		ApplyPhysics();
	}

	public void Move(Vector3 newPos) {
		_controller.Move(newPos * Time.deltaTime * MoveSpeed);
		_box.position = FindClosestSnap(transform.position, _model.forward, 4);
	}

	public void Rotate(float rotation)
	{
		_model.Rotate(transform.up, rotation);
	}

	private void ApplyPhysics()
	{
		_isGrounded = Physics.CheckSphere(transform.position, GroundDistance, GroundLayer, QueryTriggerInteraction.Ignore);
		if (_isGrounded && _velocity.y < 0)
		{
			_velocity.y = 0f;
		}

		_velocity.y += Gravity * Time.deltaTime;

		_velocity.x /= 1 + Drag.x * Time.deltaTime;
		_velocity.y /= 1 + Drag.y * Time.deltaTime;
		_velocity.z /= 1 + Drag.z * Time.deltaTime;

		_controller.Move(_velocity * Time.deltaTime);
	}

	private Vector3 FindClosestSnap(Vector3 pos, Vector3 dir, int radius)
	{
		return RoundTransform(pos + dir * radius, 2);
	}

	private Vector3 RoundTransform(Vector3 v, float snapValue)
	{
		return new Vector3
		(
			snapValue * Mathf.Round(v.x / snapValue),
			snapValue * Mathf.Round(v.y / snapValue),
			snapValue * Mathf.Round(v.z / snapValue)
		);
	}
}
