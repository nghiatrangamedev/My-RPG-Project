using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent _agent;
    Interactable _target;
    float _speedLookRotation = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            _agent.stoppingDistance = _target._radiusCanInteractable * 0.8f;
            LookAtTheTarget();
            _agent.SetDestination(_target.transform.position);
        }
    }

    public void FollowTarget(Interactable newTarget)
    {
        _target = newTarget;
        _agent.updateRotation = false;
    }

    public void StopFollowingTarget()
    {
        _target = null;
        _agent.updateRotation = true;
    }

    public void MoveToPosition(Vector3 newPosition)
    {
        _agent.SetDestination(newPosition);
    }

    void LookAtTheTarget()
    {
        Vector3 lookDirection = (_target.transform.position - transform.position).normalized;
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _speedLookRotation * Time.deltaTime );
    }
}
