using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _amount;

    private List<Cube> _pool = new List<Cube>();

    protected void Initialize(Cube prefab)
    {
        for (int i = 0; i < _amount; i++)
        {
            Cube spawned = Instantiate(prefab, _container);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out Cube unactiveCube)
    {
        unactiveCube = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return unactiveCube != null;
    }
}