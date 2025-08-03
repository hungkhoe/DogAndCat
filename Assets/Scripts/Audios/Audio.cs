using System;
using UnityEngine;
using UnityEngine.Events;
using static _BaseFeatures.Audios.Pooler.AudioPooler;
using UnityEngine.Audio;

namespace _BaseFeatures.Audios.Pooler
{
    public class Audio
    {
        public AudioPool myPool;
        public AudioSource audioSource;
        private UnityAction timeOutCallback;
        private AudioTween m_Tween;
        public bool IsPlaying { get => audioSource.isPlaying; }
        public Audio SetPooler(AudioPool pool)
        {
            myPool = pool;
            return this;
        }
        public Audio SetAudioSource(AudioSource audioSource)
        {
            this.audioSource = audioSource;
            return this;
        }
        public Audio SetClip(AudioClip clip, bool loop, float pitch)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.pitch = pitch;
            return this;
        }
        public Audio SetOutput(AudioMixerGroup mixerGroup)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
            return this;
        }
        public Audio Play(float volume = 1)
        {
            audioSource.volume = volume;
            audioSource.time = 0;
            audioSource.enabled = true;
            audioSource.Play();
            if (!audioSource.loop)
                AudioManager.Instance.AudioUpdate += onUpdatePerFrame;
            return this;
        }
        public Audio PlayAtPosition(float volume = 1, float playbackPosition = 0)
        {
            audioSource.volume = volume;
            audioSource.time = playbackPosition;
            audioSource.enabled = true;
            audioSource.Play();
            if (!audioSource.loop)
                AudioManager.Instance.AudioUpdate += onUpdatePerFrame;
            return this;
        }
        public void Stop()
        {
            audioSource.Stop();
            Despawn(this);
        }
        public void Stop(float time)
        {
            m_Tween = new AudioTween(0, 1, time, null, Stop);
        }
        public float GetPlaybackPosition() => audioSource.time;
        public Audio FadeIn(float volume = 1)
        {
            audioSource.volume = 0;
            audioSource.time = 0;
            audioSource.enabled = true;
            audioSource.Play();
            if (!audioSource.loop)
                AudioManager.Instance.AudioUpdate += onUpdatePerFrame;

            m_Tween = new AudioTween(0, volume, 1, value => audioSource.volume = value, null);
            return this;
        }
        public Audio FadeOut(UnityAction complete, float volume = 1)
        {
            m_Tween = new AudioTween(0, volume, 1, value => audioSource.volume = value, () =>
            {
                Stop();
                complete.Invoke();
            });
            return this;
        }
        public void ChangeVolume(float volumeTarget)
        {
            m_Tween = new AudioTween(audioSource.volume, volumeTarget, 1, value => audioSource.volume = value, null);
        }
        private void onUpdatePerFrame()
        {
            if (!audioSource.isPlaying)
            {
                AudioManager.Instance.AudioUpdate -= onUpdatePerFrame;
                Despawn(this);
                timeOutCallback?.Invoke();
                m_Tween?.Dispose();
                m_Tween = null;
            }
        }

        public Audio SetTimeOutCallback(UnityAction callback)
        {
            this.timeOutCallback = callback;
            return this;
        }

        private class AudioTween : IDisposable
        {
            private float m_StartValue;
            private readonly float m_EndValue;
            private readonly float m_Delta;
            private readonly float m_Duration;
            private readonly UnityAction<float> m_UpdateCallback;
            private readonly UnityAction m_OnComplete;

            ~AudioTween()
            {
                Dispose();
            }

            public AudioTween(float startValue, float endValue, float duration, UnityAction<float> updateCallback, UnityAction onComplete)
            {
                m_StartValue = 0;
                m_EndValue = endValue;
                m_Delta = endValue - startValue;
                m_UpdateCallback = updateCallback;
                m_OnComplete = onComplete;
                m_Duration = duration > 0 ? duration : 1;
                AudioManager.Instance.AudioUpdate += onUpdatePerFrame;
            }

            private void onUpdatePerFrame()
            {
                if (m_StartValue < m_EndValue)
                {
                    m_StartValue += Time.deltaTime / m_Duration * m_Delta;
                    m_UpdateCallback?.Invoke(m_StartValue);
                }
                else
                {
                    AudioManager.Instance.AudioUpdate -= onUpdatePerFrame;
                    m_OnComplete?.Invoke();
                }
            }

            public void Dispose()
            {
                AudioManager.Instance.AudioUpdate -= onUpdatePerFrame;
            }
        }
    }
}
