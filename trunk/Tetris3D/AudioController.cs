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

        public AudioController(Microsoft.Xna.Framework.Game game) : base(game)
        {
            this.audioEngine = new AudioEngine("Content\\Audio\\TetrisAudio.xgs");
            this.waveBank = new WaveBank(this.audioEngine, "Content\\Audio\\TetrisWaveBank.xwb");
            this.soundBank = new SoundBank(this.audioEngine, "Content\\Audio\\TetrisSoundBank.xsb");
            this.cue = this.soundBank.GetCue("BackgroundCue");
        }

        public override void Update(GameTime gameTime)
        {
            this.audioEngine.Update();
            base.Update(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            this.audioEngine.Dispose();
            this.waveBank.Dispose();
            this.soundBank.Dispose();
            this.cue.Dispose();
            base.Dispose(disposing);
        }

        public void Play()
        {
            this.cue = this.soundBank.GetCue(this.cue.Name);

            this.cue.Play();
        }

        public void Stop()
        {
            this.cue.Stop(AudioStopOptions.Immediate);
        }

        public void Pause()
        {
            this.cue.Pause();
        }

        public void Resume()
        {
            this.cue.Resume();
        }
    }
}
