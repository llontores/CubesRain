using UnityEngine;

public class BombsSpawner : ObjectPool<Bomb>
{
    [SerializeField] private Bomb _bombPrefab;

    private void Start()
    {
        Initialize(_bombPrefab);
    }

    public void SubcribeOnCube(Cube cube)
    {
        cube.Disabled += SpawnBomb;
    }

    private void SpawnBomb(Cube cube)
    {
        Bomb resourse;
        GetObject(out resourse);
        resourse.transform.position = cube.transform.position;
        resourse.gameObject.SetActive(true);
        resourse.SetLifeTime(cube.LifeTime);
        cube.Disabled -= SpawnBomb;
    }
}
