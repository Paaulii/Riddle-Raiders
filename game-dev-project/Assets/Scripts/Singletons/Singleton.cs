using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public abstract class Singleton<T> : SingletonBase where T : SingletonBase {
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

			return _instance;
		}

	}
	static T _instance {
		get => __instance;
		set => __instance = value;
	}
	static T __instance;

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