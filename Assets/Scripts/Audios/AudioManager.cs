using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using _BaseFeatures.Audios.Pooler;
using Random = UnityEngine.Random;

namespace _BaseFeatures.Audios
{
    public class AudioManager : MonoBehaviour
    {
        public const string MUSIC = "Music";
        public const string SFX = "SFX";

        private static AudioManager instance = null;
        private GameObject audioHolder;
        public AudioMixer m_AudioMixer;
        private Audio m_MusicTheme;

        public static AudioManager Instance => instance;
        public static GameObject Holder => instance.audioHolder;
        public static AudioMixer audioMixer => instance.m_AudioMixer;
        public Audio musicTheme => m_MusicTheme;

        /// <summary>
        /// Event check on update - Despawn audio on done.
        /// </summary>
        public event UnityAction AudioUpdate;

        public static event UnityAction OnCompleted;
        
        public static void Initialize()
        {
           new GameObject("AudioManager", typeof(AudioManager));
        }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            audioHolder = new GameObject("AudioHolder");
            audioHolder.transform.SetParent(instance.transform);
            LoadAudioMixer();
        }
        private void LoadAudioMixer()
        {
            var request = Resources.LoadAsync<AudioMixer>("AudioMixer");
            request.completed += onLoadDone;

            void onLoadDone(AsyncOperation operation)
            {
                m_AudioMixer = request.asset as AudioMixer;
                OnCompleted?.Invoke();
                request = null;
            }
        }

        private void Update() => AudioUpdate?.Invoke();
        public void PlayMusic(AudioClip clip, float volume = 0.5f)
        {
            if (m_MusicTheme == null)
                m_MusicTheme = AudioPooler.Spawn(AudioKey.MUSIC, clip, true).FadeIn(volume);
            else
                OnChangeMusic(clip, volume);
        }

        public void OnChangeMusic(AudioClip clip, float volume = 0.5f) => m_MusicTheme.FadeOut(() =>
            m_MusicTheme = AudioPooler.Spawn(AudioKey.MUSIC, clip, true).FadeIn(volume), volume);
        public Audio PlaySound(AudioClip clip, float volume = 1) => AudioPooler.Spawn(AudioKey.SOUND_FX, clip).Play(volume);
        public Audio PlaySound(AudioClip clip, float volume, float pitch = 1) => AudioPooler.Spawn(AudioKey.SOUND_FX, clip, pitch).Play(volume);
        public Audio PlaySound(AudioClip clip, float volume, float lowPitch, float highPitch)
        {
            var  pitch = Random.Range((int)(lowPitch * 100), (int)(highPitch * 100)) / 100f;
            return AudioPooler.Spawn(AudioKey.SOUND_FX, clip, pitch).Play(volume);
        }
        public Audio PlaySoundAtPosition(AudioClip clip, float volume = 1, float playbackPosition = 0) => AudioPooler.Spawn(AudioKey.SOUND_FX, clip).PlayAtPosition(volume, playbackPosition);

        #region [_______________AUDIO VOLUME METHODS_______________]

        private const string MASTER_VOL = "MasterVolume";
        private const string MUSIC_VOL = "MusicVolume";
        private const string SFX_VOL = "SFXVolume";
        private void masterVolumeChanged(float value) => m_AudioMixer.SetFloat(MASTER_VOL, value);
        private void musicVolumeChanged(float value) => m_AudioMixer.SetFloat(MUSIC_VOL, value);
        private void SFXVolumeChanged(float value) => m_AudioMixer.SetFloat(SFX_VOL, value);
        public void MasterOff() => masterVolumeChanged(-80);
        public void MasterOn() => masterVolumeChanged(0);
        public void MusicOff() => musicVolumeChanged(-80);
        public void MusicOn() => musicVolumeChanged(0);
        public void SoundOff() => SFXVolumeChanged(-80);
        public void SoundOn() => SFXVolumeChanged(0);

        #endregion
    }

}