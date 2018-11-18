using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ItemEntity : SpawnableEntity, ICollectableItem
{
	public ItemType Type;
	public UnityAction OnCollected { get; set; }

	public float PickupZoneRadius = 0.75f;
	public float RotationSpeed = 3f;

	private Collider _pickupZone;
	private Transform _model;

	private void Awake()
	{
		SphereCollider collider = gameObject.AddComponent<SphereCollider>();
		collider.radius = PickupZoneRadius;
		collider.isTrigger = true;
		_model = transform.Find("Model");
	}

	private void FixedUpdate()
	{
		_model.transform.Rotate(Vector3.up, RotationSpeed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			other.GetComponent<Player>()?.Inventory?.Add(Type);
			OnCollected?.Invoke();
			Destroy(gameObject);
		}
	}
}