using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Vamps.Utility
{
    class AudioManager : GameComponent
    {
        #region Fields
        #region Singleton

        static AudioManager audioManager = null;
        public static AudioManager Instance
        {
            get { return audioManager; }
        }
        public static bool IsInitialized
        {
            get { return audioManager.isInitialized; }
        }
        #endregion
        #region Audio Data
        SoundEffectInstance musicSound;
        Dictionary<string, SoundEffectInstance> soundBank;
        string[,] soundNames;
        #endregion
        bool isInitialized;
        #endregion

        #region Initialization
        private AudioManager(Game game)
            : base(game) { }

        public static void Initialize(Game game)
        {
            audioManager = new AudioManager(game);
            game.Components.Add(audioManager);
        }

        #endregion

        #region Loading Methods
        /// <summary>
        /// Loads sounds and organizes for future use
        /// </summary>
        public static void LoadSounds()
        {
            string soundLocation = "Sounds/";
            audioManager.soundNames = new string[,]
            {
                //add sounds here
                {"titlemusic", "titlemusic"}
            };
            audioManager.soundBank = new Dictionary<string, SoundEffectInstance>();
            for (int i = 0; i < audioManager.soundNames.GetLength(0); i++)
            {
                SoundEffect se = audioManager.Game.Content.Load<SoundEffect>(soundLocation + audioManager.soundNames[i, 0]);
                audioManager.soundBank.Add(audioManager.soundNames[i, 1], se.CreateInstance());


            }
            audioManager.isInitialized = true;

        }
        #endregion

        #region sound Methods
        /// <summary>
        /// Indexer. return a sound instance by name
        /// </summary>
        public SoundEffectInstance this[string soundName]
        {
            get
            {
                if (audioManager.soundBank.ContainsKey(soundName))
                    return audioManager.soundBank[soundName];
                else
                    return null;
            }
        }

        /// <summary>
        /// plays a sound by name
        /// </summary>
        /// <param name ="soundName">the name of the sound to play</param>
        public static void PlaySound(string soundName)
        {
            //if the sound exists start it
            if (audioManager.soundBank.ContainsKey(soundName))
                audioManager.soundBank[soundName].Play();
        }
        public static void PlaySound(string soundName, bool isLooped)
        {
            if (audioManager.soundBank.ContainsKey(soundName))
            {
                if (audioManager.soundBank[soundName].IsLooped != isLooped)
                    audioManager.soundBank[soundName].IsLooped = isLooped;
                audioManager.soundBank[soundName].Play();
            }
        }
        ///<summary>
        /// stos all currently playing music
        /// </summary>
        public static void StopSounds()
        {
            foreach (var sound in audioManager.soundBank.Values)
            {
                if (sound.State != SoundState.Stopped)
                    sound.Stop();
            }

        }

        //checks if sounds are playing or not
        public static bool AreSoundsPlaying()
        {
            foreach (var sound in audioManager.soundBank.Values)
            {
                if (sound.State == SoundState.Playing)
                {
                    return true;
                }
            }
            return false;
        }

        // pauses or resume all sounds
        
        public static void PauseResumeSounds(bool resumeSounds)
        {
            SoundState state = resumeSounds ? SoundState.Paused : SoundState.Playing;

            foreach (var sound in audioManager.soundBank.Values)
            {
                if (sound.State == state)
                {
                    if (resumeSounds)
                    {
                        sound.Resume();
                    }
                    else
                    {
                        sound.Pause();
                    }
                }
            }

        }

        //play music by sound name
        public static void PlayMusic(string musicSoundName)
        {
            //stop the old music sound
            if (audioManager.musicSound != null)
                audioManager.musicSound.Stop(true);
            //if the music sound exists
            if (audioManager.soundBank.ContainsKey(musicSoundName))
            {
                //get the instance and start it
                audioManager.musicSound = audioManager.soundBank[musicSoundName];
                if (!audioManager.musicSound.IsLooped)
                    audioManager.musicSound.IsLooped = true;
                audioManager.musicSound.Play();
            }
        }
        #endregion
        #region instance disposal methods
        //cleans up component when disposing
        protected override void Dispose(bool disposing)
        {
            try
            {   
                if (disposing)
                {
                    foreach (var item in soundBank)
                    {
                        item.Value.Dispose();
                    }
                    soundBank.Clear();
                    soundBank = null;
                }
            
            }
            finally
            {
                base.Dispose(disposing);
            
            }
            
        }
        #endregion
    }
}
