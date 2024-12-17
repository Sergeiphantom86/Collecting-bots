using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Scanner : MonoBehaviour
{
    [SerializeField] private Base _botBase;
    [SerializeField] private LoopType _loopType;
    [SerializeField] private LayerMask _objecktSelectionMask;

    private List<Resource> _resources;
    private int _maxRadiusSize;
    private int _duration;
    private int _quantityOfRepetitions;
    private float _delay;
    private WaitForSeconds _wait;

    public event Action HasAppeared;

    private void Awake()
    {
        _duration = 1;
        _maxRadiusSize = 40;
        _quantityOfRepetitions = 5;
        _resources = new List<Resource>();

        _delay = _quantityOfRepetitions * _duration;

        _wait = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        SearchForNewResources();
    }

    private void OnEnable()
    {
        _botBase.ResourcesHaveOut += SearchForNewResources;
    }

    private void OnDisable()
    {
        _botBase.ResourcesHaveOut -= SearchForNewResources;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            if (resource.NewResource)
            {
                _resources.Add(resource);
            }
        }
    }

    public List<Resource> GetColliders()
    {
        return _resources;
    }

    public void SearchForNewResources()
    {
        StartCoroutine(WaitBeforeIncreasingRadius());
    }

    private IEnumerator WaitBeforeIncreasingRadius()
    {
        transform.DOScaleX(_maxRadiusSize, _duration).SetLoops(_quantityOfRepetitions, _loopType);

        yield return _wait;

        transform.localScale = new Vector3();

        HasAppeared?.Invoke();
    }
}
