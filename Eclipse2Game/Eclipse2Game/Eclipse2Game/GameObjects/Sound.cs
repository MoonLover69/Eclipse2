using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse2Game.GameObjects
{
    /// <summary>
    /// Used to store and play a sound effect
    /// </summary>
    class Sound
    {
        private string _assetName;
        private SoundEffectInstance _effect;

        /// <summary>
        /// Create a new sound with the given asset name
        /// </summary>
        /// <param name="assetName"></param>
        public Sound(string assetName)
        {
            _assetName = assetName;
        }

        public void LoadContent(ContentManager cm)
        {
            _effect = cm.Load<SoundEffect>(_assetName).CreateInstance();
        }

        public void Play()
        {
            _effect.IsLooped = false;
            _effect.Play();
        }

        public void PlayRepeated()
        {
            _effect.IsLooped = true;
            _effect.Play();
        }

        public void Stop()
        {
            _effect.Stop();
        }

        public bool IsPlaying
        {
            get
            {
                return _effect.State == SoundState.Playing;
            }
        }

        public override string ToString()
        {
            return _assetName;
        }
    }
}
