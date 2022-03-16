using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField]
    private Bullet _prefab;
    [SerializeField]
    private int _size;

    public static BulletPooler Instance;


    private void Awake()
    {
        Instance = this;
    }

    public Queue<GameObject> poolQueue;

    private void Start()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < _size; i++)
        {
            GameObject obj = Instantiate(_prefab.gameObject);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject SpawnBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bulletSpawn = poolQueue.Dequeue();
        bulletSpawn.SetActive(true);
        bulletSpawn.transform.position = position;
        bulletSpawn.transform.rotation = rotation;
        poolQueue.Enqueue(bulletSpawn);
        Debug.Log(bulletSpawn);
        return bulletSpawn;
    }

}
