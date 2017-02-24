using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VkNetGine;

namespace Hangbot
{
    public class CommunicationChannel : Hanggame.IChannel {
        //*****************************************************IChannel implementation**********************

        public void WriteLine(string s = "\n") {
            Input_Buffer = (s + "\n");
        }
        public string Read() {

            waiter.WaitOne();
            string text = Output_Buffer;
            Output_Buffer = "";
            return text;
        }
        //*************************************************************************************************
        private EventWaitHandle waiter;
        private EventWaitHandle wait_for_bot_writes;

        private string _input_buffer;
        private string _output_buffer;
        private ClockTower _tower;
        private BufferClockTower _buffer_tower;
        //*************************************************************************************************
        //                         Event sending
        public delegate void InputBufferEventHandler(object source, EventArgs args);

        public event InputBufferEventHandler InputIsReady;




        //*************************************************************************************************

        public CommunicationChannel(ClockTower _tower) {
            waiter = new EventWaitHandle(false, EventResetMode.AutoReset);
            this._tower = _tower;
            _tower.Chime += (msg) => {
                Output_Buffer = msg.Text;
                waiter.Set();
            };
        }

        public CommunicationChannel(ClockTower _tower, BufferClockTower _bf) : this(_tower) {
            this._buffer_tower = _bf;
            
        }

        public string Input_Buffer {
            get {
                string temp = _input_buffer;
                _input_buffer = "";
                return temp;
            }

            set {
                _input_buffer = value;
                Console.WriteLine("Inside CHANNEL CLASS");

                OnInputIsReady();
                //_buffer_tower.ChimeBufferIncome(value);
            }
        }

        public string Output_Buffer {
            get {
                return _output_buffer;
            }

            set {
                _output_buffer = value;
                waiter.Set();
            }
        }

        /// <summary>
        /// Notifies subscribers that Message is ready
        /// </summary>
        protected virtual void OnInputIsReady() {
            if(InputIsReady != null) {
                InputIsReady(this, EventArgs.Empty);
            }
        }
    }
}
