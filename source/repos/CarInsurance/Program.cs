using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using CarInsurance;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

var bot = new BotService("key");

await bot.Start();

//var user = new ChatUser(); //
//user.SendMessageAsync("Hioddsd");

//var user = new ChatUser();

//for (int i = 0; i < 99999; i++)
//{
//    await user.SendMessageAsync("1");
//}
    //var client = new ChatClient(modelName, Constants.OpenAIKey);

    //var responce = await client.CompleteChatAsync("What is the capital of Ukraine");

    //Console.WriteLine(responce.Value.Content[0].Text);

    Console.ReadLine();