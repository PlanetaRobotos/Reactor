using System;
using Assets._Project.Scripts.Utilities.Extensions;

namespace _Project.Scripts.Mechanics
{
    public static class EnumTools
    {
        public static int GetLength(Type type) => Enum.GetValues(type).Length;
        public static T GetRandom<T>(Type type) => ((T[]) Enum.GetValues(type)).GetRandomElement();
    }
}