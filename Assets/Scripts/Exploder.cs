using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Min(0)] private float _radiusExplosion = 1f;
    [SerializeField, Min(0)] private float _forceExplosion = 300f;

    public void Explode(Vector3 center)
    {
        Collider[] hits = Physics.OverlapSphere(center, _radiusExplosion);

        foreach (var hit in hits)
        {
            Rigidbody rigidbody = hit.attachedRigidbody;

            if (rigidbody != null)
                rigidbody.AddExplosionForce(_forceExplosion, center, _radiusExplosion);
        }
    }
}
