using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static void SetLocalAxis(this Transform transform, Axis axis, float value)
        {
            Vector3 position = transform.localPosition;

            transform.localPosition = axis switch
            {
                Axis.X => new Vector3(value, position.y, position.z),
                Axis.Y => new Vector3(position.x, value, position.z),
                Axis.Z => new Vector3(position.x, position.y, value),
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }

        public static void SetAxis(this Transform transform, Axis axis, float value)
        {
            Vector3 position = transform.position;

            transform.position = axis switch
            {
                Axis.X => new Vector3(value, position.y, position.z),
                Axis.Y => new Vector3(position.x, value, position.z),
                Axis.Z => new Vector3(position.x, position.y, value),
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }

        public static List<Transform> GetAllChildren(this GameObject go)
        {
            List<Transform> children = new List<Transform>();
            Transform parentTransform = go.transform;
            for (int i = 0; i < parentTransform.childCount; i++)
                children.Add(parentTransform.GetChild(i));

            return children;
        }

        public static List<Transform> ActionAllChildren(this GameObject go, Action<Transform> action)
        {
            List<Transform> children = new List<Transform>();
            Transform parentTransform = go.transform;
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform child = parentTransform.GetChild(i);
                children.Add(child);
                action?.Invoke(child);
            }

            return children;
        }
    }
    
    public enum Axis
    {
        X,
        Y,
        Z
    }
}