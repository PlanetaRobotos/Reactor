using _Project.Scripts.SettingsStuff;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    [CreateAssetMenu(fileName = "BallProperties", menuName = "Properties/BallProperties")]
    public class BallProperties : ScriptableObject
    {
        [SerializeField] private BallState[] _ballStates;
        
        public BallState[] BallStates => _ballStates;
    }
}