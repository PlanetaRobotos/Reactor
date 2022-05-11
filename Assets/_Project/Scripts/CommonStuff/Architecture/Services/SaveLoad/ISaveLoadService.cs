using System;
using _Project.Scripts.Architecture.Services.SaveLoadService.Data;

namespace _Project.Scripts.Architecture.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        Action OnDataUpdated { get; set; }
        PlayerData PlayerData { get; }
        GameConfig GameConfig { get; }
        bool IsSaveFileExist(string path);
        void SaveAllData();
        void LoadAllData();
        
        // T LoadData<T>();
        // void SaveData<T>();

        /// <summary>
        /// Uses for data transferring between  scenes (Main menu -> gameplay, e.t.c.)
        /// </summary>
        /// <param name="data"></param>
        // void SetCurrentSceneData(ScriptableObject data);
        // ScriptableObject GetCurrentSceneData();
    }
}