using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField]
    private float _rotationEnemyY;

    public float RotationEnemyY { get { return _rotationEnemyY; } }
}
