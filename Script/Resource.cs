using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(LayerMask))]
public class Resource : MonoBehaviour
{
    private LayerMask _layerMask;
    private Collider _collider;
    private Rigidbody _rigidbody;

    public bool NewResource {  get; private set; }

    private void Awake()
    {
        _layerMask = GetComponent<LayerMask>(); 
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        NewResource = true;
    }

    public LayerMask GetMask()
    {
         return _layerMask;
    }

    public void SetMask(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    public void TurnOffCollider()
    {
        _collider.enabled = false;
    }
}