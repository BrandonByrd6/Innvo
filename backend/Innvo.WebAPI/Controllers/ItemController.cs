using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Item;
using Innvo.Models.Responses;
using Innvo.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innvo.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemCreate req) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _itemService.Create(req);
          
            return response == true ? Ok(new TextResponse("Item created!")) : BadRequest(new TextResponse("Could not create Item!"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _itemService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var item = await _itemService.GetOne(id);
            if (item != null)
                return Ok(item);
            return BadRequest(new TextResponse("Item Could not be found"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ItemUpdate req) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _itemService.Update(req);
          
            return response == true ? Ok(new TextResponse("Item updated!")) : BadRequest(new TextResponse("Could not update Item!"));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var response = await _itemService.Delete(id);
            return response == true ? Ok(new TextResponse("Item deleted!")) : BadRequest(new TextResponse("Could not delete Item!"));
        }
    }
}