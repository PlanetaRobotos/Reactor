using System.Collections.Generic;
using _Project.Scripts.FactoriesStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.AssetsStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.Pool;
using UnityEngine;

public interface IWasherFactory : IPoolFactory
{
}

public class WasherFactory : PoolFactory, IWasherFactory
{
    protected override string BasePath => "Washers";
    protected override int Amount => 10;
    protected override string ContainerName => "Washers";

    public override void Load()
    {
        _resources = new Dictionary<string, GameObject>
        {
            [WasherType.WasherSimple.ToString()] = AddResource("WasherSimple"),
        };
    }

    public WasherFactory(IAssetService assetService, IPoolService poolService) : base(assetService, poolService)
    {
    }
}

public enum WasherType
{
    WasherSimple,
}