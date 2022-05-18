using System;
using _Project.Scripts.Services;
using _Project.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private BallProperties _ballProperties;

        [Inject] private RandomService _randomService;

        public void Initialize(BallProperties ballProperties)
        {
            _ballProperties = ballProperties;
            _rb = GetComponent<Rigidbody2D>();

            _rb.AddForce(_randomService.GetRandomDirectionXY * _ballProperties.StartForce, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
        }
    }
}