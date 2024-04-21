using System;
using System.Collections.Generic;
using System.Linq;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Dtos.Comment
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository repo;

        public CommentController(ICommentRepository commentRepo)
        {
            repo=commentRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var model=await repo.GetAllAsync();
            var commentDto=model.Select(x=>x.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment=await repo.GetByIdAsync(id);
            if (comment==null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute]int stockId,[FromBody]CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!await repo.StockExist(stockId))
                return BadRequest("Stock does not exist");
            var model= await repo.CreateAsync(createCommentDto.ToCommentFromCreate(stockId));
            return CreatedAtAction(nameof(GetById),new{id=model.Id},model.ToCommentDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody]UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var comment=await repo.UpdateAsync(id,updateCommentDto);
            if (comment==null)
                return NotFound();
            return Ok(comment.ToCommentDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var comment=await repo.DeleteAsync(id);
            if (comment==null)
                return NotFound();
            return NoContent();
        }
    }
}