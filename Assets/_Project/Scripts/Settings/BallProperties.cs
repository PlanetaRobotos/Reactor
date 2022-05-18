using UnityEngine;

namespace _Project.Scripts.Settings
{
    [CreateAssetMenu(fileName = "BallProperties", menuName = "Properties/BallProperties")]
    public class BallProperties : ScriptableObject
    {
        [SerializeField] private float _startForce;

        public float StartForce => _startForce;
    }
}