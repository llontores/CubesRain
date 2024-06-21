using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Material _startMaterial;
    [SerializeField] private Material _materialToChange;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _minLifeTime;

    private Renderer _renderer;
    private float _lifeTime;
    private Coroutine _destroyAfterTimeJob;
    private int _countPlatformTouches = 0;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = _startMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            if (_countPlatformTouches == 0)
            {
                _renderer.material = _materialToChange;
                SetLifeTime();
            }

            _countPlatformTouches++;
        }
    }

    private void SetLifeTime()
    {
        _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        _destroyAfterTimeJob = StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        WaitForSeconds lifeTime = new WaitForSeconds(_lifeTime);
        yield return lifeTime;
        gameObject.SetActive(false);
        _renderer.material = _startMaterial;
        _countPlatformTouches = 0;
    }
}
