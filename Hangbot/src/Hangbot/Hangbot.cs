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

        // -----   -----

        private API api;

        private ClockTower _tower; //Subsribing for events there
        private Dictionary<string, CommunicationChannel> games;




        public void SendCustomMessage(string to, string what)
        {
            api.SendMessage(new Message(to, what));
        }

        public Hangbot(string token) {
            _tower = new ClockTower(); // initializing notification about new messages at all

            api = new API(token, _tower);

            ///Subscribe for the event of the new Message
            _tower.Chime += (msg) => {
                HandleIncomingMessage(msg);
                Console.WriteLine("NEW MESSAGE! HANDLING...");
            };





        }


        #region TUTORIAL
        public void OnOutputIsReady(CommunicationChannel source, EventArgs e) {
            Console.WriteLine("Sending some message from the game)+++");
            api.SendMessage(new Message(source.Player, source.Output_Buffer));
        } 
        #endregion

        private void HandleIncomingMessage(Message msg)
        {
            Console.WriteLine("New message");
            /// Game is already running;
            /// Keep playing;
            if (games.ContainsKey(msg.Target)) {
                Console.WriteLine("We are playing. Sending data to game input");
                games[msg.Target].Input_Buffer = msg.Text;
            }
            else if(WantsStartTheGame(msg.Text)){
                /// Starting the new game

                CommunicationChannel new_channel = new CommunicationChannel(msg.Target);
                new_channel.OutputIsReady += OnOutputIsReady;
                games.Add(msg.Target,new_channel);
            }
            else {
                string answer = msg.Text;
                api.SendMessage(new Message(msg.Target, answer));
            }


        }

        private bool WantsStartTheGame(string text) {
            text = text.ToLower();
            return text == "y" || text == "yes" || text == "да";
        }
    }




}
