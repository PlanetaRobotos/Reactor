using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.UIStuff;
using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UIStuff
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;

        public override void Initialize()
        {
            base.Initialize();
            
            CloseWindow(false);
            
            _closeButton.onClick.AddListener(OnResume);
            _restartButton.onClick.AddListener(OnRestart);
            _resumeButton.onClick.AddListener(OnResume);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnResume);
            _restartButton.onClick.RemoveListener(OnRestart);
            _resumeButton.onClick.RemoveListener(OnResume);
        }

        private void OnResume()
        {
            _uiService.UnloadWindow(GetWindowType());
            _uiService.OpenWindow(WindowType.HUD, false, true, true);
        }

        private void OnRestart()
        {
            CommonTools.RestartScene();
        }

        public override WindowType GetWindowType() => WindowType.Pause;
    }
}