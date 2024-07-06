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
            _everSpawnedObjectsCounter++;
            _everSpawnedText.text = Convert.ToString(_everSpawnedObjectsCounter);
            
        }
        unactiveObject = _pool.Dequeue();
        _activeObjectCount++;
        _activeObjectsText.text = Convert.ToString(_activeObjectCount);
    }

    private void EnqueueGameObject(T t)
    {
        _pool.Enqueue(t);
        _activeObjectCount--;
        _activeObjectsText.text = Convert.ToString(_activeObjectCount);

    }
}
