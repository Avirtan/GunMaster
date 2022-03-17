using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointEnd : MonoBehaviour
{
    //Триггер на перезапуск игры
    private void OnTriggerEnter(Collider other) {
        FindObjectOfType<InterfaceManager>().RestartShow();

    }
}
