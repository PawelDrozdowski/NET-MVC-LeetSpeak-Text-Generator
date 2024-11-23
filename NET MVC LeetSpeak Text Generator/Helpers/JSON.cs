using System.Runtime.Serialization.Json;
using System.Text;

namespace NET_MVC_LeetSpeak_Text_Generator.Helpers
{
    public class JSON
    {
        public static string ToJSON(object obj)
        {
            var ms = new MemoryStream();

            var ser = new DataContractJsonSerializer(obj.GetType());
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public static T FromJSON<T>(string json) where T : class, new()
        {
            var deserialized = new T();
            try
            {
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var ser = new DataContractJsonSerializer(deserialized.GetType());
                deserialized = ser.ReadObject(ms) as T;
                ms.Close();
            }
            catch { return null; }
            return deserialized;
        }
    }
}
