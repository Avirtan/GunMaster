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
    [SerializeField]
    private Vector3 _positionRotate;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat("Velocity", _agent.desiredVelocity.magnitude);
        if (_positionRotate != Vector3.zero)
            transform.LookAt(_positionRotate);
    }

    public void Shoot(Vector3 direction)
    {
        var spwnPos = GetComponentInChildren<BulletSpawn>().gameObject.transform.position;
        var bullet = BulletPooler.Instance.SpawnBullet(spwnPos, Quaternion.identity).GetComponent<Bullet>();
        // var bullet = Instantiate(_bullet, spwnPos, Quaternion.identity);
        // var bulletInPlane = Instantiate (_bullet, direction, Quaternion.identity);
        Debug.DrawLine(spwnPos, direction, Color.black);
        bullet.Direction = (direction - spwnPos).normalized;
    }

    public void SetPositionNextSopPoint(Vector3 position)
    {
        _positionRotate = position;
    }

    public void MovePlayer(Vector3 position)
    {
        _agent.SetDestination(position);
    }

}
