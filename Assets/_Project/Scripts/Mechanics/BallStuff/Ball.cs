using System;
using System.Collections;
using _Project.Scripts.Architecture.Services.UIStuff;
using _Project.Scripts.Settings;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallProperties _ballProperties;
        [SerializeField] private string _key;

        private BallMovement _ballMovement;
        private BallCollision _ballCollision;

        public Action IncreaseStateAction = () => { };
        private BallSkin _ballSkin;
        private IUIService _uiService;

        public void Construct(IUIService uiService)
        {
            _uiService = uiService;
        }

        public void Initialize()
        {
            _ballMovement = GetComponent<BallMovement>();
            _ballCollision = GetComponent<BallCollision>();
            _ballSkin = GetComponent<BallSkin>();

            _ballMovement.Construct(this);
            _ballMovement.Initialize();

            _ballCollision.Construct(this, _ballMovement);
            _ballCollision.Initialize();

            _ballSkin.Construct(this);
            _ballSkin.Initialize();
        }

        public void IncreaseState()
        {
            if (GetCurrentState() >= _ballProperties.BallStates.Length - 1)
            {
                _uiService.TriggerEvent(UIEventType.GameFinishedAction, new Hashtable {{UIEventType.GameFinishedAction, true}});
                
                return;
            }
            SetCurrentState(GetCurrentState() + 1);
            IncreaseStateAction();
        }

        public BallState CurrentBallState => _ballProperties.BallStates[GetCurrentState()];

        private void SetCurrentState(int value)
        {
            string key = GetKey;
            PlayerPrefs.SetInt(key, value);
        }

        private int GetCurrentState() => PlayerPrefs.GetInt(GetKey);

        private string GetKey => _key;
    }
}