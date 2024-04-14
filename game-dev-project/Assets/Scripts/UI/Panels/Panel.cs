using System;
using UnityEngine;

public class Panel : MonoBehaviour
{
    protected virtual void Start() { }
    protected virtual void OnDestroy() { }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
