using System.Collections;
using _Project.Scripts.UIStuff;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.Architecture.Services.UIStuff
{
    public interface IUIService
    {
        Transform GameCanvas { get; }
        void Initialize(Transform gameCanvas);
        void CloseWindow(WindowBase window, bool isSmooth);
        WindowBase OpenWindow(WindowType windowType, bool needCreate, bool isSmooth, bool needOpen);
        public void UnloadWindow(WindowType windowType);
        public WindowBase[] LoadWindows();

        public void AddListener(UIEventType type, UnityAction<Hashtable> listener);
        public void RemoveListener(UIEventType type, UnityAction<Hashtable> listener);
        public void TriggerEvent(UIEventType type, Hashtable param = null);
    }

    public enum WindowType
    {
        HUD,
        Options,
        Pause,
        CompleteWindow,
        TapToPlayWindow,
        DonatWindow,
        FailWindow,
        Pointer
    }
}