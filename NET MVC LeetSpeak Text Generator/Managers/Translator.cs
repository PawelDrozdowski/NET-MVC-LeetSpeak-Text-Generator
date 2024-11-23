using NET_MVC_LeetSpeak_Text_Generator.Contracts;
using NET_MVC_LeetSpeak_Text_Generator.Helpers;
using NET_MVC_LeetSpeak_Text_Generator.Models.DTO;
using System.Text;

namespace NET_MVC_LeetSpeak_Text_Generator.Managers
{
    public class Translator : ITranslator
    {
        private static readonly HttpClient client = new();
        private readonly string _translatorURL;

        public Translator(string translatorURL)
        {
            _translatorURL = translatorURL;
        }

        public async Task<TranslatorDtoResponse> TranslateText(string text)
        {
            string translatorReq = JSON.ToJSON(new TranslatorDtoRequest() { text = text });
            var requestContent = new StringContent(translatorReq, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync(_translatorURL, requestContent);
            var responseContent = await responseMsg.Content.ReadAsStringAsync();
            var response = JSON.FromJSON<TranslatorDtoResponse>(responseContent);
            return response;
        }
    }
}
