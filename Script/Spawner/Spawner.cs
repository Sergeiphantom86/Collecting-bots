using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private List<T> _objectsPrefab;
    [SerializeField] private int PoolCapacity;
    [SerializeField] private int _poolMaxSize;

    private Pool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new Pool<T>(
            () => Instantiate(_objectsPrefab[GetIndexOfObject()]));
    }

    public virtual T Spawn(Vector3 position)
    {
        T item = _pool.GetObject();
        item.transform.position = position;
        return item;
    }

    private int GetIndexOfObject()
    {
        if (_objectsPrefab.Count > 1)
        {
            return Random.Range(0, _objectsPrefab.Count);
        }

        return 0;
    }
}