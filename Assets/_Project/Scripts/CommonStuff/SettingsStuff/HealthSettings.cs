using UnityEngine;

namespace _Project.Scripts.Architecture.SettingsStuff
{
    [CreateAssetMenu(fileName = "HealthSettings", menuName = "Settings/HealthSettings")]
    public class HealthSettings : ScriptableObject
    {
        [SerializeField] protected int _maxHealth = 1;

        public int MAXHealth => _maxHealth;
    }
}