using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class FadingIconAnimator : MonoBehaviour
    {
        private Animator _animator;
        
        private static readonly int FadeFlashTrigger = Animator.StringToHash("FadeFlash");
        private static readonly int StopFadeFlashTrigger = Animator.StringToHash("StopFadeFlashTrigger");
        private static readonly int FadeFlashOnceTrigger = Animator.StringToHash("FadeFlashOnce");
        private static readonly int FadeUpTrigger = Animator.StringToHash("FadeUp");
        private static readonly int FadeDownTrigger = Animator.StringToHash("FadeDown");

        public void Initialize()
        {
            _animator = GetComponent<Animator>();
        }

        public void BeginFadeFlash() => _animator.SetTrigger(FadeFlashTrigger);
        public void StopFadeFlash() => _animator.SetTrigger(StopFadeFlashTrigger);
        
        public void FadeFlashOnce() => _animator.SetTrigger(FadeFlashOnceTrigger);
        public void FadeUp() => _animator.SetTrigger(FadeUpTrigger);
        public void FadeDown() => _animator.SetTrigger(FadeDownTrigger);
    }
}