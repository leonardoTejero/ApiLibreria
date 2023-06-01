using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Editorial;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IEditorialServices
    {
        List<EditorialDto> GetAllEditorial();
        Task<ResponseDto> DeleteEditorialAsync(int idEditorial);
        Task<bool> InsertEditorialAsync(InsertEditorialDto editorial);
        Task<bool> UpdateEditorialAsync(EditorialDto editorial);
    }
}
