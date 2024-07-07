using System.Collections;
using UnityEngine;

public class CubesSpawner : ObjectPool<Cube>
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _minXOffset;
    [SerializeField] private float _maxXOffset;
    [SerializeField] private float _minYOffset;
    [SerializeField] private float _maxYOffset;
    [SerializeField] private BombsSpawner _bombsSpawner;

    private void Start()
    {
        Initialize(_prefab);
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        Cube resource;

        while (true)
        {
            GetObject(out resource);
            resource.gameObject.transform.position = new Vector3(Random.Range(transform.position.x - _minXOffset, transform.position.x + _maxXOffset),
                Random.Range(transform.position.y - _minYOffset, transform.position.y + _maxYOffset), transform.position.z);
            resource.gameObject.SetActive(true);
            _bombsSpawner.SubcribeOnCube(resource);

            yield return delay;
        }
    }
}
