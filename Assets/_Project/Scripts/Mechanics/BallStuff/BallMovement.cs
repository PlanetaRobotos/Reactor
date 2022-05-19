using _Project.Scripts.Services;
using _Project.Scripts.SettingsStuff;
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

        public void Initialize()
        {
            _rb = GetComponent<Rigidbody2D>();

            _rb.AddForce(_randomService.GetRandomDirectionXY * _ball.CurrentBallState.SpeedRange.MinValue, ForceMode2D.Impulse);
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

        private void FixedUpdate()
        {
            // Debug.Log($"speed = {_rb.velocity.magnitude}, name = {gameObject.name}");
        }
        
        private MinMaxFloat SpeedRange => _ball.CurrentBallState.SpeedRange;
    }
}