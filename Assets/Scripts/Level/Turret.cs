using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
	public Grounder Target;
	private Transform _barrel;
	private int enemiesWithinRange;

	private void Awake()
	{
		_barrel = transform.Find("Barrel");
		enemiesWithinRange = 0;
	}

	private void FixedUpdate()
	{
		if (Target != null)
		{
			var targetRotation = Quaternion.LookRotation(Target.transform.position - _barrel.position);
			var str = Mathf.Min(5 * Time.deltaTime, 1);
			_barrel.rotation = Quaternion.Lerp(_barrel.rotation, targetRotation, str);

			Target.TakeDamage(2);
		}
		else
		{
			var targetRotation = Quaternion.LookRotation(transform.position + new Vector3(0,0,3));
			var str = Mathf.Min(5 * Time.deltaTime, 1);
			_barrel.rotation = Quaternion.Lerp(_barrel.rotation, targetRotation, str);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		var enemy = other.GetComponent<Grounder>();
		if (enemy != null)
		{
			enemiesWithinRange++;
			if (Target == null)
			{
				Target = enemy;
			}
		}
		
	}

	private void OnTriggerStay(Collider other)
	{
		if (Target == null)
		{
			var enemy = other.GetComponent<Grounder>();
			if (enemy != null)
			{
				Target = enemy;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var enemy = other.GetComponent<Grounder>();
		if (enemy != null)
		{
			enemiesWithinRange--;
			if (Target == enemy)
			{
				Target = null;
			}
		}
	}
}
