/*
 * Project: Tetris Project
 * Primary Author: Damon Chastain
 * Authors: Matthew Urtnowski & Damon Chastain
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Tetris3D
{
    public class AudioBank
    {
        //declare names for various sound effects
        private SoundEffect rotatePieceSound, slamPieceSound, pauseScreenSound, pauseScreenVocalSound, menuClickSound,
            gameIntroSound, forwardMenuSound, backMenuSound, pauseToResumeSound, playBeginSound, playTetrisSound, playVocalBegin,
            gameOverSound, secondGameOverSound, secondTetrisSound;
        //create a sound instance which plays the various sounds with alterations, if needed
        private SoundEffectInstance soundInstance, secondSoundInstance;
        private float soundEffectsVolume;

        protected bool isInitalized;

        //load each effect from Audio\SFX
        public void LoadContent(ContentManager Content)
        {
            try
            {
                rotatePieceSound = Content.Load<SoundEffect>(@"Audio\SFX\scratchloud");
                slamPieceSound = Content.Load<SoundEffect>(@"Audio\SFX\slam");
                pauseScreenSound = Content.Load<SoundEffect>(@"Audio\SFX\PauseSound");
                pauseScreenVocalSound = Content.Load<SoundEffect>(@"Audio\SFX\VocalPaused");
                menuClickSound = Content.Load<SoundEffect>(@"Audio\SFX\click");
                gameIntroSound = Content.Load<SoundEffect>(@"Audio\SFX\ShipEngine");
                forwardMenuSound = Content.Load<SoundEffect>(@"Audio\SFX\Forward");
                backMenuSound = Content.Load<SoundEffect>(@"Audio\SFX\Backward");
                pauseToResumeSound = Content.Load<SoundEffect>(@"Audio\SFX\VocalResumed");
                playTetrisSound = Content.Load<SoundEffect>(@"Audio\SFX\Explosion");
                playBeginSound = Content.Load<SoundEffect>(@"Audio\SFX\Begin");
                gameOverSound = Content.Load<SoundEffect>(@"Audio\SFX\VocalSystemOverload");
                secondGameOverSound = Content.Load<SoundEffect>(@"Audio\SFX\VocalGameOver");
                secondTetrisSound = Content.Load<SoundEffect>(@"Audio\SFX\VocalOhYeah");
                playVocalBegin = Content.Load<SoundEffect>(@"Audio\SFX\VocalBegin");

                soundEffectsVolume = .5f;
                this.isInitalized = true;
            }
            catch (NoAudioHardwareException)
            {
            }
        }
        //sound of player getting a Tetris
        public int GetAllSoundEffectsVolume()
        {
            return (int)(soundEffectsVolume*10);
        }

        public void SetAllSoundEffectsVolume(int number)
        {
            switch (number)
            {
                case 0: soundEffectsVolume = 0f; break;
                case 1: soundEffectsVolume = .1f; break;
                case 2: soundEffectsVolume = .2f; break;
                case 3: soundEffectsVolume = .3f; break;
                case 4: soundEffectsVolume = .4f; break;
                case 5: soundEffectsVolume = .5f; break;
                case 6: soundEffectsVolume = .6f; break;
                case 7: soundEffectsVolume = .7f; break;
                default: soundEffectsVolume = .8f; break;
                case 9: soundEffectsVolume = .9f; break;
                case 10: soundEffectsVolume = 1f; break;
            }

        }

        public void PlaySecondGameOverSound()
        {
            if (this.isInitalized)
            {
                secondGameOverSound.Play();
            }
        }
        public void PlayGameOverSound(float pitch)
        {
            if (this.isInitalized) //plays game over sound with adjusted pitch
            {
                soundInstance = gameOverSound.CreateInstance();
                soundInstance.Pitch = pitch;
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
                if (pitch == 0f)//plays game over sound with normal pitch
                {
                    secondSoundInstance = gameIntroSound.CreateInstance();
                    secondSoundInstance.Volume = soundEffectsVolume;
                    secondSoundInstance.Play();
                }
            }
        }
        public void PlayTetrisSound()
        {
            if (this.isInitalized)
            {
                soundInstance = playTetrisSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();

                secondSoundInstance = secondTetrisSound.CreateInstance();
                secondSoundInstance.Volume = soundEffectsVolume;
                secondSoundInstance.Play();
            }
        }
        public void PlayBeginSound(bool sayBegin)
        {
            if (this.isInitalized)
            {
                if (sayBegin == true)
                {
                    soundInstance = playVocalBegin.CreateInstance();
                    soundInstance.Volume = soundEffectsVolume;
                    soundInstance.Play();
                }
                secondSoundInstance = playBeginSound.CreateInstance();
                secondSoundInstance.Volume = soundEffectsVolume;
                secondSoundInstance.Play();
            }
        }

        public void PlayResumedSound()
        {
            if (this.isInitalized)
            {
                soundInstance = pauseToResumeSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }
        public void PlayMenuForwardSound()
        {
            if (this.isInitalized)
            {
                soundInstance = forwardMenuSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }
        public void PlayMenuBackwardSound()
        {
            if (this.isInitalized)
            {
                soundInstance = backMenuSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }
        public void PlayIntroSound()
        {
            if (this.isInitalized)
            {
                soundInstance = gameIntroSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }
        public void PlayMenuScrollSound()
        {
            if (this.isInitalized)
            {
                soundInstance = menuClickSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }
        public void PlayPauseSound()
        {
            if (this.isInitalized)
            {
                soundInstance = pauseScreenSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();

                secondSoundInstance = pauseScreenVocalSound.CreateInstance();
                secondSoundInstance.Volume = soundEffectsVolume;
                secondSoundInstance.Play();
            }
        }
        public void PlayRotateSound()
        {
            if (this.isInitalized)
            {
                soundInstance = rotatePieceSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }

        public void PlaySlamSound()
        {
            if (this.isInitalized)
            {
                soundInstance = slamPieceSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play();
            }
        }

        public void PlayClearLineSound()
        {
            if (this.isInitalized)
            {
                soundInstance = forwardMenuSound.CreateInstance();
                soundInstance.Volume = soundEffectsVolume;
                soundInstance.Play(); //also used in forwardMenu.  The sound suits clearing a line
            }
        }
    }
}
