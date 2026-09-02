using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float ChanceDivider = 0.5f;
    private const float ScaleDivider = 0.5f;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Pallete _pallete;

    public List<GameObject> SpawnCubes(Vector3 position, Vector3 parentScale, float parentChance, int minCount, int maxCount)
    {
        int count = Random.Range(minCount, maxCount + 1);

        List<GameObject> spawned = new();

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = Random.insideUnitSphere;
            Vector3 spawnPos = position + offset;

            GameObject newCube = Instantiate(_cubePrefab, spawnPos, Quaternion.identity);

            newCube.transform.localScale = parentScale * ScaleDivider;

            Cube cubeComp = newCube.GetComponent<Cube>();

            if (cubeComp != null)
            {
                float newChance = parentChance * ChanceDivider;

                cubeComp.SetChance(newChance);
                cubeComp.Paint(_pallete.GetRandomColor());
            }

            spawned.Add(newCube);
        }

        return spawned;
    }
}
