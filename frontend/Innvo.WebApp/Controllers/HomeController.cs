using Innvo.Models.Token;
using Innvo.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Innvo.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public const string SessionKeyToken = "_token";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(TokenRequest request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyToken)))
            {
                return RedirectToAction(nameof(Index));
            }

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                    {
                        request.UserName,
                        request.Password,
                    }),
                    Encoding.UTF8,
                    "application/json");

            HttpClient client = new HttpClient();

            HttpResponseMessage resp = await client.PostAsync("http://127.0.0.1:5236/api/token", jsonContent);
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<TokenResponse>(await resp.Content.ReadAsStringAsync());
            if(jsonResponse!.Token == null) {
                return RedirectToAction(nameof(Error));
            }

            HttpContext.Session.SetString(SessionKeyToken, jsonResponse.Token);

            return RedirectToAction("Index", "Inventory");
        }

        public IActionResult Logout(){
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyToken)))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.Remove(SessionKeyToken);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]    
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
