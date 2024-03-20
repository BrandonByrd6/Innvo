using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Innvo.Models.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Innvo.WebApp.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public InventoryController(ILogger<HomeController> logger)
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
            HttpResponseMessage resp = await client.GetAsync("http://127.0.0.1:5236/api/Inventory");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();
            System.Console.WriteLine(await resp.Content.ReadAsStringAsync());

            var jsonResponse = JsonSerializer.Deserialize<List<InventoryListItem>>(await resp.Content.ReadAsStringAsync());
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
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/Inventory/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<InventoryDetail>(await resp.Content.ReadAsStringAsync());
            if (jsonResponse == null)
            {
                return RedirectToAction("error", "home");
            }

            return View(jsonResponse);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var token = HttpContext.Session.GetString("_token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("index", "home");
            }

            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/Inventory/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var res = JsonSerializer.Deserialize<InventoryDetail>(await resp.Content.ReadAsStringAsync());
            if (res == null)
            {
                return RedirectToAction("error", "home");
            }

            var item = new InventroyUpdate()
            {
                Id = res.Id,
                ItemId = res.ItemId,
                UnitOfMesureId = res.UOMId,
                Quantity = res.Quantity
            };

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InventroyUpdate model)
        {
            var token = HttpContext.Session.GetString("_token");
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit));
            }

            using StringContent payload = new(
                JsonSerializer.Serialize(new
                {
                    Id = id,
                    model.ItemId,
                    model.UnitOfMesureId,
                    model.Quantity
                }),
                    Encoding.UTF8,
                    "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.PutAsync("http://127.0.0.1:5236/api/Inventory/", payload);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}