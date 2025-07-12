using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CarInsurance
{
    public class ChatUser
    {
        public long ChatId { get; set; }
        private IUserState UserState { get; set; }
        public List<string> MessageHistory { get; set; } = new List<string>();
        private int messageCount = 0;

        //public string UserName { get; set; }

        public ChatUser(long chatId)
        {
            ChatId = chatId;
            UserState = new StartState();
        }

        public async Task SendMessageAsync(string message)
        {
            Console.WriteLine(message);
            await BotService.TaskSendMessage(ChatId, message);
        }

        public async Task<string> SendMessageAIAsync(string message)
        {
            return await BotService.AskAIAsync(ChatId, message, Prompt());
        }

        public void SaveMessage(string Message)
        {
            messageCount++;
            MessageHistory.Add($"{messageCount} History: " + Message);

            //keeps only last 6 messages
            if (MessageHistory.Count > 6)
            {
                MessageHistory.RemoveAt(0); 
            }
        }

        public string History() {
            return string.Join("\n", MessageHistory);
        }

        public string Prompt()
        {
            return History() + UserState.Prompt ;
        }

        public string GetLastMessage()
        {
            return MessageHistory.Last();
        }

        public bool NotStart()
        {
            return (UserState.Prompt == null);
        }

        public bool WaitingDataConfirmation()
        {
            return UserState.DataConfirmation;
        }

        public void NextStep()
        {
            UserState = UserState.Next();
        }

        public void PreviousStep()
        {
            UserState = UserState.Previous();
        }
    }
}
