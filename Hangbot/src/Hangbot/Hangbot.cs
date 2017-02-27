using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNetGine;
using Hanggame;
using System.Net.Http;
using System.IO;

namespace Hangbot {


    public class Hangbot {
        //---- Cloud architecture for word loading ---

        // -----   -----

        private API api;

        private ClockTower _tower; //Subsribing for events there
        private Dictionary<string, CommunicationChannel> games;




        public void SendCustomMessage(string to, string what) {
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


        public void ReCheck() {
            api.HandleUnreadDialogs();
        }

        #region TUTORIAL
        public void OnOutputIsReady(CommunicationChannel source, EventArgs e) {
            Console.WriteLine("Sending some message from the game)+++");
            api.SendMessage(new Message(source.Player, source.Output_Buffer));
        }
        #endregion

        private void HandleIncomingMessage(Message msg) {
            // reseting the game



            /// Game is already running;
            /// Keep playing;
            if (games.ContainsKey(msg.Target)) {

                if (games[msg.Target].IsDead) {
                    games.Remove(msg.Target);
                    goto start_new_game;
                }
                Console.WriteLine("We are playing. Sending data to game input");
                games[msg.Target].Input_Buffer = msg.Text;
                return;
            }
            start_new_game:
            if (WantsStartTheGame(msg.Text)) {
                /// Starting the new game

                CommunicationChannel new_channel = new CommunicationChannel(msg.Target);
                new_channel.OutputIsReady += OnOutputIsReady;

                games.Add(msg.Target, new_channel);
            }
            else if (DontWantsStartTheGame(msg.Text)) {
                // Dispose
                games.Remove(msg.Target);

                api.SendMessage(new Message(msg.Target, a_for_n_a()));
            }
            else {
                string answer = defaultMsg();
                api.SendMessage(new Message(msg.Target, answer));
                
            }


        }

        private string defaultMsg() {
            return defaultMsgs[new Random().Next(0, defaultMsgs.Count - 1)];
        }

        private string a_for_n_a() {
            return answersForNegativeAnswers[new Random().Next(0, answersForNegativeAnswers.Count - 1)];
        }

        private string FuckingDeserealizationOfQuotesAndSlashesKostyl(string v) {
            string na_vyhod = "";
            for (int i = 0; i < v.Length; i++) {
                if (v[i] != '\\' && v[i] != '"') na_vyhod += v[i];
            }
            return na_vyhod;
        }
        private bool WantsStartTheGame(string text) {
            text = FuckingDeserealizationOfQuotesAndSlashesKostyl(text.ToLower());
            return answersToInitializeTheGame.Contains(text);
        }

        private bool DontWantsStartTheGame(string text) {
            text = FuckingDeserealizationOfQuotesAndSlashesKostyl(text.ToLower());
            return negativeAnswers.Contains(text);
        }


        #region Answers For Negative Answers
        private List<string> answersForNegativeAnswers = new List<string>() {
            "Ну что же...Так уж и быть!",
            "Понятно😰",
            "Когда захочешь поиграть - просто напиши мне!",
        };

        #endregion

        #region NegativeAnswers
        private List<string> negativeAnswers = new List<string>() {
            "n",
            "no",
            "net",
            "ne",
            "н",
            "нет",
            "не",
        };
        #endregion

        #region PositiveAnswers
        private List<string> answersToInitializeTheGame = new List<string>() {
            "y",
            "yes",
            "da",
            "go",

            "оккей",
            "может быть",
            "дат",
            "да",
            "давай",
            "д",
            "ок",
            "го",
            ".начать",

            "ебаш",
            "ну можно",
            "давай сыграем",
            "хорошо",
            "я не против",
            "валяй",
            "действуй",
            "было бы неплохо",
            "конечно",
            "хочу",
        };
        #endregion

        #region Default Answers
        private List<string> defaultMsgs = new List<string>() {
            "Хочешь поиграть в виселицу? 😎(напиши 'да', к примеру)",
            "Привет! Можем сыграть с тобой в 'Виселицу', если хочешь 😊",
            "😜 Давай играть в 'Виселицу!' Хочешь?",
            "Мне скучно, может сыграем в висельника?",
            "Просто скажи мне да, и игра начнется!",
            "Хочешь ли ты в игру?"

        };

        #endregion    }




    }
}
