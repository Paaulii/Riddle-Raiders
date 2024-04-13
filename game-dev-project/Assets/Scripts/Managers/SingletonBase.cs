using UnityEngine;

public class SingletonBase : MonoBehaviour {

	public virtual void InitNewInstance(){}

	/// <summary>
	/// In NullableSingleton, it will be called before first assignment.
	/// Can be sure this will be called before any .instance will be returned
	/// </summary>
	public virtual void LazyLoadInstance() { }


}