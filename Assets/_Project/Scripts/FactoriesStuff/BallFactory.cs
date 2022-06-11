using System.Collections.Generic;
using submodules.CommonScripts.CommonScripts.Architecture.Services.AssetsStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.Pool;
using UnityEngine;

public interface IBallFactory
{
    void Load();
    void WarmPool();
    GameObject Create(string name);
    void Release(GameObject obj, string name);
}

public class BallFactory : IBallFactory
{
    private const string BallSimple = "Balls/BallSimple";
    
    private const string ContainerName = "ResourcesPool";

    private readonly IAssetService _assetService;
    private readonly IPoolService _poolService;

    private Dictionary<string, GameObject> _resources;

    private const int Amount = 2;

    public BallFactory(IAssetService assetService, IPoolService poolService)
    {
        _assetService = assetService;
        _poolService = poolService;

        Load();
        WarmPool();
    }

    public void Load()
    {
        _resources = new Dictionary<string, GameObject>
        {
            [BallType.Simple.ToString()] = _assetService.GetObjectByName(BallSimple),
        };
    }

    public void WarmPool()
    {
        foreach (var name in _resources.Keys)
            _poolService.FillPool(PoolInfo.Create(name, Amount, _resources[name], ContainerName));
    }

    public GameObject Create(string name) =>
        _poolService.GetPoolObject(name);

    public void Release(GameObject obj, string name) =>
        _poolService.ReturnToPool(obj, name);
}

public enum BallType
{
    Simple,
}