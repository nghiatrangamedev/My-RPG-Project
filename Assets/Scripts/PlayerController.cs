using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    Camera _mainCamera;
    PlayerMovement _playerMovement;
    float _movementRange = 100f;
    [SerializeField] LayerMask _movementMask;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _movementRange, _movementMask))
            {
                Debug.Log("Move to " + hit.collider.name);

                Vector3 newPosition = hit.point;
                _playerMovement.MoveToPosition(newPosition);
            }
        }
    }
}
