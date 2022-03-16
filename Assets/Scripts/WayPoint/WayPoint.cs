using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private int _enemyCount;
    public int EnemyCount { get { return _enemyCount; } }
}
