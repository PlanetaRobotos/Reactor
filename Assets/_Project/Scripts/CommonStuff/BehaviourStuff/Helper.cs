using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Behaviours
{
    public class Helper : MonoBehaviour
    {
        [SerializeField] private KeyCode _restartKey;
        [SerializeField] private KeyCode _speed2xKey;
        private bool _isIncreased;

        private void Update()
        {
            RestartScene();
            IncreaseSpeed();
        }

        private void IncreaseSpeed()
        {
            if (Input.GetKeyDown(_speed2xKey))
            {
                Time.timeScale = _isIncreased ? 1f : 2f;
                _isIncreased = !_isIncreased;
            }
        }

        private void RestartScene()
        {
            if (Input.GetKeyDown(_restartKey)) 
                CommonTools.RestartScene();
        }
    }
}