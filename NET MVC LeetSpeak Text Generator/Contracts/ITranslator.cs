using NET_MVC_LeetSpeak_Text_Generator.Models.DTO;

namespace NET_MVC_LeetSpeak_Text_Generator.Contracts
{
    public interface ITranslator
    {
        public Task<TranslatorDtoResponse> TranslateText(string text);
    }
}