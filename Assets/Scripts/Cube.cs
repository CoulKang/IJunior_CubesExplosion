using UnityEngine;

public class Cube : MonoBehaviour
{
    private const float ScaleDivider = 0.5f;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Pallete _pallete;
    [SerializeField] private Spawner _spawner;

    public GameObject Prefab => _prefab;

    private void OnEnable()
    {
        _spawner.OnSpawn += Spawn;
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= Spawn;
    }

    private void Spawn()
    {
        GameObject newCube = Instantiate(_prefab, _spawner.transform.position, Quaternion.identity);

        newCube.transform.localScale = transform.localScale * ScaleDivider;

        Spawner newSpawner = newCube.GetComponent<Spawner>();

        if (newSpawner != null)
            newSpawner.ReduceChance(_spawner.ChanceSpawn);

        Paint(newCube);   
    }

    private void Paint(GameObject cube)
    {
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();

        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = _pallete.GetRandomColor();
    }
}
