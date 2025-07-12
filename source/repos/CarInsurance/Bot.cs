using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;


namespace CarInsurance
{
    public class BotService
    {
        private readonly TelegramBotClient _botclient;
        private static BotService _instance = null!;
        private OpenAIServices _ai;
        private MindeeServices _mindee;

        public BotService(string Apitoken)
        {
            _botclient = new TelegramBotClient(Apitoken);
            _instance = this;
            _ai = new OpenAIServices();
            _mindee = new MindeeServices();
        }

        public async Task Start()
        {

            _botclient.StartReceiving(UpdateAsync, ErrorAsync);

            Console.WriteLine("Bot Started");

        }

        private async Task ErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            long chatId = update.Message.Chat.Id;
            string message = update.Message.Text;
            var photo = update.Message.Photo;
            bool userResponce;

            Console.WriteLine(message == null || photo == null);

            if (message == null && photo == null)
                return;

            Console.WriteLine($"{update.Message.From.FirstName}: {message}");

            var user = ChatUserFactory.GetOrCreate(update.Message.Chat.Id);
            user.SaveMessage(message);

            if (user.WaitingDataConfirmation())
            {
                if(message == "Yes")
                    userResponce = true;
                else if (message == "No")
                {
                    user.PreviousStep();
                }
                else
                {
                    var AIResponce = await user.SendMessageAIAsync(message);
                    await user.SendMessageAsync(AIResponce);
                    user.SaveMessage("AI:" + AIResponce);
                    return;
                }

            }

            if (message == "/start")
            {
                await user.SendMessageAsync(Constants.start);
                if (user.NotStart())
                    user.NextStep();
                return;
            }

            Console.WriteLine(user.NotStart());

            if (user.NotStart())
            {
                await user.SendMessageAsync("Send /start to start");
                return;
            }

            Console.WriteLine(photo == null);
            if (photo is not null)
            {
                var filePath = await SavePhotoAsync(user, photo);
                var document = await _mindee.GetPassportJson(filePath);
                var passport = InternationalPassport.ParseDocument(document);
                user.NextStep();
                await user.SendMessageAsync(passport.GetData());
                var AIResponce = await user.SendMessageAIAsync("user Send Passport");
                await user.SendMessageAsync(AIResponce);
                user.SaveMessage("AI:" + AIResponce);

            }

            if (message is not null)
            {
                Console.WriteLine(message);
                var AIResponce = await user.SendMessageAIAsync(message);
                await user.SendMessageAsync(AIResponce);
                user.SaveMessage("AI:" + AIResponce);
            }


        }

        public static async Task TaskSendMessage(long chatId, string text)
        {
            await _instance._botclient.SendMessage(chatId, text);
        }

        public static async Task<string> AskAIAsync(long chatId, string message, string prompt)
        {

            var response = await _instance._ai.AskAsync(prompt, message);


            return response;
        }

        public async Task<string> SavePhotoAsync (ChatUser user, PhotoSize[] photo)
        {
            var photoId = photo.LastOrDefault().FileId;
            var file = await _botclient.GetFile(photoId);
            var filePath = $"{user.ChatId}.jpg";
            using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                await _botclient.DownloadFile(
                filePath: file.FilePath,
                destination: fileStream
                );
            }
            return filePath;

        }

    }
}
