using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public interface ICollectableItem
{
	UnityAction OnCollected { get; set; }
}
