using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Основной менеджер отвечающий за уровень
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private List<WayPoint> _wayPoints;
    [SerializeField]
    private bool _tapScreen;
    [SerializeField]
    private Vector2 _tapPosition;
    [SerializeField]
    private StateGame _stateGame;
    [SerializeField]
    private int _enemyCountInNextWayPoint;
    [SerializeField]
    private int _currentWayPoint;
    [SerializeField]
    private LayerMask _layer;

    private void Start()
    {
        _stateGame = StateGame.Menu;
        _currentWayPoint = 0;
        _enemyCountInNextWayPoint = _wayPoints[_currentWayPoint + 1].EnemyCount;
    }

    private void Update()
    {
        switch (_stateGame)
        {
            case StateGame.Menu:
                break;
            case StateGame.Start:
                UpdatePosition();
                if (_tapPosition != Vector2.zero) Shoot();
                break;
            case StateGame.Pause:
                break;
        }
    }

    //обноваление позиции игра
    private void UpdatePosition()
    {
        if (_wayPoints.Count > _currentWayPoint + 1 && _wayPoints[_currentWayPoint + 1].EnemyCount == 0)
        {
            _currentWayPoint++;
            _player.MovePlayer(_wayPoints[_currentWayPoint].GetComponentInChildren<StopPoint>().gameObject.transform.position);
        }
    }

    //выстрел
    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(_tapPosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.cyan);
        if (Physics.Raycast(ray, out hit, 20, _layer))
        {
            _player.Shoot(hit.point);
        }
        _tapPosition = Vector2.zero;
    }

    public void StartGame()
    {
        _stateGame = StateGame.Start;
    }
    private void TapScreenStart(Vector2 position)
    {
        _tapScreen = true;
        _tapPosition = position;
    }
    private void TapScreenEnd()
    {
        _tapScreen = false;
    }

    //Подписка на события тапа и начала игры
    private void OnEnable()
    {
        InputManager.onTouchStart += TapScreenStart;
        InputManager.onTouchEnd += TapScreenEnd;
        InterfaceManager.onStartGame += StartGame;
    }

    private void OnDisable()
    {
        InputManager.onTouchStart -= TapScreenStart;
        InputManager.onTouchEnd -= TapScreenEnd;
        InterfaceManager.onStartGame -= StartGame;
    }
}
