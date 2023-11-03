using Microsoft.AspNetCore.Mvc;
using Palindrome.API.Models;
using Palindrome.API.Services;
using System.Text.RegularExpressions;

namespace Palindrome.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class PalindromeController : ControllerBase
    {      

        private readonly ILogger<PalindromeController> _logger;
        private readonly IPalindromeRepository _repository;

        public PalindromeController(ILogger<PalindromeController> logger, IPalindromeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // get all palindromes
        [HttpGet]
        [Route("Palindromes")]
        public List<PalindromeInfo> Get()
        {
            return _repository.GetPalindromes();
        }

        // Palindrome check
        [HttpPost]
        [Route("IsPalindrome")]
        public async Task<bool> IsPalindrome([FromBody] PalindromeInfo entity)
        {
            if (entity == null)
                return false;
            var inputString = entity.PalindromeString.ToLower();
            var finalStr = "";
            foreach(var input in inputString)
            {
                if (char.IsLetterOrDigit(input))
                {
                    finalStr += input;
                }
            }
           
            char[] stringArr = finalStr.ToCharArray();
            Array.Reverse(stringArr);
            var reverseStr = new string(stringArr);

            return finalStr.Equals(reverseStr);
        }
        // add palindrome string in persistent list
        [HttpPost]
        [Route("Palindromes")]
        public async Task<IActionResult> Add([FromBody] PalindromeInfo entity)
        {
            if (entity == null)
                return BadRequest(ModelState);
            if (!_repository.IsExists(entity))
            {
                entity = _repository.Add(entity);
            }
            return Ok(entity);

        }
    }
}