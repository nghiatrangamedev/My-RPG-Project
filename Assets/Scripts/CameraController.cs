using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Vector3 offset;

    float _speedZoom = 4f;
    float _minZoom = 5f;
    float _maxZoom = 15f;
    float _currentZoom = 10f;

    float _yawSpeed = 100f;
    float _currentYaw = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * _speedZoom;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

        _currentYaw -= Input.GetAxis("Horizontal") * _yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position - offset * _currentZoom;

        transform.LookAt(_playerTransform.position + Vector3.up * 2);
        transform.RotateAround(transform.position, Vector3.up, _currentYaw);
    }
}
