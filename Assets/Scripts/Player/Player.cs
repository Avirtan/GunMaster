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
    [SerializeField]
    private float _speedRotate;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat("Velocity", _agent.desiredVelocity.magnitude);
        // Поворот в сторону следующей точки остановки
        if (_positionRotate != Vector3.zero && _agent.desiredVelocity.magnitude == 0)
        {
            Vector3 targetDir = _positionRotate - transform.position;
            //Если надо убрать вращение по оси Y 
            // targetDir.y = 0.0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * _speedRotate);
        }
    }

    // Выстрел и взятие пули из пулла
    public void Shoot(Vector3 direction)
    {
        var spwnPos = GetComponentInChildren<BulletSpawn>().gameObject.transform.position;
        var bullet = BulletPooler.Instance.SpawnBullet(spwnPos, Quaternion.identity).GetComponent<Bullet>();
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
