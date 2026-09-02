using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _chance;

    public float Chance => _chance;

    public void SetChance(float chance) => _chance = chance;

    public void Paint(Color color)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = color;
    }
}
