using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Palindrome.Web.Models
{
    public class PalindromeModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [DisplayName("Please provide your palindrome string")]
        [Required(ErrorMessage = "Palindrome string required")]
        [JsonPropertyName("palindromeString")]
        public string PalindromeString { get; set; } = "";
        [JsonPropertyName("isPalindrome")]
        public bool IsPalindrome { get; set; } = false;
    }
}
