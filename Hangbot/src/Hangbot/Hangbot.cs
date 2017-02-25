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
            games = new Dictionary<string, CommunicationChannel>();





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
                start_new_game:
                CommunicationChannel new_channel = new CommunicationChannel(msg.Target);
                new_channel.OutputIsReady += OnOutputIsReady;
                games.Add(msg.Target,new_channel);
            }
            else {
                string answer = defaultMsg();
                api.SendMessage(new Message(msg.Target, answer));
            }


        }

        private string defaultMsg() {
            return defaultMsgs[new Random().Next(0, defaultMsgs.Count - 1)];
        }

        private List<string> defaultMsgs = new List<string>() {
            "Хочешь поиграть в виселицу? 😎(напиши 'да', к примеру)",
            "Привет! Можем сыграть с тобой в 'Виселицу', если хочешь 😊",
            "😜 Давай играть в 'Виселицу!' Хочешь?",

        };

        private string FuckingDeserealizationOfQuotesAndSlashesKostyl(string v) {
            string na_vyhod = "";
            for (int i = 0; i < v.Length; i++) {
                if (v[i] != '\\' && v[i] != '"') na_vyhod += v[i];
            }
            return na_vyhod;
        }
        private bool WantsStartTheGame(string text) {
            text = FuckingDeserealizationOfQuotesAndSlashesKostyl(text.ToLower());
            return text == "y" || text == "yes" || text == "да" || text == "\"y\"" ;
        }

        private List<string> answersToInitializeTheGame = new List<string>() {
            "y",
            "yes",
            "da",
            "go",

            "да",
            "давай",
            "д",
            "ок",
            "го",

        };
    }




}
