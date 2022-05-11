using System;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace _Project.Scripts.Utilities.Tools
{
    public class UITools
    {
        private static readonly string[] names = {"", "K", "M", "B", "T"};

        public static string FormatAmount(float digit)
        {
            int n = 0;
            while (n + 1 < names.Length && digit >= 1000)
            {
                digit /= 1000;
                n++;
            }

            return $"{Math.Round(digit, 1)}{names[n]}";
        }

        public static void UpdateText(TMP_Text tmpText, string message) => tmpText.text = message;

        public static void UpdateText(TMP_Text tmpText, int message, bool withRound = true)
        {
            tmpText.text = withRound ? $"{FormatAmount(message)}" : $"{message.ToString()}";
        }

        public static Tween CountdownTimer(TMP_Text textTMP, int timerStart, int timerEnd, float duration,
            Action action = null)
        {
            Tween tween = DOVirtual.Float(timerStart, timerEnd, duration,
                value => textTMP.text = ((int) value).ToString()).SetEase(Ease.Linear);
            tween.onComplete += () => action?.Invoke();
            return tween;
        }

        public static Tween CountdownProgress(Image timerProgressImage, float fillingDuration, Action action = null)
        {
            Tween tween = timerProgressImage.DOFillAmount(0f, fillingDuration).SetEase(Ease.Linear);
            tween.onComplete += () => action?.Invoke();
            return tween;
        }
    }
}