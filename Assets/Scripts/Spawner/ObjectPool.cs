using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IDisableable<T>
{
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _everSpawnedText;
    [SerializeField] private TMP_Text _activeObjectsText;

    private Queue<T> _pool = new Queue<T>();
    private T _prefab;
    private int _everSpawnedObjectsCounter = 0;
    private int _activeObjectCount = 0;

    private void OnDisable()
    {
        foreach (T objectsInPool in _pool)
            objectsInPool.Disabled -= EnqueueGameObject;
    }

    protected void Initialize(T prefab)
    {
        _prefab = prefab;
    }

    protected T GetObject()
    {
        if (_pool.Count == 0)
        {
            T gameObject = Instantiate(_prefab, _container);
            _pool.Enqueue(gameObject);
            gameObject.Disabled += EnqueueGameObject;
            _everSpawnedObjectsCounter++;
            _everSpawnedText.text = Convert.ToString(_everSpawnedObjectsCounter);
        }

        _activeObjectCount++;
        _activeObjectsText.text = Convert.ToString(_activeObjectCount);
        return _pool.Dequeue();
    }

    private void EnqueueGameObject(T t)
    {
        _pool.Enqueue(t);
        _activeObjectCount--;
        _activeObjectsText.text = Convert.ToString(_activeObjectCount);
    }
}
