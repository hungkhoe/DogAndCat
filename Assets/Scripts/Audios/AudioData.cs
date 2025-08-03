using System.Collections.Generic;
using _Main.Scripts.Definition.Enum;
using UnityEngine;

namespace _Main.Scripts.Definition
{
    public class AudioData
    {
        private struct KeyPath
        {
            public SoundKey key;
            public string path;
        }
        private readonly KeyPath[] keyPaths = new[]
        {
            new KeyPath { key = SoundKey.Music_Home, path = "Audios/Music/Theme Music" },
            new KeyPath { key = SoundKey.Click, path = "Audios/Button click" },
            //new KeyPath { key = SoundKey.Item_Purchase, path = "Audios/item purchase" },
            //new KeyPath { key = SoundKey.Show_Decor, path = "Audios/SFX/UI/show decor" },
            //new KeyPath { key = SoundKey.Show_Popup, path = "Audios/SFX/UI/show popup" },
            //new KeyPath { key = SoundKey.Upgrade, path = "Audios/SFX/UI/upgrade button" },
            //new KeyPath { key = SoundKey.Win, path = "Audios/SFX/ETC/WIN" },
            //new KeyPath { key = SoundKey.Lose, path = "Audios/SFX/ETC/LOSE" },
            //new KeyPath { key = SoundKey.Throw, path = "Audios/SFX/ETC/Animal hit" },
            //new KeyPath { key = SoundKey.Throw_Miss, path = "Audios/SFX/ETC/Animal die" },
           
        };

        private readonly Dictionary<SoundKey, AudioClip> clips;

        public AudioData()
        {
            clips = new Dictionary<SoundKey, AudioClip>();
            
            foreach (var entry in keyPaths)
            {
                var request = Resources.LoadAsync<AudioClip>(entry.path);
                request.completed += operation =>
                {
                    if (request.asset != null)
                    {
                        clips.TryAdd(entry.key, (AudioClip)request.asset);
                    }
                };
            }
        }

        public AudioClip GetClip(SoundKey key)
        {
            if (clips.ContainsKey(key))
            {
                return clips[key];
            }

            Debug.LogError($"Can't find by key \"{key.ToString()}\"");
            return null;
        }
    }
}