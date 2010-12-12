using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
    //AudioController is used to control the MUSIC played in the background of the game.  Music is randomized and placed into a playlist that 
    //loops repeatedly unless paused by te user (which is done in various ways).  AudioController was implemented over MediaPlayer because
    //MediaPlayer is flawed and slows the game on newer operating systems.  AudioController seems to have no problems with performance.  
    //It could be extended to ALSO play sound effects however we already have a working AudioBank with no performance hiccups so we find it 
    //unneccessary to extend the reach of the audioController for that
    public class AudioController : GameComponent
    {
        protected AudioEngine audioEngine;
        //the bank of wave files 
        protected WaveBank waveBank;
        //the bank of sound files
        protected SoundBank soundBank;
        //the cue used for running the AudioController
        protected Cue cue;

        //the setter and getter for the volume of the music
        public float Volume
        {
            get
            {
                return this.cue.GetVariable("Volume");
            }
            set
            {
                this.cue.SetVariable("Volume", value);
            }
        }
        //a variable which defines if a user has audio hardware installed on their computer.  This is used to ensure a user without audio hardware will not attempt to play sounds (which causes a crash)
        protected bool isInitalized;

        public AudioController(Microsoft.Xna.Framework.Game game) : base(game)
        {
            try //try initiating the audio.  If it fails, no audio hardware is on the users computer
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
        //the update method used for the audiocontroller
        public override void Update(GameTime gameTime)
        {
            if (this.isInitalized)
            {
                this.audioEngine.Update();
                base.Update(gameTime);
            }
        }
        //Disposing the audiocontroller frees up the resources used by it as it closes itself
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
        //a method used to play the music in the cue
        public void Play()
        {
            if (this.isInitalized)
            {
                float volume = this.Volume;
                this.cue = this.soundBank.GetCue(this.cue.Name);
                this.Volume = volume;

                this.cue.Play();
            }
        }
        //Stop is only used if the music isn't going to start again.  It acts as more of a reset for the audiocontroller in the event that it will Play() again
        public void Stop()
        {
            if (this.isInitalized)
            {
                this.cue.Stop(AudioStopOptions.Immediate);
            }
        }
        //should be the primary function used to stop the music.  Pause saves key data and allows the music to play again without much effort.
        public void Pause()
        {
            if (this.isInitalized)
            {
                this.cue.Pause();
            }
        }
        //Resume is typically used after a Pause() to resume the music where it was left off.
        public void Resume()
        {
            if (this.isInitalized)
            {
                this.cue.Resume();
            }
        }
    }
}
