using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.UIStuff;
using _Project.Scripts.Architecture.SettingsStuff;
using _Project.Scripts.SettingsStuff;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UIStuff
{
    public abstract class WindowBase : MonoBehaviour
    {
        private const float EndValue = 1.7f;

        private WindowState _windowState;
        private Transform _casedTransform;
        private float _openWindowDuration, _closeWindowDuration;
        private UISettings _uiSettings;
        protected IUIService _uiService;
        protected DataService _dataService;

        public void Construct(DataService dataService, IUIService uiService)
        {
            _uiService = uiService;
            _dataService = dataService;
            _uiSettings = _dataService.UISettings;
        }

        public virtual void Initialize()
        {
            _casedTransform = transform;
            _openWindowDuration = _uiSettings.OpenDuration;
            _closeWindowDuration = _uiSettings.CloseDuration;
        }

        protected void ChangeState(WindowState windowState)
        {
            _windowState = windowState;
            switch (windowState)
            {
                case WindowState.Opened:
                    OnOpenedState();
                    break;
                case WindowState.Closed:
                    OnClosedState();
                    break;
            }
        }

        public abstract WindowType GetWindowType();

        protected virtual void OnOpenedState()
        {
        }

        protected virtual void OnClosedState()
        {
        }

        public void OpenWindow(bool isSmooth)
        {
            ChangeState(WindowState.Opened);
            OpenAnimate(isSmooth);
        }

        public void CloseWindow(bool isSmooth)
        {
            ChangeState(WindowState.Closed);
            CloseAnimate(isSmooth);
        }

        protected virtual void OpenAnimate(bool isSmooth)
        {
            if (isSmooth)
                transform.DOScale(1f, _openWindowDuration).From(EndValue).SetEase(Ease.InOutSine).Play();
            else
                _casedTransform.localScale = Vector3.one;
        }

        protected virtual void CloseAnimate(bool isSmooth)
        {
            if (isSmooth)
            {
                _casedTransform.DOScale(EndValue, _closeWindowDuration)
                    .From(_casedTransform.localScale).SetEase(Ease.InOutSine).Play();
            }
            else
                _casedTransform.localScale = Vector3.zero;
        }

        public bool IsWindowOpened => _windowState == WindowState.Opened;
        public bool IsWindowClosed => _windowState == WindowState.Closed;
    }

    public enum WindowState
    {
        Opened,
        Closed,
    }
}