using _Project.Scripts.Architecture.Services.UIStuff;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class Balls : MonoBehaviour
    {
        [SerializeField] private Ball[] _balls;
        
        [Inject] private IUIService _uiService;

        public void Initialize()
        {
            foreach (Ball ball in _balls)
            {
                ball.Construct(_uiService);
                ball.Initialize();
            }
        }
    }
}