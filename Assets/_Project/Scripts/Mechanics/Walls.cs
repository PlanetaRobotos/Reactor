using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics
{
    public class Walls : MonoBehaviour
    {
        [SerializeField] private Wall[] _walls;
        [SerializeField] private float _depth;
        [SerializeField] private float _zScale;
        [SerializeField] private Vector3 _center;

        public void Initialize()
        {
            foreach (Wall wall in _walls)
            {
                wall.Initialize(_depth, _zScale, _center);
            }
        }
    }
}