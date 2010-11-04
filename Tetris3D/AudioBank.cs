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
        private SoundEffect rotatePieceSound, slamPieceSound, clearLineSound;
        private SoundEffectInstance soundInstance;

        public void LoadContent(ContentManager Content)
        {
            rotatePieceSound = Content.Load<SoundEffect>(@"Audio\scratchloud");
            slamPieceSound = Content.Load<SoundEffect>(@"Audio\hit");
            clearLineSound = Content.Load<SoundEffect>(@"Audio\slam");
            // All music must be in loop
            MediaPlayer.IsRepeating = true;
        }

        public void PlayRotateSound()
        {
            soundInstance = rotatePieceSound.CreateInstance();
            soundInstance.Volume = .4f;
            soundInstance.Play();
        }

        public void PlaySlamSound()
        {
            soundInstance = slamPieceSound.CreateInstance();
            soundInstance.Volume = .5f;
            soundInstance.Play();
        }

        public void PlayClearLineSound()
        {
            clearLineSound.Play();
        }
    }
}
