using Microsoft.EntityFrameworkCore;
using Palindrome.API.Services;
using Palindrome.API.Models;

namespace Palindrome.API
{
    public class ApiContext : DbContext
    {
        // using EF in-memory database for persistent list
        // initialise DB context
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PalindromeDB");
        }
        public DbSet<PalindromeInfo> Palindroms { get; set; }
    }
}