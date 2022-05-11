using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets._Project.Scripts.Audio
{
    public class Sound
    {
        public AudioSource audioSource;
        public AudioClipId clipId;
    }

    public class AudioService : MonoBehaviour
    {
        [SerializeField] private List<AudioClipEntity> clips;
        [SerializeField] private AudioClip _background;
        
        [Range(0f, 1f)]
        [SerializeField] private float _backgroundVolume = 1f;
        private AudioSource _musicSource;
        private List<Sound> _soundsSource = new List<Sound>();
        private Coroutine _coroutine;

        private AudioClipId[] _audioClipsAttack = { AudioClipId.close_attack_1, AudioClipId.close_attack_2, AudioClipId.close_attack_3 };
        private bool _mute;
        
        void Start()
        {
            SceneManager.sceneLoaded += OnStartGame;
            // OnPlayMusic();
        }

        private void OnPlayMusic()
        {
            _musicSource =  gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.clip = _background;
            _musicSource.volume = _backgroundVolume;
            _musicSource.mute = _mute;
            _musicSource.Play();
        }

        public void UpdateVolumeSounds()
        {
            _musicSource.mute = _mute;
            _soundsSource.ForEach(s => s.audioSource.mute = _mute);
        }

        public void OnPlayRandomSound()
        {
            var i = Random.Range(0, _audioClipsAttack.Length);
            var clipId = _audioClipsAttack[i];
            OnPlaySound((int) clipId);
        }
        
        public void OnPlaySound(int clipId)
        {
            var index = clips.FindIndex(a => (int) a.Id == clipId);
            bool isAdd = false;
            AudioSource audioSource = FindAudioSource();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                isAdd = true;
            }
            audioSource.volume = clips[index].Volume;
            audioSource.clip = clips[index].TargetAudioClip;
            audioSource.mute = _mute;
            audioSource.Play();
            Sound sound = new Sound();
            sound.audioSource = audioSource;
            sound.clipId = (AudioClipId) clipId;
            if(isAdd)
                _soundsSource.Add(sound);
            StartCoroutine(DeletedClip(audioSource, clips[index].TargetAudioClip.length));
        }
        
        private IEnumerator DeletedClip(AudioSource audioSource, float time)
        {
            yield return new WaitForSeconds(time);
            audioSource.clip = null;
        }

        private AudioSource FindAudioSource()
        {
            AudioSource audio = null;
            foreach (var source in _soundsSource)
            {
                if (source.audioSource.clip == null)
                {
                    audio = source.audioSource;
                }
            }
            return audio;
        }
        
        private void OnStartGame(Scene scene, LoadSceneMode mode)
        {
           for (int i = 0; i < _soundsSource.Count; i++)
                 _soundsSource.Remove(_soundsSource[i]);
        }
    }
}
