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
    
    //Уменьшить здоровье и обновить полоску здоровья
    public void SetDamage(int damage)
    {
        // Debug.Log("damage " + damage.ToString() + " " + gameObject.name);
        _health -= damage;
        _slider.value = _health / 100;
    }
    //Смерть и включение ragdoll
    public void Death()
    {
        _animator.enabled = false;
        EnableKinematic(false);
        _slider.gameObject.SetActive(false);
        gameObject.GetComponentInParent<WayPoint>().RemoveEnemy(this);
    }
     
    private void EnableKinematic(bool enabled)
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = enabled;
        }
    }
}
