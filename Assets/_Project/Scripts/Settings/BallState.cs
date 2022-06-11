using submodules.CommonScripts.CommonScripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    [CreateAssetMenu(fileName = "BallState", menuName = "Properties/BallState")]
    public class BallState : ScriptableObject
    {
        [SerializeField] private Material _ballMaterial;
        [SerializeField] private Material _forwardGlowMaterial;
        [SerializeField] private Material _backwardGlowMaterial;
        [SerializeField] private MinMaxFloat _speedRange;
        [SerializeField] private float _sizeDelta;
        
        public MinMaxFloat SpeedRange => _speedRange;
        public Material BackwardGlowMaterial => _backwardGlowMaterial;
        public Material ForwardGlowMaterial => _forwardGlowMaterial;
        public Material BallMaterial => _ballMaterial;
        public float SizeDelta => _sizeDelta;
    }
}