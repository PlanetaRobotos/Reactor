using System;

namespace _Project.Scripts.Architecture.Services.SaveLoadService.Data
{
    [Serializable]
    public class PlayerData
    {
        public int Level
        {
            get => _level;
            set => _level = value;
        }

        private int _level;
    }
}