using Game.Services.Data;
using Game.Services.Lifetime;
using UnityEngine;

namespace Game.Services
{
    /// <summary>
    /// Provides audio-related functionalities such as playing and stopping music, sound effects, and voice clips.
    /// </summary>
    public interface IAudioService : IStartableService, IStoppableService
    {
        #region Methods
        /// <summary>
        /// Plays the specified music.
        /// </summary>
        /// <param name="music">The music to play.</param>
        /// <param name="loop">Whether the music should loop.</param>
        void PlayMusic(AudioData.Music music, bool loop = false);

        /// <summary>
        /// Plays the specified sound effect.
        /// </summary>
        /// <param name="sfx">The sound effect to play.</param>
        /// <param name="loop">Whether the sound effect should loop.</param>
        void PlaySfx(AudioData.Sfx sfx, bool loop = false);

        /// <summary>
        /// Plays the specified voice clip.
        /// </summary>
        /// <param name="voice">The voice clip to play.</param>
        /// <param name="loop">Whether the voice clip should loop.</param>
        void PlayVoice(AudioData.Voice voice, bool loop = false);

        /// <summary>
        /// Plays the specified music clip.
        /// </summary>
        /// <param name="music">The music clip to play.</param>
        /// <param name="loop">Whether the music should loop.</param>
        void PlayMusic(AudioClip music, bool loop = false);

        /// <summary>
        /// Plays the specified sound effect clip.
        /// </summary>
        /// <param name="sfx">The sound effect clip to play.</param>
        /// <param name="loop">Whether the sound effect should loop.</param>
        void PlaySfx(AudioClip sfx, bool loop = false);

        /// <summary>
        /// Plays the specified voice clip.
        /// </summary>
        /// <param name="voice">The voice clip to play.</param>
        /// <param name="loop">Whether the voice clip should loop.</param>
        void PlayVoice(AudioClip voice, bool loop = false);

        /// <summary>
        /// Plays the specified music by name.
        /// </summary>
        /// <param name="musicName">The name of the music to play.</param>
        /// <param name="loop">Whether the music should loop.</param>
        void PlayMusic(string musicName, bool loop = false);

        /// <summary>
        /// Plays the specified sound effect by name.
        /// </summary>
        /// <param name="sfxName">The name of the sound effect to play.</param>
        /// <param name="loop">Whether the sound effect should loop.</param>
        void PlaySfx(string sfxName, bool loop = false);

        /// <summary>
        /// Plays the specified voice clip by name.
        /// </summary>
        /// <param name="voiceName">The name of the voice clip to play.</param>
        /// <param name="loop">Whether the voice clip should loop.</param>
        void PlayVoice(string voiceName, bool loop = false);

        /// <summary>
        /// Stops the specified music clip.
        /// </summary>
        /// <param name="music">The music clip to stop.</param>
        void StopMusic(AudioClip music);

        /// <summary>
        /// Stops the specified sound effect clip.
        /// </summary>
        /// <param name="sfx">The sound effect clip to stop.</param>
        void StopSfx(AudioClip sfx);

        /// <summary>
        /// Stops the specified voice clip.
        /// </summary>
        /// <param name="voice">The voice clip to stop.</param>
        void StopVoice(AudioClip voice);

        /// <summary>
        /// Stops the specified music by name.
        /// </summary>
        /// <param name="musicName">The name of the music to stop.</param>
        void StopMusic(string musicName);

        /// <summary>
        /// Stops the specified sound effect by name.
        /// </summary>
        /// <param name="sfxName">The name of the sound effect to stop.</param>
        void StopSfx(string sfxName);

        /// <summary>
        /// Stops the specified voice clip by name.
        /// </summary>
        /// <param name="voiceName">The name of the voice clip to stop.</param>
        void StopVoice(string voiceName);

        /// <summary>
        /// Stops all music currently playing.
        /// </summary>
        void StopAllMusic();

        /// <summary>
        /// Stops all sound effects currently playing.
        /// </summary>
        void StopAllSfx();

        /// <summary>
        /// Stops all voice clips currently playing.
        /// </summary>
        void StopAllVoice();

        /// <summary>
        /// Stops all audio currently playing.
        /// </summary>
        void StopAll();

        /// <summary>
        /// Sets the volume for music.
        /// </summary>
        /// <param name="volume">The volume level to set.</param>
        void SetMusicVolume(float volume);

        /// <summary>
        /// Sets the volume for sound effects.
        /// </summary>
        /// <param name="volume">The volume level to set.</param>
        void SetSfxVolume(float volume);

        /// <summary>
        /// Sets the volume for voice clips.
        /// </summary>
        /// <param name="volume">The volume level to set.</param>
        void SetVoiceVolume(float volume);

        #endregion
    }
}