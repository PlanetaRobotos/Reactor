using System;
using _Project.Scripts.Settings;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallProperties _ballProperties;
        [SerializeField, ReadOnly] private int _currentState;

        private BallMovement _ballMovement;
        private BallCollision _ballCollision;

        public Action IncreaseStateAction = () => { };
        private BallSkin _ballSkin;

        public void Initialize()
        {
            Debug.Log($"name: {gameObject.name}, code: {gameObject.GetHashCode()}");

            _ballMovement = GetComponent<BallMovement>();
            _ballCollision = GetComponent<BallCollision>();
            _ballSkin = GetComponent<BallSkin>();

            _ballMovement.Construct(this);
            _ballMovement.Initialize();

            _ballCollision.Construct(this, _ballMovement);
            _ballCollision.Initialize();

            _ballSkin.Construct(this);
            _ballSkin.Initialize();

            IncreaseStateAction += OnStateIncreased;
        }

        private void OnStateIncreased()
        {
            CurrentState++;
        }

        public int CurrentState
        {
            get => GetCurrentState();
            private set
            {
                _currentState = GetCurrentState();
                // Debug.Log("GetCurrentState = " + GetCurrentState());
                SetCurrentState(value);
            }
        }

        public BallProperties Properties => _ballProperties;
        
        public BallState CurrentBallState => Properties.BallStates[CurrentState];

        private void SetCurrentState(int value)
        {
            string key = GetKey;
            PlayerPrefs.SetInt(key, value);
        }

        private int GetCurrentState()
        {
            return PlayerPrefs.GetInt(GetKey);
        }
        
        private string GetKey => gameObject.GetHashCode().ToString();
    }
}