using Data;
using Dotnet_backend.Dtos.Comment;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_backend.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository) : ControllerBase
    {
        private readonly ICommentRepository _commentRepository = commentRepository;

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest createCommentRequest)
        {
            var comment = createCommentRequest.ToCommentFromCreateDto();
            var createdComment = await _commentRepository.CreateAsync(comment);

            if (createdComment == null)
            {
                return BadRequest(new { message = "given stock id not invalid or not stock give ID" });
            }
            return CreatedAtAction(nameof(GetCommentById), new
            {
                id = comment.Id
            }, comment.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequest updateCommentRequest)
        {
            var comment = await _commentRepository.UpdateAsync(id, updateCommentRequest);

            if (comment == null)
            {
                return NotFound(new { message = "Not found comment given comment Id" });
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound(new { message = "Not comment to given Id" });
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            var commentDtos = comments.Select(c => c.ToCommentDto());

            return Ok(commentDtos);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteComment(id);

            if (comment == null)
            {
                return NotFound(new { message = "comment not fount given Id" });
            }

            return Ok(new { message = "comment deleted" });
        }

        [HttpGet("stock/{id:int}")]
        public async Task<IActionResult> GetCommentsByStockId([FromRoute] int id)
        {
            var comments = await _commentRepository.GetCommentsByStockId(id);

            if (comments == null)
            {
                return NotFound(new { message = "Not comments yet given Id" });
            }
            
            var commentDtos = comments.Select(s => s.ToCommentDto());

            return Ok(commentDtos);
        }
    }
}