using _Project.Scripts.SettingsStuff;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    [CreateAssetMenu(fileName = "BallProperties", menuName = "Properties/BallProperties")]
    public class BallProperties : ScriptableObject
    {
        [SerializeField] private float _startForce;
        [SerializeField] private BallState[] _ballStates;
        
        public float StartForce => _startForce;

        public BallState[] BallStates => _ballStates;
    }
}