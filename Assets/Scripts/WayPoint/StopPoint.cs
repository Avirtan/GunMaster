using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    [SerializeField]
    private LevelManager _levelManager;

    private void Start() {
        _levelManager = FindObjectOfType<LevelManager>();
    }
    // Триггер на обновление индекса текущей платформы
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            // Debug.Log("Update way point");
            _levelManager.UpdateCurrentWayPoint();
        }
    }
}
