using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private SideType _sideType;
        
        [SerializeField] private bool _useOffset;
        [SerializeField, ShowIf("_useOffset")] private Vector2 _offset;

        private Transform _cachedTransform;
        private ScreenTools.ScreenValues _screenValues;
        private float _depth, _zScale, _halfZScale;
        private Vector3 _center;

        public void Initialize(float depth, float zScale, Vector3 center)
        {
            _center = center;
            _zScale = zScale;
            _halfZScale = _zScale * 0.5f;
            _depth = depth;

            _cachedTransform = transform;
            _screenValues = ScreenTools.GetScreenValues();

            Place();
        }

        private void Place()
        {
            _cachedTransform.position = _center + (Vector3)_offset;
            
            switch (_sideType)
            {
                case SideType.Top:
                    _cachedTransform.position +=
                        new Vector3(0, _screenValues.HalfHeight, -_halfZScale);
                    _cachedTransform.localScale = new Vector3(_screenValues.Width, _depth, _zScale);
                    break;
                case SideType.Left:
                    _cachedTransform.position +=
                        new Vector3(-_screenValues.HalfWidth, 0, -_halfZScale);
                    _cachedTransform.localScale = new Vector3(_depth, _screenValues.Height, _zScale);
                    break;
                case SideType.Right:
                    _cachedTransform.position +=
                        new Vector3(_screenValues.HalfWidth, 0, -_halfZScale);
                    _cachedTransform.localScale = new Vector3(_depth, _screenValues.Height, _zScale);
                    break;
                case SideType.Bottom:
                    _cachedTransform.position +=
                        new Vector3(0, -_screenValues.HalfHeight, -_halfZScale);
                    _cachedTransform.localScale = new Vector3(_screenValues.Width, _depth, _zScale);
                    break;
            }
        }
    }

    internal enum SideType
    {
        Top,
        Left,
        Right,
        Bottom
    }
}