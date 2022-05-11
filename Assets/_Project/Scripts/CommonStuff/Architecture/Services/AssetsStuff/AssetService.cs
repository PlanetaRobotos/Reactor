using System.Linq;
using _Project.Scripts.Architecture.Services;
using _Project.Scripts.Architecture.Services.UIStuff;
using _Project.Scripts.UIStuff;
using Assets._Project.Scripts.Utilities.Constants;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class AssetService
    {
        public GameObject GetObjectByName(string path) => 
            Resources.Load<GameObject>(path);   
        
        public T GetObjectByName<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
        
        public T GetObjectByType<T>(string path) where T : Object
        {
            Object[] objects = Resources.LoadAll<Object>(path);
            return (T) objects.FirstOrDefault(o => o.GetType() == typeof(T));
        }
        
        public GameObject[] GetObjectsByType(string path)
        {
            GameObject[] objects = Resources.LoadAll<GameObject>(path);
            // return (GameObject[]) objects.Where(o => o.GetType() == typeof(T));
            return objects;
        }

        public GameObject GetObjectByIndex(string path, string body, int index)
        {
            string pathFull = $"{path}{body}_{index.ToString()}";
            return Resources.Load<GameObject>(pathFull);
        }
        
        public T GetScriptableObjectByIndex<T>(string path, string body, int index) where T : ScriptableObject
        {
            string pathFull = $"{path}/{body}_{index.ToString()}";
            return Resources.Load<T>(pathFull);
        }

        public T Load<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }

        public WindowBase LoadWindow(WindowType windowType)
        {
            WindowBase[] windows = Resources.LoadAll<WindowBase>(AssetPath.WindowsPath);
            return windows.First(window => window.GetWindowType() == windowType);
        }
    }
    
    public class ResourcesKey
    {
        public string Key { get; }
        private ResourcesKey(string key) => Key = key;
        public static implicit operator ResourcesKey(string key) => new ResourcesKey(key);
    }
}