using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.UIStuff;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UIStuff
{
    public class TapToPlayWindow : WindowBase
    {
        [SerializeField] private Button _playButton;

        public override void Initialize()
        {
            base.Initialize();
            _playButton.onClick.AddListener(Play);
        }

        private void Play()
        {
            _uiService.TriggerEvent(UIEventType.GameStartedAction);
            _uiService.OpenWindow(WindowType.HUD, false, true, true);
            CloseWindow(false);
        }

        public override WindowType GetWindowType() => WindowType.TapToPlayWindow;
    }
}