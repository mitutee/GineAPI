using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VkNetGine;
using Hanggame;

namespace Hangbot
{
    public class CommunicationChannel : Hanggame.IChannel {
        //*****************************************************IChannel implementation**********************

        public void WriteLine(string s = "\n") {
            Output_Buffer = (s + "\n");
        }
        public string Read() {

            waiter.WaitOne();
            string text = Input_Buffer;
            Input_Buffer = "";
            return text;
        }
        //*************************************************************************************************
        private EventWaitHandle waiter;
        private EventWaitHandle wait_for_bot_writes;

        private string _output_buffer;
        private string _input_buffer;
        private string _player;
        private Hanggame.Hanggame _game;


        //*************************************************************************************************
        //                         Event sending
        public delegate void OutputBufferEventHandler(CommunicationChannel source, EventArgs args);

        public event OutputBufferEventHandler OutputIsReady;


        /// <summary>
        /// Notifies subscribers that Message is ready
        /// </summary>
        protected virtual void OnOutputIsReady() {
            if (OutputIsReady != null) {
                OutputIsReady(this, EventArgs.Empty);
            }
        }

        //*************************************************************************************************

        private CommunicationChannel() {
            waiter = new EventWaitHandle(false, EventResetMode.AutoReset);
            Game = new Hanggame.Hanggame(this);

        }

        public CommunicationChannel(string target) : this() {

            this.Player = target;
        }

        /// <summary>
        /// To read from
        /// </summary>
        public string Output_Buffer {
            get {
                string temp = _output_buffer;
                _output_buffer = "";
                return temp;
            }

            set {
                _output_buffer = value;
                Console.WriteLine("Game hase written something..");

                OnOutputIsReady();
                //_buffer_tower.ChimeBufferIncome(value);
            }
        }

        /// <summary>
        /// To write there 
        /// </summary>
        public string Input_Buffer {
            get {
                return _input_buffer;
            }

            set {
                _input_buffer = value; // _output_buffer
                waiter.Set();
            }
        }

        
        public string Player {
            get {
                return _player;
            }

            set {
                _player = value;
            }
        }

        public Hanggame.Hanggame Game {
            get {
                return _game;
            }

            set {
                _game = value;
            }
        }


    }
}
