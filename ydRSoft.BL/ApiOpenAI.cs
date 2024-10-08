using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;
namespace ydRSoft.BL
{
    public class ApiOpenAI
    {
        private static readonly string apiKey = "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";
        public static async Task<string> PreguntaApi(string txtPregunta)
        {
            string model = "gpt-4-turbo";
            //string model = "gpt-3.5-turbo";
            string messageContent = txtPregunta;
            double temperature = 0.7;

            string completion = await GetChatCompletion(model, messageContent, temperature);

            return completion;

        }

        private static async Task<string> GetChatCompletion(string model1, string messageContent, double temperature1)
        {
            HttpClient client = new HttpClient();
            string url = "https://api.openai.com/v1/chat/completions";

            var requestData = new
            {
                model = model1,
                messages = new[]
                {
                new { role = "user", content = messageContent }
            },
                temperature = temperature1
            };

            var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            if (!client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var response = await client.PostAsync(url, requestContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return "Consulta no Autorizada";

        }


        public static string MensajeContent(string jsonText)
        {
            try
            {
                ChatCompletion completion = JsonConvert.DeserializeObject<ChatCompletion>(jsonText);
                string Nombre = completion.Choices[0].Message.Content;

                return Nombre;
            }
            catch
            {
                return "Consulta no Autorizada";
            }

        }
    }
}
