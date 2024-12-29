using Books.Database;
using Books.Dto.Author;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Author;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<AuthorModel>>> FindAll()
    {
        ResponseModel<List<AuthorModel>> response = new();

        try
        {
            List<AuthorModel> authors = await _context.Authors.ToListAsync();
            response.Data = authors;
            response.Message = "Successfully loaded all authors";
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> FindById(int id)
    {
        ResponseModel<AuthorModel> response = new();

        try
        {
            AuthorModel? author = await _context.Authors.FirstOrDefaultAsync(found => found.Id == id);
            response.Data = author;
            response.Message = author == null ? $"No author found for id {id}" : $"Successfully found author {id}";
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> FindByBookId(int bookId)
    {
        ResponseModel<AuthorModel> response = new();

        try
        {
            AuthorModel? author = (await _context.Books.Include(book => book.Author)
                .FirstOrDefaultAsync(book => book.Id == bookId))?.Author;

            response.Data = author;
            response.Message = author == null
                ? $"No author found for book id {bookId}"
                : $"Successfully found author {author.Id} for book {bookId}";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> Create(AuthorDto dto)
    {
        ResponseModel<AuthorModel> response = new();

        try
        {
            AuthorModel newAuthor = new() { FirstName = dto.FirstName, LastName = dto.LastName };
            var createdData = _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
            AuthorModel createdAuthor = createdData.Entity;

            response.Data = createdAuthor;
            response.Message = createdAuthor == null
                ? $"Failed to create author"
                : $"Successfully created author";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> Update(int id, AuthorDto dto)
    {
        ResponseModel<AuthorModel> response = new();

        try
        {
            AuthorModel? authorToUpdate = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);

            if (authorToUpdate == null)
            {
                response.Success = false;
                response.Message = $"No author found for id {id}";
            }
            else
            {
                if (dto.FirstName != null) authorToUpdate.FirstName = dto.FirstName;
                if (dto.LastName != null) authorToUpdate.LastName = dto.LastName;
                await _context.SaveChangesAsync();

                response.Message = $"Author {id} successfully updated";
            }

            response.Data = authorToUpdate;

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> DeleteById(int id)
    {
        ResponseModel<AuthorModel> response = new();

        try
        {
            AuthorModel? authorToDelete = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
            if (authorToDelete != null)
            {
                _context.Remove(authorToDelete);
                await _context.SaveChangesAsync();
            }

            response.Data = null;
            response.Message = "Successfully deleted author";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }
}
