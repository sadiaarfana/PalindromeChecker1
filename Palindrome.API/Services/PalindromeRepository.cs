using Microsoft.Extensions.Caching.Memory;
using Palindrome.API.Models;

namespace Palindrome.API.Services
{
    public class PalindromeRepository : IPalindromeRepository
    {

        public PalindromeRepository()
        {
           
        }

        public PalindromeInfo Add(PalindromeInfo palindromeInfo)
        {
            try
            {
                if (palindromeInfo.PalindromeString != null)
                {
                    using (var context = new ApiContext())
                    {
                        context.Palindroms.Add(palindromeInfo);
                        context.SaveChanges();

                        return palindromeInfo;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PalindromeInfo> GetPalindromes()
        {
            using (var context = new ApiContext())
            {
                var list = context.Palindroms.ToList();
                return list;
            }
        }
       
        public bool IsExists(PalindromeInfo entity)
        {
            using (var context = new ApiContext())
            {
                if (context.Palindroms.Any(x => x.PalindromeString.ToLower() == entity.PalindromeString)) return true;
                
            }
            return false;
        }
    }
}
