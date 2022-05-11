using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.Pool;
using _Project.Scripts.Architecture.Services.SaveLoadService;
using _Project.Scripts.Architecture.Services.SaveLoadService.IO;
using _Project.Scripts.Architecture.Services.UIStuff;
using _Project.Scripts.Mechanics;
using _Project.Scripts.Services;
using _Project.Scripts.SettingsStuff;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private GizmosService _gizmosService;

        public override void InstallBindings()
        {
            BindTupleSateManager();
            BindSaveLoadService();
            BindPoolService();
            BindAssetService();
            BindDataService();
            BindRandomService();
            BindUIService();
            BindGizmosService();
        }

        private void BindTupleSateManager()
        {
            Container.Bind<IStateManager>().To<TupleStateManager>();
        }

        private void BindGizmosService()
        {
            Container.Bind<GizmosService>().FromInstance(_gizmosService).AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container.Bind<IWriterReader>().To<JsonWriterReader>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        }

        private void BindUIService()
        {
            Container.Bind<IUIService>().To<UIService>().AsSingle();
        }

        private void BindRandomService()
        {
            Container.Bind<RandomService>().AsSingle();
        }

        private void BindAssetService()
        {
            Container.Bind<AssetService>().AsSingle();
        }

        private void BindDataService()
        {
            Container.Bind<DataService>().AsSingle();
        }

        private void BindPoolService()
        {
            Container.Bind<IPoolService>().To<PoolService>().AsSingle();
        }
    }
}