using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public event Action<Vector3> HasAppeared;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            HasAppeared?.Invoke(resource.transform.position);
        }
    }
}