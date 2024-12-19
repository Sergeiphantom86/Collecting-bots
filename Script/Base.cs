using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Warehouse))]
public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private List<Bot> _bots;

    private float _delay;
    private List<Vector3> _resources;
    private Warehouse _warehouse;
    private WaitForSeconds _wait;

    public event Action ResourcesHaveOut;

    private void Awake()
    {
        _warehouse = GetComponent<Warehouse>();

        _delay = 0.5f;
        _resources = new List<Vector3>();
        _wait = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _scanner.HasAppeared += AssignBotsToResource;

        foreach (Bot bot in _bots)
        {
            bot.CameBack += UseBot;
        }
    }

    private void OnDisable()
    {
        _scanner.HasAppeared -= AssignBotsToResource;

        foreach (Bot bot in _bots)
        {
            bot.CameBack -= UseBot;
        }
    }

    private void UseBot(Bot bot)
    {
        _bots.Add(bot);

        ResourcesHaveOut?.Invoke();
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

        while (_bots.Count > 0)
        {
            if (_resources.Count <= 0)
            {
                ResourcesHaveOut?.Invoke();
                yield break;
            }
            else
            {
                _bots[0].SetTargetPosition(_resources[0]);
                _bots.RemoveAt(0);
                _resources.RemoveAt(0);
                yield return _wait;
            }
        }
    }
}