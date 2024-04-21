using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IStockRepository stockRepo;

        public StockController(ApplicationDbContext _context,IStockRepository _stockRepo)
        {
            context=_context;
            stockRepo=_stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
            var stocks= await stockRepo.GetAllAsync(query);
            var stockDto=stocks.Select(x=>x.ToStockDto());
            return Ok(stockDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var stock=await stockRepo.GetByIdAsync(id);
            if (stock==null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateStockDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var model=await stockRepo.CreateAsync(stockDto);
            return CreatedAtAction(nameof(GetById),new {id=model.Id},model.ToStockDto());
        }
        [HttpPut("{id:int}")]
        public  async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateStockDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var model=await stockRepo.UpdateAsync(id,stockDto);
            if (model==null)
            {
                return NotFound();
            }
            return Ok(model.ToStockDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var model=await stockRepo.DeleteAsync(id);
            if (model==null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}