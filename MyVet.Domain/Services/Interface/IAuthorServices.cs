using MyLibrary.Domain.Dto.Author;
using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IAuthorServices
    {
        List<AuthorDto> GetAllAuthors();
        Task<bool> InsertAuthorAsync(InsertAuthorDto author, int idUser);
        Task<ResponseDto> DeleteAuthorAsync(int idAuthor);
        Task<bool> UpdateAuthorAsync(AuthorDto author);
    }
}
