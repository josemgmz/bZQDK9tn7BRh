using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using NotImplementedException = System.NotImplementedException;

namespace Game.Services.Impl
{
    public class AudioServiceImpl : IAudioService
    {
        #region Variables
        
        private const string AUDIO_CONTAINER = "AudioContainer";
        private const string AUDIO_PATH = "Assets/Content/Sound/";
        private const string MUSIC_PATH = AUDIO_PATH + "Music/";
        private const string SFX_PATH = AUDIO_PATH + "SFX/";
        private const string VOICE_PATH = AUDIO_PATH + "Voice/";
        private const string AUDIO_MIXER_PATH = AUDIO_PATH + "audioMixer.mixer";
        private const string AUDIO_MIXER_GROUP_MUSIC = "Music";
        private const string AUDIO_MIXER_GROUP_SFX = "SFX";
        private const string AUDIO_MIXER_GROUP_VOICE = "Voice";
        private const string AUDIO_MIXER_GROUP_MASTER = "Master";
        private const uint MAX_SFX_SOURCES = 10;
        private const uint MAX_MUSIC_SOURCES = 2;
        private const uint MAX_VOICE_SOURCES = 2;

        private List<AudioSource> _musicSources = new ();
        private List<AudioSource> _sfxSources = new ();
        private List<AudioSource> _voiceSources = new ();
        private GameObject _audioContainer;
        private AudioMixer _audioMixer;
        private GameAddressableContext _addressableContext = new ();

        [Inject] private ILogService _logService;

        #endregion

        #region Lifecycle Methods

        public void Start()
        {
            Initialization();
        }

        #endregion

        #region Play Methods

        public void PlayMusic(AudioClip music, bool loop)
        {
            var freeMusicSource = _musicSources.Find(audioSource => !audioSource.isPlaying);
            if (freeMusicSource == null)
            {
                freeMusicSource = CreateAudioSource(GetAudioMixerGroup(AUDIO_MIXER_GROUP_MUSIC));
                _musicSources.Add(freeMusicSource);
            }

            freeMusicSource.loop = loop;
            freeMusicSource.clip = music;
            freeMusicSource.Play();
        }

        public void PlaySfx(AudioClip sfx, bool loop)
        {
            var freeSfxSource = _sfxSources.Find(audioSource => !audioSource.isPlaying);
            if (freeSfxSource == null)
            {
                freeSfxSource = CreateAudioSource(GetAudioMixerGroup(AUDIO_MIXER_GROUP_SFX));
                _sfxSources.Add(freeSfxSource);
            }

            freeSfxSource.loop = loop;
            freeSfxSource.clip = sfx;
            freeSfxSource.Play();
        }

        public void PlayVoice(AudioClip voice, bool loop)
        {
            var freeVoiceSource = _voiceSources.Find(audioSource => !audioSource.isPlaying);
            if (freeVoiceSource == null)
            {
                freeVoiceSource = CreateAudioSource(GetAudioMixerGroup(AUDIO_MIXER_GROUP_VOICE));
                _voiceSources.Add(freeVoiceSource);
            }

            freeVoiceSource.loop = loop;
            freeVoiceSource.clip = voice;
            freeVoiceSource.Play();
        }

        public async void PlayMusic(string musicName, bool loop)
        {
            var music = await _addressableContext.LoadAssetAsync<AudioClip>($"{MUSIC_PATH}{musicName}");
            PlayMusic(music, loop);
        }

        public async void PlaySfx(string sfxName, bool loop)
        {
            var sfx = await _addressableContext.LoadAssetAsync<AudioClip>($"{SFX_PATH}{sfxName}");
            PlaySfx(sfx, loop);
        }

        public async void PlayVoice(string voiceName, bool loop)
        {
            var voice = await _addressableContext.LoadAssetAsync<AudioClip>($"{VOICE_PATH}{voiceName}");
            PlayVoice(voice, loop);
        }

        #endregion

        #region Stop Methods

        public void StopMusic(AudioClip music)
        {
            var musicSource = _musicSources.Find(audioSource => audioSource.clip == music);
            musicSource?.Stop();
        }

        public void StopSfx(AudioClip sfx)
        {
            var sfxSource = _sfxSources.Find(audioSource => audioSource.clip == sfx);
            sfxSource?.Stop();
        }

        public void StopVoice(AudioClip voice)
        {
            var voiceSource = _voiceSources.Find(audioSource => audioSource.clip == voice);
            voiceSource?.Stop();
        }

        public void StopMusic(string musicName)
        {
            var music = _musicSources.Find(audioSource => audioSource.clip.name == $"{MUSIC_PATH}{musicName}");
            music?.Stop();
        }

        public void StopSfx(string sfxName)
        {
            var sfx = _sfxSources.Find(audioSource => audioSource.clip.name == $"{SFX_PATH}{sfxName}");
            sfx?.Stop();
        }

        public void StopVoice(string voiceName)
        {
            var voice = _voiceSources.Find(audioSource => audioSource.clip.name == $"{VOICE_PATH}{voiceName}");
            voice?.Stop();
        }

        public void StopAllMusic()
        {
            _musicSources.ForEach(audioSource => audioSource.Stop());
        }

        public void StopAllSfx()
        {
            _sfxSources.ForEach(audioSource => audioSource.Stop());
        }

        public void StopAllVoice()
        {
            _voiceSources.ForEach(audioSource => audioSource.Stop());
        }

        public void StopAll()
        {
            StopAllMusic();
            StopAllSfx();
            StopAllVoice();
        }

        #endregion

        #region Private Methods

        private void Initialization()
        {
            // Create audio container
            _audioContainer = new GameObject(AUDIO_CONTAINER)
            {
                hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector
            };
            Object.DontDestroyOnLoad(_audioContainer);

            // Create audio mixer
            _audioMixer = _addressableContext.LoadAsset<AudioMixer>(AUDIO_MIXER_PATH, false);

            // Create audio sources
            for (var i = 0; i < MAX_MUSIC_SOURCES; i++)
            {
                _musicSources.Add(CreateAudioSource(GetAudioMixerGroup(AUDIO_MIXER_GROUP_MUSIC)));
            }

            for (var i = 0; i < MAX_SFX_SOURCES; i++)
            {
                _sfxSources.Add(CreateAudioSource(GetAudioMixerGroup(AUDIO_MIXER_GROUP_SFX)));
            }

            for (var i = 0; i < MAX_VOICE_SOURCES; i++)
            {
                _voiceSources.Add(CreateAudioSource(GetAudioMixerGroup(AUDIO_MIXER_GROUP_VOICE)));
            }

            _logService.Log("AudioService initialized");
        }

        private AudioSource CreateAudioSource(AudioMixerGroup audioMixerGroup)
        {
            var audioSource = _audioContainer.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            return audioSource;
        }

        private AudioMixerGroup GetAudioMixerGroup(string groupName)
        {
            return _audioMixer.FindMatchingGroups(groupName).First();
        }

        #endregion

        #region Configuration Methods

        public async void SetMusicVolume(float volume)
        {
            //Unity 2021 seems to have a bug with _audioMixer the first tick after the game starts is not able to set
            //the volume, so we add a more or less one tick of delay 
            await Task.Yield();
            _audioMixer.SetFloat(AUDIO_MIXER_GROUP_MUSIC, volume);
        }
        
        public async void SetSfxVolume(float volume)
        {
            await Task.Yield();
            _audioMixer.SetFloat(AUDIO_MIXER_GROUP_SFX, volume);
        }
        
        public async void SetVoiceVolume(float volume)
        {
            await Task.Yield();
            _audioMixer.SetFloat(AUDIO_MIXER_GROUP_VOICE, volume);
        }
        
        public async void SetMasterVolume(float volume)
        {
            await Task.Yield();
            _audioMixer.SetFloat(AUDIO_MIXER_GROUP_MASTER, volume);
        }

        #endregion

        public void Stop()
        {
            _addressableContext.Release();
        }
    }
}