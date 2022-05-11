using System.Collections.Generic;
using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.UIStuff;
using _Project.Scripts.Mechanics;
using CodeMonkey.Utils;
using UnityEngine;

namespace _Project.Scripts.UIStuff
{
    public class PointersWindow : WindowBase
    {
        [Header("Properties")] [SerializeField]
        private float _borderSize;

        [Header("Sub Behaviours")] [SerializeField]
        private RectTransform _pointerPrefab;

        [SerializeField] private Transform _pointersParent;

        private readonly List<PointerElement> _pointers = new(1);
        private Camera _camera;

        private Camera _uiCamera;

        public override void Initialize()
        {
            base.Initialize();

            _camera = Camera.main;
            _uiCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            foreach (var target in _pointers) 
                CalculatePointer(target);
        }

        private void CalculatePointer(PointerElement target)
        {
            Vector3 targetPosScreenPoint = _camera.WorldToScreenPoint(target.Target.position);

            switch (target.PointerState)
            {
                case PointerState.OnTheScreen:
                    if (IsOffScreen(targetPosScreenPoint))
                    {
                        Show(target.Pointer);
                        target.PointerState = PointerState.OffScreen;
                    }

                    break;
                case PointerState.OffScreen:
                    if (!IsOffScreen(targetPosScreenPoint))
                    {
                        Hide(target.Pointer);
                        target.PointerState = PointerState.OnTheScreen;
                    }

                    Vector3 cappedTargetScreenPos = targetPosScreenPoint;
                    cappedTargetScreenPos = AttachToScreenBorder(cappedTargetScreenPos, target.Pointer);
                    Vector3 pointerWorldPos = _uiCamera.ScreenToWorldPoint(cappedTargetScreenPos);
                    target.Pointer.position = pointerWorldPos;
                    var localPosition = target.Pointer.localPosition;
                    localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
                    target.Pointer.localPosition = localPosition;
                    break;
            }
        }

        private Vector3 AttachToScreenBorder(Vector3 cappedTargetScreenPos, RectTransform pointer)
        {
            if (cappedTargetScreenPos.x <= _borderSize)
            {
                cappedTargetScreenPos.x = _borderSize;
                pointer.localEulerAngles = Vector3.forward * 180;
            }

            if (cappedTargetScreenPos.x >= Screen.width - _borderSize)
            {
                cappedTargetScreenPos.x = Screen.width - _borderSize;
                pointer.localEulerAngles = Vector3.zero;
            }

            if (cappedTargetScreenPos.y <= _borderSize)
            {
                cappedTargetScreenPos.y = _borderSize;
                pointer.localEulerAngles = Vector3.forward * -90;
            }

            if (cappedTargetScreenPos.y >= Screen.height - _borderSize)
            {
                cappedTargetScreenPos.y = Screen.height - _borderSize;
                pointer.localEulerAngles = Vector3.forward * 90;
            }

            return cappedTargetScreenPos;
        }

        private bool IsOffScreen(Vector3 targetPosScreenPoint) =>
            targetPosScreenPoint.x <= _borderSize ||
            targetPosScreenPoint.x >= Screen.width - _borderSize ||
            targetPosScreenPoint.y <= _borderSize ||
            targetPosScreenPoint.y >= Screen.height - _borderSize;

        private void SmartRotation(PointerElement target)
        {
            Vector3 toPos = target.Target.position;
            Vector3 fromPos = _camera.transform.position;
            fromPos.z = 0f;
            Vector3 dir = (toPos - fromPos).normalized;
            float angle = UtilsClass.GetAngleFromVectorFloat(dir);
            target.Pointer.localEulerAngles = new Vector3(0, 0, angle);
        }

        private static void Show(RectTransform pointer)
        {
            pointer.gameObject.SetActive(true);
        }

        private static void Hide(RectTransform pointer)
        {
            pointer.gameObject.SetActive(false);
        }

        private void AddPointer(Transform target)
        {
            var pointer = Instantiate(_pointerPrefab, _pointersParent);
            _pointers.Add(PointerElement.CreateInstance(pointer, PointerState.OnTheScreen, target));
            Hide(pointer);
        }

        public void RemovePointer(Transform target)
        {
            PointerElement pointerElement = _pointers.Find(p => p.Target == target);
            _pointers.Remove(pointerElement);
        }
        
        public override WindowType GetWindowType() => WindowType.Pointer;
    }

    internal class PointerElement
    {
        public static PointerElement
            CreateInstance(RectTransform pointer, PointerState pointerState, Transform target) =>
            new PointerElement(pointer, pointerState, target);

        private PointerElement(RectTransform pointer, PointerState pointerState, Transform target)
        {
            Target = target;
            PointerState = pointerState;
            Pointer = pointer;
        }

        public RectTransform Pointer { get; }

        public PointerState PointerState { get; set; }

        public Transform Target { get; }
    }

    public enum PointerState
    {
        OnTheScreen,
        OffScreen
    }
}