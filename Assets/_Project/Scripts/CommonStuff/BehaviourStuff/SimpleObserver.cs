using UnityEngine;

namespace _Project.Scripts.Architecture.Behaviours
{
    public class SimpleObserver : MonoBehaviour
    {
        private ITriggable _parent;

        public void Setup(ITriggable parent) => _parent = parent;

        private void OnTriggerEnter(Collider other) => _parent?.TriggerEnter(other);

        private void OnTriggerExit(Collider other) => _parent?.TriggerExit(other);
    }

    public interface ITriggable
    {
        void TriggerEnter(Collider other);
        void TriggerExit(Collider other);
    }
}