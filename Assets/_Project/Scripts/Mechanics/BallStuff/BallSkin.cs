using _Project.Scripts.Settings;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics.BallStuff
{
    public class BallSkin : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _ballRenderer;
        [SerializeField] private MeshRenderer _fgGlowRenderer;
        [SerializeField] private MeshRenderer _bgGlowRenderer;

        private Ball _ball;

        public void Construct(Ball ball)
        {
            _ball = ball;
        }

        public void Initialize()
        {
            SetSkin(_ball.CurrentBallState);
            _ball.IncreaseStateAction += () => SetSkin(_ball.CurrentBallState);
        }

        private void SetSkin(BallState ballState)
        {
            _ballRenderer.material = ballState.BallMaterial;
            _bgGlowRenderer.material = ballState.BackwardGlowMaterial;
            _fgGlowRenderer.material = ballState.ForwardGlowMaterial;

            transform.localScale = Vector3.one * ballState.SizeDelta;
        }
    }
}