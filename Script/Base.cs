using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Warehouse))]
public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;

    private int _daley;
    private List<Bot> _bots;
    private List<Resource> _resources;
    private Warehouse _warehouse;
    private WaitForSeconds _wait;

    public event Action ResourcesHaveOut;

    private void Awake()
    {
        _warehouse = GetComponent<Warehouse>();

        _daley = 1;
        _bots = new List<Bot>();
        _resources = new List<Resource>();
        _wait = new WaitForSeconds(_daley);
    }

    private void Update()
    {
        if (_resources.Count > 0 && _bots.Count > 0)
        {
            StartCoroutine(Wait());
        }
    }

    private void OnEnable()
    {
        _scanner.HasAppeared += AssignBotsToResource;
    }

    private void OnDisable()
    {
        _scanner.HasAppeared -= AssignBotsToResource;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Bot bot))
        {
            if (bot.Busy == false)
            {
                _bots.Add(bot);

                ResourcesHaveOut?.Invoke();
            }
        }
    }

    private void AssignBotsToResource()
    {
        StartCoroutine(Wait());
    }

    public void AddResourece(Resource resource)
    {
        _warehouse.AddRecource(resource);
    }

    private IEnumerator Wait()
    {
        _resources = _scanner.GetColliders();

        yield return _wait;

        while (_bots.Count > 0 && _resources.Count > 0)
        {
            _bots[0].SetResourcePosition(_resources[0].transform.position);
            _bots[0].SetBusyness(true);
            _bots.RemoveAt(0);
            _resources.RemoveAt(0);
        }
    }
}