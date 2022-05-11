using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Architecture.Services.SaveLoadService
{
    public static class CustomEditorMenuItem
    {
        [MenuItem("Data/Open data folder &o")]
        private static void OpenDataFolder()
        {
            Debug.Log($"{Application.persistentDataPath}Open folder: ");
            if (!Directory.Exists(Application.persistentDataPath)) return;

            EditorUtility.RevealInFinder(Application.persistentDataPath);

#if UNITY_EDITOR_WIN 
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                Arguments = $"/C start {Application.persistentDataPath}",
                FileName = "explorer.exe"
            };
#endif  

#if UNITY_EDITOR_OSX
            System.Diagnostics.Process.Start(Application.persistentDataPath);
#endif

        }

        [MenuItem("Data/Clear all data &;")]
        private static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();

            if (Directory.Exists(Application.persistentDataPath))
            {
                string[] files = Directory.GetFiles(Application.persistentDataPath);

                foreach (var file in files)
                {
                    File.Delete(file);
                    Debug.Log($"File '{file}' has been deleted.");
                }
            }
        }
    }
}