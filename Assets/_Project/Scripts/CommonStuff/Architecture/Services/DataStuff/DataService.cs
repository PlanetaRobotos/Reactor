using _Project.Scripts.Architecture.SettingsStuff;
using _Project.Scripts.Services;
using Assets._Project.Scripts.Utilities.Constants;

namespace _Project.Scripts.SettingsStuff
{
    public class DataService
    {
        public WorldSettings WorldSettings { get; }
        public UISettings UISettings { get; }

        public DataService(AssetService assetService)
        {
            WorldSettings = assetService.GetObjectByType<WorldSettings>(AssetPath.GlobalSettingsPath);
            UISettings = assetService.GetObjectByType<UISettings>(AssetPath.GlobalSettingsPath);
        }
    }
}