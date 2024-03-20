using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Innvo.Models.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Innvo.WebApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public TransactionController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("_token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("index", "home");
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.GetAsync("http://127.0.0.1:5236/api/Transaction/");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<List<TransactionListItem>>(await resp.Content.ReadAsStringAsync());
            if (jsonResponse == null)
            {
                return RedirectToAction("error", "home");
            }

            return View(jsonResponse);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var token = HttpContext.Session.GetString("_token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("index", "home");
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/Transaction/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<TransactionDetail>(await resp.Content.ReadAsStringAsync());
            if (jsonResponse == null)
            {
                return RedirectToAction("error", "home");
            }

            return View(jsonResponse);
        }
    }
}