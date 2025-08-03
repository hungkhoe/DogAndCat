using _BaseFeatures.Audios.Pooler;
using UnityEngine;

namespace _BaseFeatures.Audios
{
    public class AudioUtilities
    {
        public static void PlayMusic(AudioClip clip, float volume = 0.5f) => AudioManager.Instance.PlayMusic(clip, volume);
        public static void ChangeMusic(AudioClip clip, float volume = 0.5f) => AudioManager.Instance.OnChangeMusic(clip, volume);
        public static Audio PlaySound(AudioClip clip, float volume = 1) => AudioManager.Instance.PlaySound(clip, volume);
        public static Audio PlaySound(AudioClip clip, float volume, float pitch) => AudioManager.Instance.PlaySound(clip, volume, pitch);
        public static Audio PlaySound(AudioClip clip, float volume, float lowPitch, float highPitch) => AudioManager.Instance.PlaySound(clip, volume, lowPitch, highPitch);
        public static Audio PlaySoundAtPosition(AudioClip clip, float volume = 1, float playbackPosition = 0) => AudioManager.Instance.PlaySoundAtPosition(clip, volume, playbackPosition);
        public static void MasterVolumeOn() => AudioManager.Instance.MasterOn();
        public static void MasterVolumeOff() => AudioManager.Instance.MasterOff();
        public static void MusicVolumeOn() => AudioManager.Instance.MusicOn();
        public static void MusicVolumeOff() => AudioManager.Instance.MusicOff();
        public static void SoundVolumeOn() => AudioManager.Instance.SoundOn();
        public static void SoundVolumeOff() => AudioManager.Instance.SoundOff();
    }
}