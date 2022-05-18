using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Balls : MonoBehaviour
    {
        [SerializeField] private Ball[] _balls;

        public void Initialize()
        {
            foreach (Ball ball in _balls)
            {
                ball.Initialize();
            }
        }
    }
}