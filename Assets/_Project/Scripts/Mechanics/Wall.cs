using System;
using submodules.CommonScripts.CommonScripts.Utilities.Tools;
using UnityEngine;

namespace _Project.Scripts.Mechanics
{
    public class Wall : MonoBehaviour
    {
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
            _screenValues = ScreenTools.GetScreenValuesAspect();
        }

        public void Place(WallProperties wallProperties)
        {
            _cachedTransform.position = _center + (Vector3)wallProperties.Offset;
            
            switch (wallProperties.SideType)
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

    [Serializable]
    public class WallProperties
    {
        public SideType SideType;
        public Vector2 Offset;
    }    

    public enum SideType
    {
        Top,
        Left,
        Right,
        Bottom
    }
}