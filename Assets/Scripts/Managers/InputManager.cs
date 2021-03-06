using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void TouchStartHandler(Vector2 position);
    public delegate void TouchEndHandler();
#nullable enable
    // Событие на начало тапа
    static public event TouchStartHandler? onTouchStart;
    // Событие на конец тапа
    static public event TouchEndHandler? onTouchEnd;
#nullable disable
    private Vector2 position;

    void Update()
    {
        TouchInput();
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                onTouchStart?.Invoke(_touch.position);
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                onTouchEnd?.Invoke();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            onTouchStart?.Invoke(Input.mousePosition);
        }
    }
}
