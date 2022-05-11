using System;

namespace _Project.Scripts.Architecture.Services.SaveLoadService.Data
{
    [Serializable]
    public class GameConfig
    {
        public int MusicOn = 1;
        public int SoundsOn = 1;
        
        public float MusicVolume = 1f;
        public float SoundsVolume  =  1f;
    }
}