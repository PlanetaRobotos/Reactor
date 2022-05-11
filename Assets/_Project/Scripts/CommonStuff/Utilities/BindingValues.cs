using System;
using UnityEngine;

namespace Assets._Project.Scripts.Utilities
{
    [Serializable]
    public class BindingValue<T>
    {
        public event Action<T> ValueChanged;

        [SerializeField]
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        public static implicit operator T(BindingValue<T> bv)
        {
            return bv._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    [Serializable]
    public class BindingInt : BindingValue<int> { }

    [Serializable]
    public class BindingFloat : BindingValue<float> { }

    [Serializable]
    public class BindingString : BindingValue<string> { }
}