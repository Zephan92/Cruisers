using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour, ICollectableItem
{
	public ItemType Type { get; set; }
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

		GameObject modelGameObject = Instantiate(Resources.Load($"{Type}ItemModel"), transform) as GameObject;
		_model = modelGameObject.transform;
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
