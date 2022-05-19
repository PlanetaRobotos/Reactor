using _Project.Scripts.Architecture.Behaviours;
using Assets._Project.Scripts.Utilities.Constants;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class BallCollision : MonoBehaviour, ICollissible2D
    {
        [SerializeField] private CollisionObserver2D _observer;
        
        private BallMovement _ballMovement;
        private Ball _ball;

        public void Construct(Ball ball, BallMovement ballMovement)
        {
            _ball = ball;
            _ballMovement = ballMovement;
        }

        public void Initialize()
        {
            _observer.Setup(this);
        }
        public void CollisionEnter(Collision2D other)
        {
            if (other.gameObject.CompareTag(Tags.Ball))
            {
                _ball.IncreaseState();
            }
        }

        public void CollisionExit(Collision2D other)
        {
            _ballMovement.SetVelocity();
        }
    }
}