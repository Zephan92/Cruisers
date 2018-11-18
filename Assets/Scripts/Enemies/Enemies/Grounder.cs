using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Grounder : EnemyEntity
{
	private Transform _target;
	private NavMeshAgent _agent;
	private bool _nearTarget;

	private void Start()
	{
		_target = FindObjectOfType<PlayerController>().transform;
		_agent = GetComponent<NavMeshAgent>();
	}

	private void FixedUpdate()
	{
		if (_agent.isOnNavMesh)
		{
			_agent.destination = _target.position;
		}

		_nearTarget = Vector3.Distance(transform.position, _agent.destination) <= 3;
		if (_nearTarget)
		{
			//Attack Target or something
		}
	}
}
