using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public abstract class Singleton<T> : SingletonBase where T : SingletonBase {

	public static event Action<T> onInstanceChanged;

	[NotNull]
	public static T instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<T>(true);
			}
			if (_instance == null && Application.isPlaying) {
				GameObject newGO = new(typeof(T).Name);
				_instance = newGO.AddComponent<T>();
				Debug.Log("#Singleton# New instance: " + typeof(T).Name, _instance);
				_instance.InitNewInstance();
			}
			//Debug.Log("Singleton instance reference: " + typeof(T).Name);

			return _instance;
		}

	}
	static T _instance {
		get => __instance;
		set {
			bool emitEvent = value != __instance;
			__instance = value;
			if (emitEvent && value != null) {
				onInstanceChanged?.Invoke(__instance);
			}
		}
	}
	static T __instance;

	public static T instanceNullable => instanceExists ? instance : null;

	public static bool instanceExists {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<T>();
			}

			return _instance != null;
		}
	}

	protected virtual void Start() {
		if (instanceExists && instance != this) {
			Destroy(gameObject);
		}
		else {
			_instance = this as T;
		}
	}

	void OnDestroy() {
		if (_instance == this) {
			_instance = null;
		}
	}

	public sealed override void LazyLoadInstance() {}

}