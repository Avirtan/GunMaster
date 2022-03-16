using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Bullet _bullet;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat("Velocity", _agent.desiredVelocity.magnitude);
    }

    public void Shoot(Vector3 direction)
    {
        var spwnPos = GetComponentInChildren<BulletSpawn>().gameObject.transform.position;
        var bullet = Instantiate(_bullet, spwnPos, Quaternion.identity);
        // var bulletInPlane = Instantiate (_bullet, direction, Quaternion.identity);
        Debug.DrawLine(spwnPos, direction, Color.black);
        bullet.Direction = (direction - spwnPos).normalized;
    }

    public void MovePlayer(Vector3 position)
    {
        _agent.SetDestination(position);
    }

}
