using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Responses;
using Innvo.Models.UnitOfMeasure;
using Innvo.Services.UnitOfMeasure;
using Microsoft.AspNetCore.Mvc;

namespace Innvo.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly IUnitOfMeasureService _UOMService;

        public UnitOfMeasureController(IUnitOfMeasureService UOMService)
        {
            _UOMService = UOMService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnitOfMeasureCreate req) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _UOMService.Create(req);
          
            return response == true ? Ok(new TextResponse("Unit Of Measure created!")) : BadRequest(new TextResponse("Could not create Unit Of Measure!"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _UOMService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var item = await _UOMService.GetOne(id);
            if (item != null)
                return Ok(item);
            return BadRequest(new TextResponse("Unit Of Measure Could not be found"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UnitOfMeasureUpdate req) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _UOMService.Update(req);
          
            return response == true ? Ok(new TextResponse("Unit Of Measure updated!")) : BadRequest(new TextResponse("Could not update Unit Of Measure!"));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var response = await _UOMService.Delete(id);
            return response == true ? Ok(new TextResponse("Unit Of Measure deleted!")) : BadRequest(new TextResponse("Could not delete Unit Of Measure!"));
        }   
    }
}