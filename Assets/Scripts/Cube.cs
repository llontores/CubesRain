using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, IDisableable<Cube>
{
    [SerializeField] private Material _startMaterial;
    [SerializeField] private Material _materialToChange;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _minLifeTime;

    public event UnityAction<Cube> Disabled;
    public float LifeTime => _lifeTime;

    private Renderer _renderer;
    private float _lifeTime;
    private int _countPlatformTouches = 0;

    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = _startMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_countPlatformTouches == 0)
        {
            if (collision.collider.TryGetComponent(out Platform platform))
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
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return _lifeTime;
        gameObject.SetActive(false);
        _renderer.material = _startMaterial;
        _countPlatformTouches = 0;
    }
}
