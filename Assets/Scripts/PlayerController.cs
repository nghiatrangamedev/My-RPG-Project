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

    Interactable _focus;

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
                if (_focus != null)
                {
                    RemoveFocus();
                }

                Vector3 newPosition = hit.point;
                _playerMovement.MoveToPosition(newPosition);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Interactable interactable;

            if (Physics.Raycast(ray, out hit, _movementRange))
            {
                interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (_focus != newFocus)
        {
            if (_focus != null)
            {
                _focus.OnDefocused();
            }

            _focus = newFocus;
            _playerMovement.FollowTarget(_focus);
        }

        _focus.OnFocused(transform);

    }

    void RemoveFocus()
    {
        if (_focus != null)
        {
            _focus.OnDefocused();
        }
        
        _focus = null;
        _playerMovement.StopFollowingTarget();
    }

}
