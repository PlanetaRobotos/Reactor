using _Project.Scripts.Services.InputStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.Game;
using submodules.CommonScripts.CommonScripts.Constants;
using UnityEngine;

namespace _Project.Scripts.Mechanics.WasherStuff
{
    public class Washer : MonoBehaviour
    {
        private InputBehaviour _inputBehaviour;
        private IListable _washers;
        private Transform _transform;

        public void Construct(IListable washers, InputBehaviour inputBehaviour)
        {
            _washers = washers;
            _inputBehaviour = inputBehaviour;
        }

        public void Initialize()
        {
            _transform = transform;

            _inputBehaviour.ObjectMouseClick += ScreenMouseClick;
        }

        public void SetPosition(Vector3 at)
        {
            _transform.position = at;
        }

        private void ScreenMouseClick(GameObject obj, Vector3 position)
        {
            if (obj.layer != LayerMask.NameToLayer(Layers.Washer))
                return;
            
            Debug.Log(gameObject.name);
            _washers.Release(this);
        }
    }
}