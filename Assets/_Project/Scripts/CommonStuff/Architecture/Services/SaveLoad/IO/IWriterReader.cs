namespace _Project.Scripts.Architecture.Services.SaveLoadService.IO
{
    public interface IWriterReader
    {
        bool IsExist(string path);
        T Read<T>(string path);
        void Write(string path, object data);
    }
}