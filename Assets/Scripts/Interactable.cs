using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float _radiusCanInteractable;
    Transform _playerTransform;
    bool _isFocus;
    bool _isInteracted;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusCanInteractable);
    }

    public virtual void Interactive()
    {
        Debug.Log("Interactive with " + transform.name);
    }

    private void Update()
    {
        if (_isFocus)
        {
            float distance = Vector3.Distance(_playerTransform.position, transform.position);

            if (distance <= _radiusCanInteractable && !_isInteracted)
            {
                _isInteracted = true;
                Interactive();
            }
        }
    }

    public void OnFocused(Transform newPlayerTransform)
    {
        _isFocus = true;
        _isInteracted = false;
        _playerTransform = newPlayerTransform;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _isInteracted = false;
        _playerTransform = null;
    }
}
