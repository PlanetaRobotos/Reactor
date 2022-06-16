using submodules.CommonScripts.CommonScripts.Architecture.Services.InstantiateStuff;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Mechanics
{
    public class Walls : MonoBehaviour
    {
        [SerializeField] private Wall _wallPrefab;
        [SerializeField] private WallProperties[] _wallProperties;
        [SerializeField] private float _depth;
        [SerializeField] private float _zScale;
        [SerializeField] private Vector3 _center;
        private IInstantiate _instantiateProvider;

        [Inject]
        private void Construct(IInstantiate instantiateProvider)
        {
            _instantiateProvider = instantiateProvider;
        }

        public void Initialize()
        {
            foreach (var wallProperty in _wallProperties)
            {
                var wall = _instantiateProvider.Instantiate(_wallPrefab, transform);
                wall.Initialize(_depth, _zScale, _center);
                wall.Place(wallProperty);
            }
        }
    }
}