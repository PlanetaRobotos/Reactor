using System.Collections.Generic;
using _Project.Scripts.Settings;
using submodules.CommonScripts.CommonScripts.Architecture.Services;
using submodules.CommonScripts.CommonScripts.Architecture.Services.DataStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.UIStuff;
using submodules.CommonScripts.CommonScripts.Utilities.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Balls : MonoBehaviour
    {
        private IDataService _dataService;
        private IUIService _uiService;
        private IBallFactory _ballFactory;

        private readonly List<BallData> _balls = new();
        private int _ballsAmount;
        private Vector2[] _startPoints;

        [Inject]
        private void Construct(IUIService uiService, IDataService dataService, IBallFactory ballFactory)
        {
            _ballFactory = ballFactory;
            _uiService = uiService;
            _dataService = dataService;
        }

        public void Initialize()
        {
            _ballsAmount = _dataService.WorldSettings.BallsAmount;

            _startPoints =  DistanceTools.GetPoints(_dataService.WorldSettings.MINStartDistanceBetweenBalls, _ballsAmount);

            for (int i = 0; i < _ballsAmount; i++)
            {
                string key = $"ball_{i.ToString()}";

                var ballData = AddBall(key, i);
                _balls.Add(ballData);
            }

            Debug.Log($"dist {DistanceTools.GetDistance(_startPoints[0], _startPoints[1])}");
            Debug.Log($"is above {DistanceTools.IsAbove(_dataService.WorldSettings.MINStartDistanceBetweenBalls, _startPoints[0], _startPoints[1])}");
        }


        private BallData AddBall(string saveKey, int index)
        {
            var ball = _ballFactory.Create(BallType.Simple.ToString()).GetComponent<Ball>();
            ball.Construct(_uiService);
            ball.SetKey(saveKey);
            ball.gameObject.SetActive(true);
            ball.transform.SetParent(transform);

            var startPosition = _startPoints[index];
            ball.Initialize(startPosition);
            return BallData.CreateInstance(saveKey, ball.gameObject, startPosition);
        }
    }

    public class BallData
    {
        private string _saveKey;
        private GameObject _gameObject;
        private Vector2 _startPosition;

        private BallData(string saveKey, GameObject gameObject, Vector2 startPosition)
        {
            _saveKey = saveKey;
            _gameObject = gameObject;
            _startPosition = startPosition;
        }

        public static BallData CreateInstance(string saveKey, GameObject gameObject, Vector2 startPosition)
        {
            return new(saveKey, gameObject, startPosition);
        }
    }
}