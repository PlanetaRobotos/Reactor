using System.Collections.Generic;
using _Project.Scripts.Mechanics;
using _Project.Scripts.SettingsStuff;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Services
{
    public class RandomService
    {
        public Vector3 GetOffsetFor3Axis(MinMaxFloat xOffset, MinMaxFloat yOffset, MinMaxFloat zOffset)
        {
            float randX = GetValue(xOffset);
            float randY = GetValue(yOffset);
            float randZ = GetValue(zOffset);
            return new Vector3(randX, randY, randZ);
        }

        public Vector3 GetOffsetFor3Axis(MinMaxFloat offset)
        {
            float randX = GetValue(offset);
            float randY = GetValue(offset);
            float randZ = GetValue(offset);
            return new Vector3(randX, randY, randZ);
        }

        public float GetValue(MinMaxFloat offset) =>
            Random.Range(offset.MinValue, offset.MaxValue);

        public Vector2 GetValue(Borders borders) =>
            new Vector2(Random.Range(borders.XBorder.MinValue, borders.XBorder.MaxValue),
                Random.Range(borders.YBorder.MinValue, borders.YBorder.MaxValue));

        public float GetValue(float min, float max)
        {
            MinMaxFloat offset = new MinMaxFloat(min, max);
            return Random.Range(offset.MinValue, offset.MaxValue);
        }

        public int GetValue(MinMaxInt minMaxValue) =>
            Random.Range(minMaxValue.MinValue, minMaxValue.MaxValue);

        public int GetValue(int min, int max) =>
            Random.Range(min, max);

        public Vector3 GetRandomDirection()
        {
            return new Vector3(GetValue(-1f, 1f), 0f, GetValue(-1f, 1f)).normalized;
        }

        public Vector2 GetRandomPointInsideCircle(float patrolRadius) =>
            Random.insideUnitCircle * patrolRadius;

        public List<int> GetDifferentValues(int amount, MinMaxInt minMaxValue)
        {
            var randomList = new List<int>(amount);
            for (int i = 0; i < amount; i++)
            {
                int numToAdd = GetValue(minMaxValue);
                while (randomList.Contains(numToAdd))
                    numToAdd = GetValue(minMaxValue);
                randomList.Add(numToAdd);
            }

            return randomList;
        }
    }
}