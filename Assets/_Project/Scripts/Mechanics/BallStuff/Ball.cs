using _Project.Scripts.Settings;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallProperties _ballProperties;

        private BallMovement _ballMovement;
        
        public void Initialize()
        {
            _ballMovement = GetComponent<BallMovement>();
            _ballMovement.Initialize(_ballProperties);
        }
    }
}