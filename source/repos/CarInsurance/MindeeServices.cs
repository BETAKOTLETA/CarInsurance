using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mindee;
using Mindee.Input;
using Mindee.Product.Passport;



namespace CarInsurance
{
    public class MindeeServices
    {
        private string _apiKey = System.Environment.GetEnvironmentVariable("Mindee_Token");
        private MindeeClient client;
        
        public MindeeServices()
        {
            client = new MindeeClient(_apiKey);
        }

        public async Task<string> GetPassportJson(string filePath)
        {
            var inputSource = new LocalInputSource(filePath);
            var response = await client.ParseAsync<PassportV1>(inputSource);
            return response.Document.ToString();
        }

    }
}
