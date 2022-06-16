using UnityEngine;

namespace _Project.Scripts.Mechanics
{
    public class GameField : MonoBehaviour
    {
        private GameFieldScaler _gameFieldScaler;

        public void Initialize()
        {
            _gameFieldScaler = GetComponent<GameFieldScaler>();
            _gameFieldScaler.Scale();
        }
    }
}