using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Warehouse))]
public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private List<Bot> _bots;

    private float _delay;
    private List<Vector3> _resources;
    private Warehouse _warehouse;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _warehouse = GetComponent<Warehouse>();

        _delay = 0.1f;
        _resources = new List<Vector3>();
        _wait = new WaitForSeconds(_delay);
    }

    private void Update()
    {
        if (_bots.Count > 0)
        {
            StartCoroutine(AssignBotToResource());
        }
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

    public void AddResourece(Resource resource)
    {
        _warehouse.AddRecource(resource);
    }

    private void UseBot(Bot bot)
    {
        _bots.Add(bot);
    }

    private void AssignBotsToResource(Vector3 resourcePosition)
    {
        _resources.Add(resourcePosition);
    }

    private IEnumerator AssignBotToResource()
    {
        while (_bots.Count > 0 && _resources.Count > 0)
        {
            _bots[0].SetTargetPosition(_resources[0]);
            _bots.RemoveAt(0);
            _resources.RemoveAt(0);
            
            yield return _wait;
        }
    }
}