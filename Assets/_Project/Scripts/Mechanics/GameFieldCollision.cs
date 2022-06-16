using UnityEngine;

namespace _Project.Scripts.Mechanics
{
    public class GameFieldCollision : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        // public bool IsInside(Vector3 point)
        // {
        //     Vector3 closest = _collider.ClosestPoint(point);
        //     return closest == point;
        // }

        public Collider Collider => _collider;
    }
}