using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MaxChance = 1f;
    private const float ChanceDivider = 0.5f;

    [Space(10)]
    [SerializeField, Range(0f, 1f)] private float _chanceSpawn = MaxChance;
    [SerializeField, Min(0)] private int _minCountSpawn = 2;
    [SerializeField, Min(1)] private int _maxCountSpawn = 6;

    public event System.Action OnSpawn;
    public event System.Action OnExplode;

    public float ChanceSpawn => _chanceSpawn;

    public void ReduceChance(float previousChance)
    {
        _chanceSpawn = previousChance * ChanceDivider;
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
                OnSpawn?.Invoke();
            }

            OnExplode?.Invoke();
        }

        Destroy(gameObject);
    }
}
