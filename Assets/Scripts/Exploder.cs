using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [Space(10)]
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] private float _explosionForce = 1000f;

    private void OnEnable()
    {
        _spawner.OnExplode += Explode;
    }

    private void OnDisable()
    {
        _spawner.OnExplode -= Explode;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }
}
