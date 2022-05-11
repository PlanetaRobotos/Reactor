using System;

namespace _Project.Scripts.SettingsStuff
{ 
    public interface IMinMaxValue<out T> where T : struct, IEquatable<T>
    {
        public T MinValue { get; }
        public T MaxValue { get; }
    }
    
    public readonly struct MinMaxInt : IMinMaxValue<int>
    {
        public MinMaxInt(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int MinValue { get; }
        public int MaxValue { get; }

        public static MinMaxInt Create(int minValue, int maxValue) => new MinMaxInt(minValue, maxValue);
    } 
    
    [Serializable]
    public struct MinMaxFloat
    {
        public MinMaxFloat(float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
        
        public MinMaxFloat(float maxValue)
        {
            MinValue = 0;
            MaxValue = maxValue;
        }

        public float MinValue;
        public float MaxValue;
        
        public static MinMaxFloat Create(float minValue, float maxValue) => new MinMaxFloat(minValue, maxValue);
    } 
}