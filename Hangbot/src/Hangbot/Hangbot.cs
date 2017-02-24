using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNetGine;
using Hanggame;

namespace Hangbot
{


    public class Hangbot
    {
        //---- Better architecture for message sending ---
        private Messager _messager;

        public Messager messager {
            get {
                return _messager;
            }

            set {
                _messager = value;
            }
        }
        // -----   -----

        private API api;

        private BufferClockTower _buffer_tower;
        private ClockTower _tower; //Subsribing for events there
        private Hanggame.Hanggame game;
        private CommunicationChannel game_channel;



        public void SendCustomMessage(string to, string what)
        {
            api.SendMessage(new Message(to, what));
        }

        public Hangbot(string token) {
            _tower = new ClockTower();
            _buffer_tower = new BufferClockTower();
            api = new API(token, _tower);

            messager = new Messager(api);

            //_buffer_tower.TimeToSendMessageToTheUser += (_buffer_message) => {
            //    SendCustomMessage("80314023", _buffer_message);
            //};
            _tower.Chime += (msg) => {
                HandleIncomingMessage(msg);
                Console.WriteLine("NEW MESSAGE! HANDLING...");
            };
            game_channel = new CommunicationChannel(_tower, _buffer_tower);

            game_channel.InputIsReady += OnInputIsReady;

            game = new Hanggame.Hanggame(game_channel);
            game.PlayGame();



        }


        #region TUTORIAL
        public void OnInputIsReady(object source, EventArgs e) {
            Console.WriteLine("Inside BOT CLASS");
            SendCustomMessage("80314023", game_channel.Input_Buffer);
        } 
        #endregion

        private void HandleIncomingMessage(Message msg)
        {
            game_channel.Output_Buffer += msg.Text;
            string answer = "John";
            //api.SendMessage(new Message(msg.Target, answer));
        }


        
    }
    public delegate void HandleBuffer(string _from_game);
     
    public class BufferClockTower {

        public event HandleBuffer TimeToSendMessageToTheUser;

        public void ChimeBufferIncome(string income) {
            TimeToSendMessageToTheUser(income);
        }
    }



}
