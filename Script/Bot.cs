using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Hand _hand;

    private NavMeshAgent _agent;
    private Vector3 _startPosition;
    private Vector3 _resourcePosition;
    private bool _isTask;

    public event Action<Resource, Transform> HasCome;
    public event Action<Bot> CameBack;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        TakeResource(collision);

        PutResource(collision);
    }

    public void SetTargetPosition(Vector3 position)
    {
        if (_isTask == false)
        {
            _agent.SetDestination(position);
            _resourcePosition = position;

            SetTask();
        }
    }

    private void TakeResource(Collider collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            if (resource.transform.position == _resourcePosition)
            {
                SetNewTarget(resource, _hand.transform);

                SetTargetPosition(_startPosition);
            }
        }
    }

    private void PutResource(Collider collision)
    {
        if (collision.TryGetComponent(out Base botBase))
        {
            Resource resource = gameObject.GetComponentInChildren<Resource>();

            if (resource != null)
            {
                SetNewTarget(resource, botBase.transform);

                CameBack?.Invoke(this);

                botBase.AddResourece(resource);
                resource.TurnOffCollider();
            }
        }
    }

    private void SetNewTarget(Resource resource, Transform transform)
    {
        HasCome?.Invoke(resource, transform);
        RemoveTask();
    }

    private void SetTask()
    {
        _isTask = true;
    }

    private void RemoveTask()
    {
        _isTask = false;
    }
}