using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Transaction;
using Innvo.Models.Responses;
using Innvo.Services.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innvo.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _transactionService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var transaction = await _transactionService.GetOne(id)!;
            if (transaction != null)
                return Ok(transaction);
            return BadRequest(new TextResponse("Inventory Could not be found"));
        }
    }
}