using System;
using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.CommonStuff.Mechanics
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private ScaleType _scaleType;

        private void Scale()
        {
            var screenValues = CommonTools.GetScreenValues();

            switch (_scaleType)
            {
                case ScaleType.OnlyWidth:
                    // transform.localScale = new Vector3(screenValues.x)
                    break;
                case ScaleType.OnlyHeight:
                    break;
                case ScaleType.Fit:
                    break;
            }
        }
    }

    internal enum ScaleType
    {
        OnlyWidth,
        OnlyHeight,
        Fit
    }
}