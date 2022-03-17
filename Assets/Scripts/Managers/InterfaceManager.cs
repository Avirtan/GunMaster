using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InterfaceManager : MonoBehaviour
{
    public delegate void StartGameHandler();
#nullable enable
    //Событие на отслеживане начала игры, 
    //так же можно создать события на выход в меню, для приостановлении игры 
    static public event StartGameHandler? onStartGame;
#nullable disable
    [SerializeField]
    private Button _startButton;

    private void Start()
    {
        //Отслеживание нажатия на кнопку старт
        _startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _startButton.gameObject.SetActive(false);
        onStartGame?.Invoke();
    }
}
