using _Project.Scripts.Architecture.Services.UIStuff;
using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UIStuff
{
    public class FailWindow : WindowBase
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
        
        public override WindowType GetWindowType() => WindowType.FailWindow;
    }
}