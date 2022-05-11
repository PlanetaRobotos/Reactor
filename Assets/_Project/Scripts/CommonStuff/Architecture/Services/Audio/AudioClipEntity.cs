using System;
using UnityEngine;

namespace Assets._Project.Scripts.Audio
{
    [Serializable]
    public class AudioClipEntity 
    {
        [SerializeField]
        private AudioClipId id;
        [SerializeField]
        private AudioClip targetAudioClip;
        [SerializeField]
        private float volume = 1;

        public AudioClipId Id
        {
            get { return id; }
        }

        public AudioClip TargetAudioClip
        {
            get { return targetAudioClip; }
        }

        public float Volume
        {
            get { return volume; }
        }
    }
}
