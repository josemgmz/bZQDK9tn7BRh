using Game.Services.Data;
using Game.Services.Lifetime;
using UnityEngine;

namespace Game.Services
{
    public interface IAudioService : IStartableService, IStoppableService
    {
        #region Methods
        void PlayMusic(AudioData.Music music, bool loop = false);
        void PlaySfx(AudioData.Sfx sfx, bool loop = false);
        void PlayVoice(AudioData.Voice voice, bool loop = false);
        void PlayMusic(AudioClip music, bool loop = false);
        void PlaySfx(AudioClip sfx, bool loop = false);
        void PlayVoice(AudioClip voice, bool loop = false);
        void PlayMusic(string musicName, bool loop = false);
        void PlaySfx(string sfxName, bool loop = false);
        void PlayVoice(string voiceName, bool loop = false);
        void StopMusic(AudioClip music);
        void StopSfx(AudioClip sfx);
        void StopVoice(AudioClip voice);
        void StopMusic(string musicName);
        void StopSfx(string sfxName);
        void StopVoice(string voiceName);
        void StopAllMusic();
        void StopAllSfx();
        void StopAllVoice();
        void StopAll();
        public void SetMusicVolume(float volume);
        public void SetSfxVolume(float volume);
        public void SetVoiceVolume(float volume);

        #endregion
    }
}