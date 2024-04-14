using System;
using UnityEngine;
using UnityEngine.Events;

public class OrderClassMembers : MonoBehaviour{
    
    //1. variables
    
    // public, invisible in Inspector
    public event Action onEvent; 
    public static readonly string VisibleConstantA;

    //visible in inspector
    [SerializeField] public string serializedPublic;
    [SerializeField] public string serializedPublicProperty { get; set; }
    [SerializeField] private string serializedPrivate;
    [SerializeField] protected string serializedProtected;
    
    //invisible in inspector
    public int someInt => _privateInt;
    protected int protectedInt;
    private int _privateInt;
    private const int constPrivateInt = 1;
    
    // related with Unity
    UnityEvent myEvent; 
    
    //2. methods
    
    // a ) related with Unity

    private void Reset() { }
    private void Awake() { }
    private void Start() { }
    private void OnEnable() { }
    private void Update() { }
    
    // ...

    // b ) public methods
    // virtual, override
    // normal methods
    public void MethodName(){}
    // scene/ prefab methods
    // related with interfaces 
    
    // c ) protected methods
    // virtual, override
    // normal methods
    protected void ProtectedMethod() { }
    
    // d ) private methods
    private void MyMethod() {}

    public enum TestEnum
    {
        one,
        two,
        three
    }

    public class TestClass
    {
        
    }
}