using Books.Dto.Author;
using Books.Models;

namespace Books.Services.Author;

public interface IAuthorService
{
    Task<ResponseModel<List<AuthorModel>>> FindAll();
    Task<ResponseModel<AuthorModel>> FindById(int id);
    Task<ResponseModel<AuthorModel>> FindByBookId(int bookId);
    Task<ResponseModel<AuthorModel>> Create(AuthorDto dto);
    Task<ResponseModel<AuthorModel>> Update(AuthorDto dto);
    Task<ResponseModel<AuthorModel>> DeleteById(int id);
}
