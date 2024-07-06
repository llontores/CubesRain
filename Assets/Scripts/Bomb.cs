using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour, IDisableable<Bomb>
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private float _lifeTime;
    private Material _material;
    private Color _originalColor;

    public event UnityAction<Bomb> Disabled;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _originalColor = _material.color;
    }

    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    private void OnEnable()
    {
        _material.color = _originalColor;
    }

    public void SetLifeTime(float time)
    {
        print(time);
        _lifeTime = time;
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        float elapsed = 0.0f;
        Color color = _material.color;

        while (elapsed < _lifeTime)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(_originalColor.a, 0.0f, elapsed / _lifeTime);
            color.a = alpha;
            _material.color = color;
            yield return null;
        }

        Explode();
        gameObject.SetActive(false);
    }


    private void Explode()
    {
        Collider[] explodableObjects = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider explodableObject in explodableObjects)
        {
            Rigidbody rigidBody = explodableObject.GetComponent<Rigidbody>();

            if (rigidBody != null)
                rigidBody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
