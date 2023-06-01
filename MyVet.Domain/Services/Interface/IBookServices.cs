using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IBookServices
    {
        List<ConsultBookDto> GetAllBooks();
        ResponseDto GetOneBook(int idBook);
        Task<bool> InsertBookAsync(InsertBookDto book);
        Task<ResponseDto> DeleteBookAsync(int idBook);
        Task<bool> UpdateBookAsync(ConsultBookDto book);
    }
}
