using System.ComponentModel.DataAnnotations;

namespace NET_MVC_LeetSpeak_Text_Generator.Models
{
    public class ApiCall
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MinLength(5)]
        public string Request { get; set; }
        public string Response { get; set; }
    }
}
