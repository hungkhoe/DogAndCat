using System.Collections.Generic;
using UnityEngine;

namespace _BaseFeatures.Audios.Pooler
{
    public enum AudioKey
    {
        MUSIC,
        SOUND_FX
    }
    public static class AudioPooler
    {
        public readonly struct AudioPool
        {
            private readonly AudioKey audioKey;
            private readonly Stack<Audio> inactive;

            public AudioPool(AudioKey key)
            {
                audioKey = key;
                inactive = new Stack<Audio>();
            }

            public Audio Spawn(AudioClip clip, bool loop = false, float pitch = 1)
            {
                Audio _audio;
                if (inactive.Count == 0)
                {
                    AudioSource _source = AudioManager.Holder.AddComponent<AudioSource>();
                    _audio = new Audio().SetPooler(this).SetAudioSource(_source);
                    switch (audioKey)
                    {
                        case AudioKey.MUSIC:
                            _audio.SetOutput(AudioManager.audioMixer.FindMatchingGroups(AudioManager.MUSIC)[0]);
                            break;
                        case AudioKey.SOUND_FX:
                            _audio.SetOutput(AudioManager.audioMixer.FindMatchingGroups(AudioManager.SFX)[0]);
                            break;
                    }
                }
                else
                {
                    _audio = inactive.Pop();
                }
                _audio.SetClip(clip, loop, pitch);
                return _audio;
            }

            public void Despawn(Audio audio)
            {
                audio.audioSource.enabled = false;
                inactive.Push(audio);
            }
        }
        private static readonly Dictionary<AudioKey, AudioPool> pools = new Dictionary<AudioKey, AudioPool>();
        private static void Init(AudioKey key)
        {
            if (!pools.ContainsKey(key))
                pools.Add(key, new AudioPool(key));
        }
        public static Audio Spawn(AudioKey key, AudioClip clip)
        {
            Init(key);
            return pools[key].Spawn(clip);
        }
        public static Audio Spawn(AudioKey key, AudioClip clip, bool loop, float pitch = 1)
        {
            Init(key);
            return pools[key].Spawn(clip, loop, pitch);
        }
        public static Audio Spawn(AudioKey key, AudioClip clip, float pitch)
        {
            Init(key);
            return pools[key].Spawn(clip, false, pitch);
        }
        public static void Despawn(Audio audio)
        {
            if (audio.audioSource.enabled)
            {
                audio.myPool.Despawn(audio);
            }
        }
    }
}