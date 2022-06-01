using System.Collections.Generic;
using _Project.Scripts.Architecture.Services.UIStuff;
using _Project.Scripts.Mechanics;
using _Project.Scripts.Services;
using _Project.Scripts.SettingsStuff;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Balls : MonoBehaviour
    {
        private IDataService _dataService;
        private IUIService _uiService;
        private IBallFactory _ballFactory;
        
        private readonly Dictionary<string, GameObject> _balls = new();
        private RandomService _randomService;
        private int _ballsAmount;
        private Vector2 _nextPoint;
        
        [Inject]
        private void Construct(IUIService uiService, IDataService dataService, IBallFactory ballFactory, RandomService randomService)
        {
            _randomService = randomService;
            _ballFactory = ballFactory;
            _uiService = uiService;
            _dataService = dataService;
        }

        public void Initialize()
        {
            _ballsAmount = _dataService.WorldSettings.BallsAmount;

            _nextPoint = _randomService.GetRandomScreenPoint(150);

            for (int i = 0; i < _ballsAmount; i++)
            {
                string key = $"ball_{i.ToString()}";
                _balls.Add(key, null);
                
                AddBall(key);
            }
        }
        
        private void AddBall(string saveKey)
        {
            var ball = _ballFactory.Create(BallType.Simple.ToString()).GetComponent<Ball>();
            ball.Construct(_uiService);
            ball.SetKey(saveKey);
            ball.gameObject.SetActive(true);
            ball.transform.SetParent(transform);

            var point = GetRandomScreenPoint;
            for (int i = 0; i < _ballsAmount; i++)
            {
                DistanceTools.CheckDistance(300f, point, _nextPoint, ChangePoint);
            }

            ball.Initialize(point);
        }

        private void ChangePoint(Vector2 point)
        {
            _nextPoint = GetRandomScreenPoint;
        }

        private Vector2 GetRandomScreenPoint => _randomService.GetRandomScreenPoint(150);
    }
}