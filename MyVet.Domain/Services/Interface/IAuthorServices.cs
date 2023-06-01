using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Author;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IAuthorServices
    {
        List<AuthorDto> GetAllAuthors();
        ResponseDto GetOneAuthor(int id);
        Task<bool> InsertAuthorAsync(InsertAuthorDto author, int idUser);
        Task<ResponseDto> DeleteAuthorAsync(int idAuthor);
        Task<bool> UpdateAuthorAsync(AuthorDto author);
    }
}
