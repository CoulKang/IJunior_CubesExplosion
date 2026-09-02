using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public event Action<RaycastHit> HitDetected;

    private void OnEnable()
    {
        var inputReader = FindObjectOfType<InputReader>();

        if (inputReader != null)
            inputReader.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        var inputReader = FindObjectOfType<InputReader>();

        if (inputReader != null)
            inputReader.Clicked -= OnClicked;
    }

    private void OnClicked()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            HitDetected?.Invoke(hit);
    }
}
