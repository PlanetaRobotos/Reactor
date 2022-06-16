using System.Collections.Generic;
using _Project.Scripts.Mechanics;
using submodules.CommonScripts.CommonScripts.Architecture.Services.AssetsStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.InstantiateStuff;
using submodules.CommonScripts.CommonScripts.Architecture.Services.Pool;
using submodules.CommonScripts.CommonScripts.Constants;
using UnityEngine;

public class GameFiledFactory
{
    private const string GameFieldBoxSimple = "GameFieldBoxSimple";

    private readonly IAssetService _assetService;

    private Dictionary<string, GameField> _resources;
    private readonly IInstantiate _instantiateProvider;

    public GameFiledFactory(IAssetService assetService, IInstantiate instantiateProvider)
    {
        _instantiateProvider = instantiateProvider;
        _assetService = assetService;

        Load();
    }

    public void Load()
    {
        _resources = new Dictionary<string, GameField>
        {
            [GameFieldType.BoxSimple.ToString()] = _instantiateProvider.Load<GameField>(
                $"{AssetPath.GameFieldsPath}/{GameFieldBoxSimple}"),
        };
    }

    public GameField Create(string name) =>
        _instantiateProvider.Instantiate(_resources[name]);
}

internal enum GameFieldType
{
    BoxSimple,
}