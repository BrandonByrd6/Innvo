using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Innvo.Models.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Innvo.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ItemController(ILogger<HomeController> logger)
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
            HttpResponseMessage resp = await client.GetAsync("http://127.0.0.1:5236/api/item");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<List<ItemListItem>>(await resp.Content.ReadAsStringAsync());
            if (jsonResponse == null)
            {
                return RedirectToAction("error", "home");
            }

            return View(jsonResponse);
        }

        public IActionResult Create()
        {
            var token = HttpContext.Session.GetString("_token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreate model)
        {
            var token = HttpContext.Session.GetString("_token");
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            using StringContent payload = new(
                JsonSerializer.Serialize(new
                {
                    model.Name,
                    model.Code,
                    model.Description,
                    model.ImgUrl,
                    model.UOMId,
                    model.Quantity
                }),
                    Encoding.UTF8,
                    "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.PostAsync("http://127.0.0.1:5236/api/item/", payload);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
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
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/item/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<ItemDetail>(await resp.Content.ReadAsStringAsync());
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
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/item/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var res = JsonSerializer.Deserialize<ItemDetail>(await resp.Content.ReadAsStringAsync());
            if (res == null)
            {
                return RedirectToAction("error", "home");
            }

            var item = new ItemUpdate()
            {
                Id = res.Id,
                Name = res.Name,
                Code = res.Code,
                Description = res.Description,
                ImgUrl = res.ImgUrl,
                BarCode = res.BarCode
            };

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemUpdate model)
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
                    model.Name,
                    model.Code,
                    model.Description,
                    model.ImgUrl,
                }),
                    Encoding.UTF8,
                    "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.PutAsync("http://127.0.0.1:5236/api/item/", payload);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
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
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/item/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<ItemDetail>(await resp.Content.ReadAsStringAsync());
            if (jsonResponse == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(jsonResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("_token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.DeleteAsync($"http://127.0.0.1:5236/api/item/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}