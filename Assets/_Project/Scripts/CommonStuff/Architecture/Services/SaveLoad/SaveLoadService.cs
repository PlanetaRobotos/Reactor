using System;
using System.Collections.Generic;
using _Project.Scripts.Architecture.Services.SaveLoadService.Data;
using _Project.Scripts.Architecture.Services.SaveLoadService.IO;
using _Project.Scripts.SettingsStuff;
using UnityEngine;
using Object = System.Object;

namespace _Project.Scripts.Architecture.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        public Action OnDataUpdated { get; set; }

        public PlayerData PlayerData { get; set; } = new PlayerData();
        public GameConfig GameConfig { get; set; } = new GameConfig();

        private readonly IWriterReader _writerReader;
        private readonly string _playerDataPath;
        private readonly string _gameConfigDataPath;

        public SaveLoadService(IWriterReader writerReader)
        {
            _writerReader = writerReader;

            var dataPath = $"{Application.persistentDataPath}";
            _playerDataPath = $"{dataPath}/{nameof(PlayerData)}";
            _gameConfigDataPath = $"{dataPath}/{nameof(GameConfig)}";

            FirstGameInitialization();
            LoadAllData();
        }

        private void FirstGameInitialization()
        {
            if (!IsSaveFileExist(_playerDataPath))
            {
                PlayerData = new PlayerData();
                SaveAllData();
            }

            if (!IsSaveFileExist(_gameConfigDataPath))
            {
                GameConfig = new GameConfig();
                SaveAllData();
            }
        }

        public bool IsSaveFileExist(string path) => _writerReader.IsExist(path);

        public void SaveAllData()
        {
            _writerReader.Write(_playerDataPath, PlayerData);
            _writerReader.Write(_gameConfigDataPath, GameConfig);
        }

        public void LoadAllData()
        {
            PlayerData = _writerReader.Read<PlayerData>(_playerDataPath);
            GameConfig = _writerReader.Read<GameConfig>(_gameConfigDataPath);
        }
    }
}