using System.Collections.Generic;
using UnityEngine;

public class Pallete : MonoBehaviour
{
    [SerializeField] private List<Color> _colors = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        Color.gray
    };

    public Color GetRandomColor() => _colors[Random.Range(0, _colors.Count)];
}
