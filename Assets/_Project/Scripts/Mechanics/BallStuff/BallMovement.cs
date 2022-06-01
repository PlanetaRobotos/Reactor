using System;
using _Project.Scripts.Services;
using _Project.Scripts.SettingsStuff;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [Inject] private RandomService _randomService;
        private Ball _ball;

        public void Construct(Ball ball)
        {
            _ball = ball;
        }

        public void Initialize(Vector2 startPoint)
        {
            _rb = GetComponent<Rigidbody2D>();

            transform.position = startPoint;
            // DOVirtual.DelayedCall(0.1f, AddRandomForce);
        }

        private void AddRandomForce()
        {
            Vector2 randomDirection = _randomService.GetRandomDirectionXY * _ball.CurrentBallState.SpeedRange.MinValue;
            _rb.AddForce(randomDirection, ForceMode2D.Impulse);
        }

        public void SetVelocity()
        {
            Vector3 velocity = _rb.velocity;
            float speed = velocity.magnitude;
            velocity.Normalize();
            
            if (speed < SpeedRange.MinValue || speed > SpeedRange.MinValue)
            {
                speed = Mathf.Clamp(speed, SpeedRange.MinValue, SpeedRange.MinValue);
                _rb.velocity = velocity * speed;
            }
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddRandomForce();
            }
        }*/

        private MinMaxFloat SpeedRange => _ball.CurrentBallState.SpeedRange;
    }
}