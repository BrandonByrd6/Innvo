using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Innvo.Models.UnitOfMeasure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Innvo.WebApp.Controllers
{
    public class UnitOfMeasureController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UnitOfMeasureController(ILogger<HomeController> logger)
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
            HttpResponseMessage resp = await client.GetAsync("http://127.0.0.1:5236/api/UnitOfMeasure");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<List<UnitOfMeasureListItem>>(await resp.Content.ReadAsStringAsync());
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
        public async Task<IActionResult> Create(UnitOfMeasureCreate model)
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
                    model.Description,
                    model.Abbreviation
                }),
                    Encoding.UTF8,
                    "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.PostAsync("http://127.0.0.1:5236/api/UnitOfMeasure/", payload);
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
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/UnitOfMeasure/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var jsonResponse = JsonSerializer.Deserialize<UnitOfMeasureDetail>(await resp.Content.ReadAsStringAsync());
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
            HttpResponseMessage resp = await client.GetAsync($"http://127.0.0.1:5236/api/UnitOfMeasure/{id}");
            //resp.EnsureSuccessStatusCode();
            //resp.WriteRequestToConsole();

            var res = JsonSerializer.Deserialize<UnitOfMeasureDetail>(await resp.Content.ReadAsStringAsync());
            if (res == null)
            {
                return RedirectToAction("error", "home");
            }

            var item = new UnitOfMeasureUpdate()
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Abbreviation = res.Abbreviation,
            };

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UnitOfMeasureUpdate model)
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
                    model.Abbreviation,
                    model.Description,
                }),
                    Encoding.UTF8,
                    "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage resp = await client.PutAsync("http://127.0.0.1:5236/api/UnitOfMeasure/", payload);
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

            var jsonResponse = JsonSerializer.Deserialize<UnitOfMeasureDetail>(await resp.Content.ReadAsStringAsync());
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
            HttpResponseMessage resp = await client.DeleteAsync($"http://127.0.0.1:5236/api/UnitOfMeasure/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}