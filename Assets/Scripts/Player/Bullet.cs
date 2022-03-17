using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private Vector3 _direction;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private int _damage;

    public Vector3 Direction { set { _direction = value; } }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_lifeTime <= 0) gameObject.SetActive(false); //Destroy(gameObject);
        else _lifeTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction * _moveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        var enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.SetDamage(_damage);
            //сброс урона из-за того, что может быть столкновение с несколькими костями,
            //так как Ragdoll
            _damage = 0;
            if (enemy.IsDead)
            {
                enemy.Death();
                //добавить силу для отталкивания пративника при поподании пули
                Vector3 dir = other.contacts[0].point - transform.position;
                dir = -dir.normalized;
                dir.y = 0f;
                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * 200, ForceMode.Impulse);
            }
        }
        gameObject.SetActive(false);
    }

    //Сброс урона и времени жизни
    private void OnEnable()
    {
        _damage = 50;
        _lifeTime = 1.5f;
    }
}
