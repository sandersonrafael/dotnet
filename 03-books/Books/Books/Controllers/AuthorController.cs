using Books.Dto.Author;
using Books.Models;
using Books.Services.Author;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet("FindAuthors")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> FindAll()
    {
        ResponseModel<List<AuthorModel>> authors = await _service.FindAll();
        return Ok(authors);
    }

    [HttpGet("FindAuthor/{id}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> FindById(int id)
    {
        ResponseModel<AuthorModel> author = await _service.FindById(id);
        return Ok(author);
    }

    [HttpGet("FindAuthorByBookId/{bookId}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> FindByBookId(int bookId)
    {
        ResponseModel<AuthorModel> author = await _service.FindByBookId(bookId);
        return Ok(author);
    }

    [HttpPost("CreateAuthor")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Create(AuthorDto dto)
    {
        ResponseModel<AuthorModel> author = await _service.Create(dto);
        return Created($"FindAuthor/{author.Data?.Id}", author);
    }

    [HttpDelete("DeleteAuthorById/{id}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> DeleteById(int id)
    {
        ResponseModel<AuthorModel> author = await _service.DeleteById(id);
        return Ok(author);
    }
}
