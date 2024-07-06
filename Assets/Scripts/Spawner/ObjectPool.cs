using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IDisableable<T>
{
    [SerializeField] private Transform _container;

    private Queue<T> _pool = new Queue<T>();
    private T _prefab;

    private void OnDisable()
    {
        foreach (T gameObject in _pool)
            gameObject.Disabled -= EnqueueGameObject;
    }

    protected void Initialize(T prefab)
    {
        _prefab = prefab;
    }

    protected void TryGetObject(out T unactiveObject)
    {
        if (_pool.Count == 0)
        {
            T gameObject = Instantiate(_prefab, _container);
            _pool.Enqueue(gameObject);
            gameObject.Disabled += EnqueueGameObject;
        }
        unactiveObject = _pool.Dequeue();
    }

    private void EnqueueGameObject(T t)
    {
        _pool.Enqueue(t);
    }
}
