using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Core.Scripts.Managers.Save;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts.Utilities
{
    public static class CommonTools
    {
        public static bool IsPointerOverUIObject()
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Any(result => result.gameObject.layer == 5);
        }

        /*public static bool IsPointerOverUIObjectIgnoreJoystick()
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return !results.Where(result => result.gameObject.layer == 5)
                .Any(res => res.gameObject.TryGetComponent(out Joystick _));
        }*/

        public static void SaveCurrentTime() =>
            SaveManager.SetString(SaveKey.LastQuitGameTime, DateTime.Now.ToString(CultureInfo.InvariantCulture));

        public static DateTime GetLastQuitGameTime =>
            DateTime.Parse(SaveManager.GetString(SaveKey.LastQuitGameTime));

        public static double GetQuitedGameSeconds() =>
            (DateTime.Now - GetLastQuitGameTime).TotalSeconds;

        public static int BoolToInt(bool val) => val ? 1 : 0;

        public static bool IntToBool(int val) => val != 0;

        public static int GetRandomIndex(int maxValue) => UnityEngine.Random.Range(0, maxValue);

        public static int[] GetRandomIndexes(int maxValue, int amount)
        {
            var rand = new System.Random();
            List<int> listNumbers = new List<int>();
            listNumbers.AddRange(Enumerable.Range(0, maxValue)
                .OrderBy(i => rand.Next()).Take(amount));
            return listNumbers.ToArray();
        }

        public static bool IsOnlyOneTouch()
        {
#if UNITY_EDITOR
            return true;
#else
        return Input.touchCount == 1;
#endif
        }

        public static void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public static void CountdownInt(TMP_Text textTMP, int timerStart, int timerEnd, float duration) =>
            DOVirtual.Float(timerStart, timerEnd, duration, value => textTMP.text = ((int) value).ToString());

        public static GameObject GetObjectByEnumName(Type enumType, string enumElement, Func<GameObject> func)
        {
            if (Enum.GetNames(enumType).Any(name => name == enumElement))
            {
                return func?.Invoke();
            }

            throw new ArgumentOutOfRangeException(nameof(enumElement), enumElement, "Resource is out of range");
        }

        public static void EnumNamesLoopAction(Type enumType, Action<string> action)
        {
            foreach (var name in Enum.GetNames(enumType))
                action.Invoke(name);
        }

        public static void DictionaryLoopAction(Dictionary<string, GameObject> dictionary, Action<string> action)
        {
            foreach (var name in dictionary.Keys)
                action.Invoke(name);
        }

        // public static T[] GetAllEnumElements<T>(Type enumType)
        // {
        //     return Enum.GetValues()
        // }
        public static void TimerActionLooped(ref float currentValue, float maxValue, Action action)
        {
            if (currentValue >= maxValue)
            {
                action?.Invoke();
                currentValue = 0f;
            }

            currentValue += Time.fixedDeltaTime;
        }

        public static void TimerActionOnce(ref float currentValue, float maxValue, Action action)
        {
            if (currentValue >= maxValue) 
                action?.Invoke();
            else 
                currentValue += Time.fixedDeltaTime;
        }

        public static () GetScreenValues()
        {
            Camera camera = Camera.main;
            if (camera is { })
            {
                float height = camera.orthographicSize * 2.0f;
                float width = height * camera.aspect;
            
                return new Vector2(height, width);
            }

            throw new Exception("Camera isn't find");
        }
    }

    internal enum ScreenValue
    {
        Width,
        Height
    }
}