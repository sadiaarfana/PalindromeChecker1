using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Palindrome.Web.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;
using System.Text.Json;

namespace Palindrome.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<APIConfig> _apiConfig;
        public HomeController(ILogger<HomeController> logger, IOptions<APIConfig> apiConfig)
        {
            _logger = logger;
            _apiConfig = apiConfig;

        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> PalindromeCheck()
        {
            return View();
        }
        // check palindrome
        [HttpPost]
        public async Task<IActionResult> PalindromeCheck(PalindromeModel model)
        {
            if(ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing API base URL
                    client.BaseAddress = new Uri(_apiConfig.Value.BaseURL);
                    client.DefaultRequestHeaders.Clear();

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // api request body serialization
                    string strJson = JsonSerializer.Serialize<PalindromeModel>(model);
                    var content = new StringContent(strJson, Encoding.UTF8, "application/json");

                    // call post api to check palindrome
                    HttpResponseMessage _resp = await client.PostAsync("api/IsPalindrome", content);

                    //Checking the response is successful or not which is sent using HttpClient
                    if (_resp.IsSuccessStatusCode)
                    {
                        var _palindromeResponse = await _resp.Content.ReadAsStringAsync();
                        if(Convert.ToBoolean(_palindromeResponse))
                        {
                            model.IsPalindrome = true;
                            strJson = JsonSerializer.Serialize<PalindromeModel>(model);
                            content = new StringContent(strJson, Encoding.UTF8, "application/json");

                            _resp = await client.PostAsync("api/Palindromes", content);
                            // Add string if it is a palindrome 
                            if (_resp.IsSuccessStatusCode)
                            {
                                var _jsonString = await _resp.Content.ReadAsStringAsync(); 
                                var _entity =  JsonSerializer.Deserialize<PalindromeModel>(_jsonString);
                                if (_entity != null)
                                {                                                                   
                                    return View(_entity);
                                }
                            }
                        }                         
                    }
                }
            }
            return View(model);
        }

        // get list of persistant Palindromes
        public async Task<IActionResult> PalindromeList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiConfig.Value.BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Palindromes");

                if (Res.IsSuccessStatusCode)
                {
                    var _itemList = Res.Content.ReadFromJsonAsync<List<PalindromeModel>>().Result;
                    return  View(_itemList);
                }
            }
            return View();
        }
    }
}