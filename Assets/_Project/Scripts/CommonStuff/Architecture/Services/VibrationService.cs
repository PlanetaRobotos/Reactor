// using System;
// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using MoreMountains.NiceVibrations;
// using UnityEngine;
//
// namespace Assets._Project.Scripts.Managers
//  {
//     public class VibrationService : MonoBehaviour
//     {
//         private Coroutine _vibration;
//         private bool _isActive = true;
//
//         public void StartVibrate()
//          {
//             if (!_isActive) return;
//             _vibration ??= StartCoroutine(CountiniousVibration());
//          }
//
//         public void StopVibrate()
//         {
//            if (_vibration != null)
//                 StopCoroutine(_vibration);
//            _vibration = null;
//         }
//
//        private IEnumerator CountiniousVibration()
//        {
//              while (true)
//             {
//                MMNVAndroid.AndroidVibrate(200, 1);
//                 yield return new WaitForSeconds(0.2f);
//             }
//        }
//
//        public void Vibrate(int milliseconds = 40)
//        {
//            if (!_isActive) return;
//            // Debug.Log("VibrationManager: vibrate");
//            MMNVAndroid.AndroidVibrate(milliseconds, 1);
//        }
//
//        public void VibrateMany()
//         {
//              DOVirtual.DelayedCall(0f, () =>
//             {
//                 Debug.Log("VibrationManager: vibrate many");
//                 try
//                 {
//                     MMNVAndroid.AndroidVibrate(
//                         new long[] { 344, 160, 272, 176, 303, 161, 292, 156, 328, 136, 105, 123, 88, 120, 72, 160 },
//                        new int[] { 0, 1, 0, 2, 0, 3, 0, 4, 0, 3, 0, 2, 0, 1, 0, 1 },
//                         -1);
//                 }
//                 catch (Exception e)
//                  {
//                     Debug.LogError(e);
//               }
//             });
//         }
//
//         public void SetActive(bool active)
//         {
//              MMVibrationManager.SetHapticsActive(active);
//             _isActive = active;
//             if (!active) StopVibrate();
//        }
//
// #if UNITY_EDITOR
//
//          private List<Record> _pattern;
//          private bool recording;
//
//         private void Update()
//         {
//              if (Input.GetKeyUp(KeyCode.Space))
//              {
//                 recording = !recording;
//                if (recording)
//                {
//                     Debug.Log("VibrationManager: Recording pattern");
//                     _pattern = new List<Record>();
//                      _pattern.Add(new Record(GetLongTime(), RecordEvent.START));
//                 }
//                 else
//                {
//                     Debug.Log("VibrationManager: pattern recorded");
//                     _pattern.Add(new Record(GetLongTime(), RecordEvent.END));
//                    PrintPattern();
//                 }
//             }
//
//            if (recording)
//            {
//               if (Input.GetMouseButtonDown(1))
//                 {
//                     _pattern.Add(new Record(GetLongTime(), RecordEvent.DOWN));
//                 }
//
//                 if (Input.GetMouseButtonUp(1))
//                {
//                   _pattern.Add(new Record(GetLongTime(), RecordEvent.UP));
//                }
//            }
//         }
//
//         private long GetLongTime()
//         {
//            return (long)(Time.time * 1000);
//         }
//
//         private void PrintPattern()
//         {
//             string intervalPattern = "";
//             string amplitudePattern = "";
//             long previous = 0;
//             foreach (var s in _pattern)
//             {
//                 if (s.ev == RecordEvent.START) previous = s.time;
//                  if (s.ev == RecordEvent.UP)
//                  {
//                    intervalPattern += $"{(s.time - previous)}, ";
//                      amplitudePattern += "1, ";
//                 }
//
//                 if (s.ev == RecordEvent.DOWN)
//                 {
//                     intervalPattern += $"{(s.time - previous)}, ";
//                     amplitudePattern += "0, ";
//                 }
//
//                  previous = s.time;
//              }
//
//             intervalPattern = "new long[]{" + intervalPattern.Substring(0, intervalPattern.Length - 2) + "},";
//             amplitudePattern = "new int[]{" + amplitudePattern.Substring(0, amplitudePattern.Length - 2) + "},";
//
//              Debug.Log($"Patterns: \n {intervalPattern} \n {amplitudePattern}");
//          }
// #endif
//
//          private struct Record
//         {
//             public long time;
//              public RecordEvent ev;
//
//              public Record(long time, RecordEvent ev)
//              {
//                  this.time = time;
//                  this.ev = ev;
//              }
//         }
//
//         private enum RecordEvent
//         {
//              START = 0,
//              DOWN = 1,
//              UP = 2,
//              END = 3
//          }
//
//         private void OnEnable()
//          {
//             MMVibrationManager.SetHapticsActive(true);
//         }
//      }
//  }