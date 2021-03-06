using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _typeEnemy;
    [SerializeField]
    private int _enemyCount;
    [SerializeField]
    private List<Enemy> _enemies;
    public int EnemyCount { get { return _enemies.Count; } }
    [SerializeField]
    private bool _endWayPoint;
    [SerializeField]
    public bool EndWayPoint { get { return _endWayPoint; } }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
    private void Start()
    {
        // если WayPoint является конечным, то не генерировать врагов
        if (!_endWayPoint) GenerateEnemy();
    }
    //Создание врагов на RespawnEnemy точках, 
    //так же можно задать поворот врага по оси Y в RespawnEnemy скрипте, 
    //для корректного поворота врага
    private void GenerateEnemy()
    {
        foreach (var respawn in GetComponentsInChildren<RespawnEnemy>())
        {
            var randomEnemy = Random.Range(0, _typeEnemy.Count);
            var enemy = Instantiate(_typeEnemy[randomEnemy], respawn.gameObject.transform.position, Quaternion.identity);
            enemy.transform.rotation = Quaternion.Euler(enemy.transform.rotation.x, respawn.RotationEnemyY, enemy.transform.rotation.z);
            _enemies.Add(enemy);
            enemy.transform.parent = transform;
        }
    }
    //Обновление всех врагов, при перезапуске уровня
    public void Restart()
    {
        _enemies = new List<Enemy>();
        GenerateEnemy();
    }
}
