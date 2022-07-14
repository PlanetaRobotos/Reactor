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
        private IInstantiateProvider instantiatorProvider;

        [Inject]
        private void Construct(IInstantiateProvider instantiatorProvider)
        {
            this.instantiatorProvider = instantiatorProvider;
        }

        public void Initialize()
        {
            foreach (var wallProperty in _wallProperties)
            {
                var wall = instantiatorProvider.Instantiate(_wallPrefab, transform);
                wall.Initialize(_depth, _zScale, _center);
                wall.Place(wallProperty);
            }
        }
    }
}