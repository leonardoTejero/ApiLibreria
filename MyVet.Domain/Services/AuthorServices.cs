using Infraestructure.Core.UnitOfWork;
using Infraestructure.Entity.Model.Library;
using MyLibrary.Domain.Dto.Author;
using MyLibrary.Domain.Services.Interface;
using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services
{
    public class AuthorServices : IAuthorServices
    {
        #region Attributes
        private readonly UnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public AuthorServices(UnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }
        #endregion

        #region Methods
        public List<AuthorDto> GetAllAuthors()
        {
            var author = _unitOfWork.AuthorRepository.GetAll(); //x => x.UserEntity

            List<AuthorDto> listAuthors = author.Select(x => new AuthorDto
            {
                Id = x.IdAuthor,
                Name = x.Name,
                LastName = x.LastName,
                //IdUser = x.IdUser,

            }).ToList();

            return listAuthors;
        }

        public async Task<bool> InsertAuthorAsync(InsertAuthorDto author, int idUser)
        {

            AuthorEntity authors = new AuthorEntity()
            {
                Name = author.Name,
                LastName = author.LastName,
                IdUser = idUser,
            };

            _unitOfWork.AuthorRepository.Insert(authors);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<ResponseDto> DeleteAuthorAsync(int idAuthor)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.AuthorRepository.Delete(idAuthor);

            response.IsSuccess = await _unitOfWork.Save() > 0;

            if (response.IsSuccess)
                response.Message = "Se elminó correctamente el autor";
            else
                response.Message = "Hubo un error al eliminar el autor, por favor vuelva a intentalo";

            return response;
        }

        public async Task<bool> UpdateAuthorAsync(AuthorDto author)
        {
            bool result = false;

            AuthorEntity authors = _unitOfWork.AuthorRepository.FirstOrDefault(x => x.IdAuthor == author.Id);
            if (authors != null)
            {
                authors.Name = author.Name;
                authors.LastName = author.LastName;

                _unitOfWork.AuthorRepository.Update(authors);
                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        #endregion
    }
}
