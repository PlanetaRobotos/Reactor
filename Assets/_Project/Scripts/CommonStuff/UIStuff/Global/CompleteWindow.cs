using System;
using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.UIStuff;
using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UIStuff
{
    public class CompleteWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;

        public override void Initialize()
        {
            base.Initialize();
            _continueButton.onClick.AddListener(Continue);
        }

        private void Continue()
        {
            CommonTools.RestartScene();
        }

        private void OpenOptions()
        {
            _uiService.OpenWindow(WindowType.Pause, true, true, true);
            CloseWindow(false);
        }

        public override WindowType GetWindowType() => WindowType.CompleteWindow;
    }
}