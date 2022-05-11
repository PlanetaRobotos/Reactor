namespace _Core.Scripts.Managers.Save
{
    public interface ISave
    {
        void Save();
        void Load();
        void DeleteSave();
        void ClearHashTable();
        bool HasKey(string key);
        void SetValue(string key, string value, bool isCleared = true);
        int GetInt(string key);
        long GetInt64(string key);
        string GetString(string key);
        bool GetBool(string key);
        double GetDouble(string key);
        float GetFloat(string key);
    }
}