using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Min(0)] private float _radiusExplosion = 1f;
    [SerializeField, Min(0)] private float _forceExplosion = 300f;

    public void Explode(Vector3 center)
    {
        float sizeFactor = 1f / Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        float totalRadius = _radiusExplosion * sizeFactor;
        float totalForce = _forceExplosion * sizeFactor;

        Collider[] hits = Physics.OverlapSphere(center, totalRadius);

        foreach (var hit in hits)
        {
            Rigidbody rigidbody = hit.attachedRigidbody;

            if (rigidbody != null)
                rigidbody.AddExplosionForce(totalForce, center, totalRadius);
        }
    }
}
