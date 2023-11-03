using Palindrome.API.Models;

namespace Palindrome.API.Services
{
    // reponsitory interface
    public interface IPalindromeRepository
    {
        public List<PalindromeInfo> GetPalindromes();
        public PalindromeInfo Add(PalindromeInfo palindromeInfo);
        public bool IsExists(PalindromeInfo entity);
    }
}
