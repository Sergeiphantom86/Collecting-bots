using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _rigidbody;

    public bool NewResource {  get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        NewResource = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Scanner _))
        {
            NewResource = false;
        }
    }

    public void TurnOffCollider()
    {
        _collider.enabled = false;
    }

    public void Relocate(Vector3 vector)
    {
        transform.position = vector;
    }

    public void TurnOnIsKinematic()
    {
        _rigidbody.isKinematic = true;
    }

    public void TurnOffIsKinematic()
    {
        _rigidbody.isKinematic = false;
    }
}