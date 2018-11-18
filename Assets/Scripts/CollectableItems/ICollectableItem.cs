using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public interface ICollectableItem
{
	ItemType Type { get; set; }
	UnityAction OnCollected { get; set; }
}
