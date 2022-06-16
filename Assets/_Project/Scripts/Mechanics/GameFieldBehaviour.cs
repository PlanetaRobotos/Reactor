using UnityEngine;
using Zenject;

namespace _Project.Scripts.Mechanics
{
    public class GameFieldBehaviour : MonoBehaviour
    {
        [SerializeField] private GameFieldType _gameFieldType;

        private GameFiledFactory _gameFiledFactory;
        private GameField _gameField;

        [Inject]
        private void Construct(GameFiledFactory gameFiledFactory)
        {
            _gameFiledFactory = gameFiledFactory;
        }

        public void Initialize()
        {
            GameField gameField = _gameFiledFactory.Create(_gameFieldType.ToString());
            _gameField = gameField;
            _gameField.Initialize();
        }

        public GameField GameField => _gameField;
    }
}