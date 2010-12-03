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
        private SoundEffect rotatePieceSound, slamPieceSound, pauseScreenSound, pauseScreenVocalSound, menuClickSound,
            gameIntroSound, forwardMenuSound, backMenuSound, pauseToResumeSound, playBeginSound, playTetrisSound;
        private SoundEffectInstance soundInstance;

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
            playTetrisSound = Content.Load<SoundEffect>(@"Audio\SFX\VocalOhYeah");
            playBeginSound = Content.Load<SoundEffect>(@"Audio\SFX\Begin");

            // All music must be in loop
            MediaPlayer.IsRepeating = true;
        }
        public void PlayTetrisSound()
        {
            soundInstance = playTetrisSound.CreateInstance();
            soundInstance.Pitch = .1f;
            soundInstance.Play();
        }
        public void PlayBeginSound()
        {
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
