using System.Collections.Generic;
using System.IO;
using submodules.CommonScripts.CommonScripts.Architecture.Services.AssetsStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.Pool;
using UnityEngine;

namespace _Project.Scripts.FactoriesStuff
{
    public abstract class PoolFactory : IPoolFactory
    {
        private readonly IAssetService _assetService;
        private readonly IPoolService _poolService;

        protected Dictionary<string, GameObject> _resources;

        protected abstract string ContainerName { get; }
        protected abstract int Amount { get; }
        protected abstract string BasePath { get; }
        
        protected PoolFactory(IAssetService assetService, IPoolService poolService)
        {
            _assetService = assetService;
            _poolService = poolService;
        }

        public void Init()
        {
            Load();
            WarmPool();
        }

        public abstract void Load();

        public void WarmPool()
        {
            foreach (var name in _resources.Keys)
                _poolService.FillPool(PoolInfo.Create(name, Amount, _resources[name], ContainerName));
        }

        public GameObject Create(string name) =>
            _poolService.GetPoolObject(name);

        public void Release(GameObject obj, string name) =>
            _poolService.ReturnToPool(obj, name);
    
        protected GameObject AddResource(string fileName) => 
            _assetService.GetObjectByName(Path.Join(BasePath, fileName));
    }
    
    public interface IPoolFactory
    {
        void Init();
        void Load();
        void WarmPool();
        GameObject Create(string name);
        void Release(GameObject obj, string name);
    }
}