using UnityEngine;

namespace _Project.Scripts.Architecture.SettingsStuff
{
    [CreateAssetMenu(fileName = "UISettings", menuName = "Settings/Global/UISettings")]
    public class UISettings : ScriptableObject
    {
        [Header("Common")]
        [SerializeField] private float _openDuration = 1f;
        [SerializeField] private float _closeDuration = 1f;

        public float CloseDuration => _closeDuration;

        public float OpenDuration => _openDuration;
    }
}