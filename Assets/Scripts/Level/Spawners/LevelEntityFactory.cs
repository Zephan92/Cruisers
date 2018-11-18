using UnityEngine;
using System.Collections;

public abstract class LevelEntityFactory<T> : MonoBehaviour
{
	public abstract SpawnableEntity Create(T type, Vector3 destination, Transform parent);
}
