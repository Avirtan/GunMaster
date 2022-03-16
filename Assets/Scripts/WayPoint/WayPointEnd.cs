using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        FindObjectOfType<LevelManager>().Restart();
    }
}
