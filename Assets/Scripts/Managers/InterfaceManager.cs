using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InterfaceManager : MonoBehaviour
{
    public delegate void StartGameHandler();
    public delegate void RestartGameHandler();
#nullable enable
    //Событие на отслеживане начала игры и рестарт, 
    //так же можно создать события на выход в меню, для приостановлении игры 
    static
     public event StartGameHandler? onStartGame;
    static public event RestartGameHandler? onRestartGame;
#nullable disable
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _restartButton;

    private void Start()
    {
        //Отслеживание нажатия на кнопку старт
        _startButton.onClick.AddListener(StartGame);
        _restartButton.onClick.AddListener(RestartGame);

    }

    public void StartGame()
    {
        _startButton.gameObject.SetActive(false);
        onStartGame?.Invoke();
    }
    public void RestartGame()
    {
        _restartButton.gameObject.SetActive(false);
        onRestartGame?.Invoke();
    }

    public void RestartShow()
    {
        _restartButton.gameObject.SetActive(true);
    }


}
