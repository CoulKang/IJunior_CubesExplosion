using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    [Space(10)]
    [SerializeField] private int _minSpawn = 2;
    [SerializeField] private int _maxSpawn = 6;

    private void OnEnable()
    {
        var raycaster = FindObjectOfType<Raycaster>();

        if (raycaster != null)
            raycaster.HitDetected += OnHitDetected;
    }

    private void OnDisable()
    {
        var raycaster = FindObjectOfType<Raycaster>();

        if (raycaster != null)
            raycaster.HitDetected -= OnHitDetected;
    }

    private void OnHitDetected(RaycastHit hit)
    {
        Cube cube = hit.collider.GetComponent<Cube>();

        if (cube == null) 
            return;

        float chance = cube.Chance;

        if (Random.value <= chance)
        {
            _spawner.SpawnCubes(cube.transform.position, cube.transform.localScale, chance, _minSpawn, _maxSpawn);
            
            _exploder.Explode(cube.transform.position);
        }

        Destroy(cube.gameObject);
    }
}
