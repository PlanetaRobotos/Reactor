using System.Collections.Generic;
using submodules.CommonScripts.CommonScripts.Architecture.Services.AssetsStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.Pool;
using UnityEngine;

namespace _Project.Scripts.FactoriesStuff
{
    public interface IBallFactory : IPoolFactory
    {
    }

    public class BallFactory : PoolFactory, IBallFactory
    {
        protected override int Amount => 2;
        protected override string ContainerName => "Balls";
        protected override string BasePath => "Balls";

        public override void Load()
        {
            _resources = new Dictionary<string, GameObject>
            {
                [BallType.BallSimple.ToString()] = AddResource("BallSimple"),
            };
        }

        public BallFactory(IAssetService assetService, IPoolService poolService) : base(assetService, poolService)
        {
        }
    }

    public enum BallType
    {
        BallSimple,
    }
}