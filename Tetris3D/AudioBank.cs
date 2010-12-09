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
        private SoundEffectInstance soundInstance;

        //load each effect from Audio\SFX
        public void LoadContent(ContentManager Content)
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
            // All music must be in loop
        }
        //sound of player getting a Tetris
        public void PlaySecondGameOverSound()
        {
            secondGameOverSound.Play();
        }
        public void PlayGameOverSound(float pitch)
        {
            soundInstance = gameOverSound.CreateInstance();
            soundInstance.Pitch = pitch;
            soundInstance.Volume = 1f;
            soundInstance.Play();
            if (pitch == 0f)
                gameIntroSound.Play();

        }
        public void PlayTetrisSound()
        {
            soundInstance = playTetrisSound.CreateInstance();
            soundInstance.Volume = .6f;
            soundInstance.Play();
            secondTetrisSound.Play();
        }
        public void PlayBeginSound(bool sayBegin)
        {
            if (sayBegin == true)
            {
                soundInstance = playVocalBegin.CreateInstance();
                soundInstance.Volume = .5f;
                soundInstance.Play();
            }
            playBeginSound.Play();
        }

        public void PlayResumedSound()
        {
            pauseToResumeSound.Play();
        }
        public void PlayMenuForwardSound()
        {
            forwardMenuSound.Play();
        }
        public void PlayMenuBackwardSound()
        {
            backMenuSound.Play();
        }
        public void PlayIntroSound()
        {
            soundInstance = gameIntroSound.CreateInstance();
            soundInstance.Volume = 1f;
            soundInstance.Play();
        }
        public void PlayMenuScrollSound()
        {
            soundInstance = menuClickSound.CreateInstance();
            soundInstance.Volume = .9f;
            soundInstance.Play();
        }
        public void PlayPauseSound()
        {
            soundInstance = pauseScreenSound.CreateInstance();
            soundInstance.Volume = .4f;
            soundInstance.Play();
            pauseScreenVocalSound.Play();
        }
        public void PlayRotateSound()
        {
            soundInstance = rotatePieceSound.CreateInstance();
            soundInstance.Volume = .4f;
            soundInstance.Play();
        }

        public void PlaySlamSound()
        {
            slamPieceSound.Play();
        }

        public void PlayClearLineSound()
        {
            forwardMenuSound.Play(); //also used in forwardMenu.  The sound suits clearing a line
        }
    }
}
