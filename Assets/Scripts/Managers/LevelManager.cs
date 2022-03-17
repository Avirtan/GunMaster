using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Основной класс отвечающий за уровень
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
    [SerializeField]
    private float _delayStop;

    private void Start()
    {
        _stateGame = StateGame.Menu;
        _currentWayPoint = 0;
        _player.SetPositionNextSopPoint(_wayPoints[_currentWayPoint + 1].GetComponentInChildren<StopPoint>().gameObject.transform.position);
        _player.transform.position = _wayPoints[_currentWayPoint].GetComponentInChildren<StopPoint>().gameObject.transform.position;
    }

    private void Update()
    {
        //Отслеживания состояние игры
        switch (_stateGame)
        {
            case StateGame.Menu:
                break;
            case StateGame.Start:
                UpdatePosition();
                if (_tapPosition != Vector2.zero) Shoot();
                break;
        }
        _delayStop -= Time.deltaTime;
    }

    private void UpdatePosition()
    {
        //Проверка если следующий WayPoint не последний, и все враги убиты, то происходит перемещение 
        //игрока и поворот в сторону следующей точки остановки 
        if (_wayPoints.Count > _currentWayPoint + 1 && !_wayPoints[_currentWayPoint + 1].EndWayPoint)
        {
            var nextWayPoint = _currentWayPoint + 1;
            _enemyCountInNextWayPoint = _wayPoints[nextWayPoint].EnemyCount;
            if (_wayPoints[nextWayPoint].EnemyCount == 0)
            {
                var IndexNextStopPoint = _currentWayPoint + 1 < _wayPoints.Count ? _currentWayPoint + 1 : _currentWayPoint;
                _player.MovePlayer(_wayPoints[IndexNextStopPoint].GetComponentInChildren<StopPoint>().gameObject.transform.position);
            }
        }
        //Задержка для последней платформы, чтобы был сначала поворт и потом движение
        else if (_wayPoints[_currentWayPoint + 1].EndWayPoint && _delayStop <= 0)
        {

            _player.MovePlayer(_wayPoints[_currentWayPoint + 1].GetComponentInChildren<StopPoint>().gameObject.transform.position);
        }
    }

    public void UpdateCurrentWayPoint()
    {
        _delayStop = 1;
        _currentWayPoint++;
        _player.SetPositionNextSopPoint(_wayPoints[_currentWayPoint + 1].GetComponentInChildren<StopPoint>().gameObject.transform.position);
    }

    public void Restart()
    {
        foreach (var enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        foreach (var wayPoint in GameObject.FindObjectsOfType<WayPoint>())
        {
            wayPoint.Restart();
        }
        _currentWayPoint = 0;
        _player.transform.position = _wayPoints[_currentWayPoint].GetComponentInChildren<StopPoint>().gameObject.transform.position;
        _player.SetPositionNextSopPoint(_wayPoints[1].GetComponentInChildren<StopPoint>().gameObject.transform.position);
        _player.MovePlayer(_wayPoints[0].GetComponentInChildren<StopPoint>().gameObject.transform.position);
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
        InterfaceManager.onRestartGame += Restart;
    }

    private void OnDisable()
    {
        InputManager.onTouchStart -= TapScreenStart;
        InputManager.onTouchEnd -= TapScreenEnd;
        InterfaceManager.onStartGame -= StartGame;
        InterfaceManager.onRestartGame -= Restart;
    }
}
