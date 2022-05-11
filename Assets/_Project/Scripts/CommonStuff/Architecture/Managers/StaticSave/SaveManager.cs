using System.Globalization;
using UnityEngine;

namespace _Core.Scripts.Managers.Save
{
    public enum SaveKey
    {
        None,
        [Header("Cleared")] SoundActivation,
        SearchedCard,

        [Header("Uncleared")] Default,
        LastQuitGameTime,
        OpenedTiles
    }

    public static class SaveManager
    {
        private static readonly ISave saver = new XmlSave();

        // private static void Setup()
        // {
        //     // saver = new XmlSave();
        //     saver.Load();
        // }

        // private static bool IsSaverNull => saver == null;

        public static void SetInt(SaveKey key, int value, bool isCleared = true) =>
            saver.SetValue(key.ToString(), value.ToString(), isCleared);

        public static int GetInt(SaveKey key, int defaultValue = 0)
        {
            saver.Load();
            return saver.HasKey(key.ToString()) ? saver.GetInt(key.ToString()) : defaultValue;
        }

        public static void SetFloat(SaveKey key, float value) =>
            saver.SetValue(key.ToString(), value.ToString(CultureInfo.InvariantCulture));

        public static float GetFloat(SaveKey key, float defaultValue = 0.0f)
        {
            saver.Load();
            return saver.HasKey(key.ToString()) ? saver.GetFloat(key.ToString()) : defaultValue;
        }

        public static void SetString(SaveKey key, string value) => saver.SetValue(key.ToString(), value);

        public static string GetString(SaveKey key, string defaultValue = "")
        {
            saver.Load();
            return saver.HasKey(key.ToString()) ? saver.GetString(key.ToString()) : defaultValue;
        }

        public static void SetBool(SaveKey key, bool value) => saver.SetValue(key.ToString(), value.ToString());

        public static bool GetBool(SaveKey key, bool defaultValue = false)
        {
            saver.Load();
            return saver.HasKey(key.ToString()) ? saver.GetBool(key.ToString()) : defaultValue;
        }

        public static void DeleteSave() => saver.DeleteSave();

        // public static void SetInt64(ClearedSaveKey key, long value) => saver.SetValue(key.ToString(), value.ToString());
        //
        // public static long GetInt64(ClearedSaveKey key, int defaultValue = 0) =>
        //     saver.HasKey(key.ToString()) ? saver.GetInt64(key.ToString()) : defaultValue;

        // public static void SetDouble(ClearedSaveKey key, double value) =>
        //     saver.SetValue(key.ToString(), value.ToString(CultureInfo.InvariantCulture));
        //
        // public static double GetDouble(ClearedSaveKey key, double defaultValue = 0.0f) =>
        //     saver.HasKey(key.ToString()) ? saver.GetDouble(key.ToString()) : defaultValue;

        // public static void Save() => saver.Save();
        // public static void Load() => saver.Load();
        // public static void ClearHashTable() => saver.ClearHashTable();
    }
}