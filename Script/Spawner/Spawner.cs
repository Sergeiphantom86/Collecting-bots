using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _objectsPrefab;

    private Pool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new Pool<T>(
            () => Instantiate(_objectsPrefab));
    }

    public virtual T Spawn(Vector3 position)
    {
        T item = _pool.GetObject();
        item.transform.position = position;
        return item;
    }
}