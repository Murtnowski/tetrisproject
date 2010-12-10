using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
    public class AudioController : GameComponent
    {
        protected AudioEngine audioEngine;
        protected WaveBank waveBank;
        protected SoundBank soundBank;
        protected Cue cue;
        protected float volume;

        public float Volume
        {
            get
            {
                return this.volume;
            }
            set
            {
                this.volume = value;
                this.cue.SetVariable("Volume", this.volume);
            }
        }
        protected bool isInitalized;

        public AudioController(Microsoft.Xna.Framework.Game game) : base(game)
        {
            try
            {
                this.audioEngine = new AudioEngine("Content\\Audio\\TetrisAudio.xgs");
                this.waveBank = new WaveBank(this.audioEngine, "Content\\Audio\\TetrisWaveBank.xwb");
                this.soundBank = new SoundBank(this.audioEngine, "Content\\Audio\\TetrisSoundBank.xsb");
                this.cue = this.soundBank.GetCue("BackgroundCue");
                this.isInitalized = true;
            }
            catch (InvalidOperationException)
            {
                //If the machine has no audio device this exception occurs
                this.audioEngine = null;
                this.waveBank = null;
                this.soundBank = null;
                this.cue = null;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.isInitalized)
            {
                this.audioEngine.Update();
                base.Update(gameTime);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.isInitalized)
            {
                this.audioEngine.Dispose();
                this.waveBank.Dispose();
                this.soundBank.Dispose();
                this.cue.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Play()
        {
            if (this.isInitalized)
            {
                this.cue = this.soundBank.GetCue(this.cue.Name);

                this.cue.Play();
            }
        }

        public void Stop()
        {
            if (this.isInitalized)
            {
                this.cue.Stop(AudioStopOptions.Immediate);
            }
        }

        public void Pause()
        {
            if (this.isInitalized)
            {
                this.cue.Pause();
            }
        }

        public void Resume()
        {
            if (this.isInitalized)
            {
                this.cue.Resume();
            }
        }
    }
}
