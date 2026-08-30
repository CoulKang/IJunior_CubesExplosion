using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MaxChance = 1f;
    private const float ChanceDivider = 0.5f;

    [SerializeField, Range(0f, 1f)] private float _chanceSpawn = MaxChance;
    [SerializeField, Min(0)] private float _spawnRadius = 1f;
    [SerializeField] private int _minCountSpawn = 2;
    [SerializeField] private int _maxCountSpawn = 6;

    [Space(5)]
    [SerializeField] private float _explosionForce = 1000f;

    public event System.Action Activated;

    public float SpawnRadius => _spawnRadius;

    public float ChanceSpawn => _chanceSpawn;

    public void ReduceChance(float previousChance)
    {
        _chanceSpawn = previousChance * ChanceDivider;
    }

    private void OnValidate()
    {
        if (_minCountSpawn >= _maxCountSpawn)
            _minCountSpawn = _maxCountSpawn - 1;
    }

    private void OnMouseUpAsButton()
    {
        Launch();
    }

    private void Launch()
    {
        float currentChance = Random.Range(0f, MaxChance);

        if (currentChance <= _chanceSpawn)
        {
            int currentCountSpawn = Random.Range(_minCountSpawn, _maxCountSpawn + 1);

            for (int i = 0; i < currentCountSpawn; i++)
            {
                Activated?.Invoke();
            }
                
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _spawnRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _spawnRadius);

        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }
}
