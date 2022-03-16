using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InterfaceManager : MonoBehaviour
{
    public delegate void StartGameHandler();
#nullable enable
    static public event StartGameHandler? onStartGame;
#nullable disable
    [SerializeField]
    private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _startButton.gameObject.SetActive(false);
        onStartGame?.Invoke();
    }
}
