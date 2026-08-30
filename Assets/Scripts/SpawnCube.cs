using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Pallete _palette;

    [Space(5)]
    [SerializeField, Min(0)] private float _scaleDivider = 0.5f;

    private void Awake()
    {
        _palette = GetComponent<Pallete>();
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        if (_spawner != null)
            _spawner.Activated += Spawn;
    }

    private void OnDisable()
    {
        if (_spawner != null)
            _spawner.Activated -= Spawn;
    }

    private void Spawn()
    {
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        float spawnRadius = _spawner.SpawnRadius;

        Vector3 offset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0f,
            Random.Range(-spawnRadius, spawnRadius));

        newCube.transform.position = transform.position + offset;
        newCube.transform.localScale = transform.localScale * _scaleDivider;

        newCube.AddComponent<Rigidbody>();
        newCube.AddComponent<Pallete>();

        Spawner newSpawner = newCube.AddComponent<Spawner>();
        newSpawner.ReduceChance(_spawner.ChanceSpawn);

        newCube.AddComponent<SpawnCube>();

        MeshRenderer renderer = newCube.GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = _palette.GetRandomColor();
    }
}
