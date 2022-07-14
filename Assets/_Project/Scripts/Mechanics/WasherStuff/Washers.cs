using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Mechanics.WasherStuff;
using _Project.Scripts.Services.InputStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.DataStuff;
using submodules.CommonScripts.CommonScripts.Constants;
using submodules.CommonScripts.CommonScripts.Utilities.Tools;
using UnityEngine;
using Zenject;

namespace submodules.CommonScripts.CommonScripts.Architecture.Services.Game
{
    public class Washers : MonoBehaviour, IListable
    {
        [SerializeField] private float _minDistance;

        private readonly Dictionary<Washer, Vector3> _washers = new();

        private InputBehaviour _inputBehaviour;
        private IWasherFactory _washerFactory;
        private IDataService _dataService;

        [Inject]
        private void Construct(InputBehaviour inputBehaviour, IWasherFactory washerFactory, IDataService dataService)
        {
            _dataService = dataService;
            _washerFactory = washerFactory;
            _inputBehaviour = inputBehaviour;
        }

        public void Initialize()
        {
            _inputBehaviour.ObjectMouseClick += ScreenMouseClick;

            _washerFactory.Init();
        }

        private void ScreenMouseClick(GameObject obj, Vector3 position)
        {
            if (obj.layer != LayerMask.NameToLayer(Layers.GameField))
                return;

            if (!AppropriateAmount && EnoughDistanceToOtherWashers(new Vector3(position.x, position.y, 0))) 
            {
                Washer washer = CreateWasher(position);
                Add(washer);
            }
        }

        private bool EnoughDistanceToOtherWashers(Vector3 checkPosition)
        {
            return DistanceTools.IsAboveDistanceToOther(_minDistance, checkPosition, _washers.Values.ToArray());
        }

        private bool AppropriateAmount => _washers.Count > _dataService.WorldSettings.MaxWashersAmount - 1;

        private Washer CreateWasher(Vector3 at)
        {
            GameObject washerObj = _washerFactory.Create(WasherType.WasherSimple.ToString());
            Washer washer = washerObj.GetComponent<Washer>();
            washerObj.SetActive(true);
            washer.Construct(this, _inputBehaviour);
            washer.Initialize();
            var startPosition = new Vector3(at.x, at.y, 0);
            washer.SetPosition(startPosition);
            return washer;
        }

        public void Add<T>(T instance) where T : Component
        {
            Washer washer = (Washer)(Object)instance;
            _washers.Add(washer, washer.transform.position);
        }

        public void Release<T>(T instance) where T : Component
        {
            Washer washer = (Washer)(Object)instance;
            washer.gameObject.SetActive(false);
            _washerFactory.Release(washer.gameObject, WasherType.WasherSimple.ToString());
            _washers.Remove(washer);
        }
    }

    public interface IListable
    {
        void Add<T>(T instance) where T : Component;
        void Release<T>(T instance) where T : Component;
    }
}