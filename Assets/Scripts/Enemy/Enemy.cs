using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private Slider _slider;
    private Rigidbody[] _rigidbodies;
    private Animator _animator;

    public bool IsDead { get { return _health <= 0; } }

    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        EnableKinematic(true);
    }

    public void SetDamage(int damage)
    {
        _health -= damage;
        _slider.value = _health / 100;
    }

    public void Death()
    {
        _animator.enabled = false;
        EnableKinematic(false);
        _slider.enabled = false;
        // ForceDeath();
    }

    private void ForceDeath()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.AddForce(Vector3.forward * 100);
        }
    }

    private void EnableKinematic(bool enabled)
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = enabled;
        }
    }
}
