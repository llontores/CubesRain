using System.Collections;
using UnityEngine;

public class CubesSpawner : ObjectPool
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _dalay;
    [SerializeField] private float _minXOffset;
    [SerializeField] private float _maxXOffset;
    [SerializeField] private float _minYOffset;
    [SerializeField] private float _maxYOffset;


    private void Start()
    {
        Initialize(_prefab);
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds delay = new WaitForSeconds(_dalay);
        Cube resource;

        while (true)
        {
            TryGetObject(out resource);
            resource.gameObject.transform.position = new Vector3(Random.Range(transform.position.x - _minXOffset, transform.position.x + _maxXOffset),
                Random.Range(transform.position.y - _minYOffset, transform.position.y + _maxYOffset), transform.position.z);
            resource.gameObject.SetActive(true);


            yield return delay;
        }
    }
}
