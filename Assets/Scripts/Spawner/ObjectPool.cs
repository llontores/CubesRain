using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _amount;

    private Queue<Cube> _pool = new Queue<Cube>();
    private Cube _prefab;

    private void OnDisable()
    {
        foreach (Cube cube in _pool)
            cube.Disabled -= EnqueueCube;
    }

    protected void Initialize(Cube cube)
    {
        _prefab = cube;
    }

    protected void TryGetObject(out Cube unactiveCube)
    {
        if(_pool.Count == 0)
        {
            Cube cube = Instantiate(_prefab, _container);
            _pool.Enqueue(cube);
            cube.Disabled += EnqueueCube;
        }
        unactiveCube = _pool.Dequeue();
    }

    private void EnqueueCube(Cube cube)
    {
        _pool.Enqueue(cube);
    }
}