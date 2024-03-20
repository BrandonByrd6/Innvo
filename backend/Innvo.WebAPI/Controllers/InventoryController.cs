using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Inventory;
using Innvo.Models.Responses;
using Innvo.Services.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innvo.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventroyService;

        public InventoryController(IInventoryService inventroyService)
        {
            _inventroyService = inventroyService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _inventroyService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var inventory = await _inventroyService.GetOne(id)!;
            if (inventory != null)
                return Ok(inventory);
            return BadRequest(new TextResponse("Inventory Could not be found"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InventroyUpdate req) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _inventroyService.Update(req);
          
            return response == true ? Ok(new TextResponse("Inventory updated!")) : BadRequest(new TextResponse("Could not update Inventory!"));
        }        
    }
}