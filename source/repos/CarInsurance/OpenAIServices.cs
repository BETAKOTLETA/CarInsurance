using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
     //bvbhcbcv
    public class OpenAIServices
    {
        private ChatClient _client;

        public OpenAIServices()
        {
            var model = "openai/gpt-4o-mini";
            var endpoint = "https://models.github.ai/inference";
            var credential = System.Environment.GetEnvironmentVariable("GITHUB_TOKEN");

            var openAIOptions = new OpenAIClientOptions()
            {
                Endpoint = new Uri(endpoint)

            };
            _client = new ChatClient(model, new ApiKeyCredential(credential), openAIOptions);
        }

        public async Task<string> AskAsync(string prompt, string message)
        {
            List<ChatMessage> messages = new List<ChatMessage>()
                {
                    new SystemChatMessage(prompt),
                    new UserChatMessage(message),
                };

            var requestOptions = new ChatCompletionOptions()
            {
                Temperature = 1.0f,
                TopP = 1.0f,
                MaxOutputTokenCount = 500
            };

            var responce = await _client.CompleteChatAsync(messages, requestOptions);
            return responce.Value.Content[0].Text;
        }
    }
}
