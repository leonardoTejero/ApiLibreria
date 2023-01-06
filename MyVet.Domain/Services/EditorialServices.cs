using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Library;
using Infraestructure.Entity.Model.Library;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Editorial;
using MyLibrary.Domain.Services.Interface;
using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services
{
    public class EditorialServices : IEditorialServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public EditorialServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public List<EditorialDto> GetAllEditorial()
        {
            var editorial = _unitOfWork.EditorialRepository.GetAll(x => x.BookEntities);  //x => x.BookEntities

            List<EditorialDto> list = editorial.Select(x => new EditorialDto
            {
                Id = x.Id,
                Name = x.Name,
                Campus = x.Campus,
                Books = x.BookEntities.ToList().Select(x => x.Name),

            }).ToList();

            return list;
        }

        public async Task<bool> InsertEditorialAsync(InsertEditorialDto editorial)
        {
            EditorialEntity editoriales = new EditorialEntity()
            {
                Name = editorial.Name,
                Campus = editorial.Campus,
            };

            _unitOfWork.EditorialRepository.Insert(editoriales);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<ResponseDto> DeleteEditorialAsync(int idEditorial)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.EditorialRepository.Delete(idEditorial);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = "Se elminó correctamente el editorial";
            else
                response.Message = "Hubo un error al eliminar el editorial, por favor vuelva a intentalo";

            return response;
        }

        public async Task<bool> UpdateEditorialAsync(EditorialDto editorial)
        {
            bool result = false;

            EditorialEntity editorialEntity = _unitOfWork.EditorialRepository.FirstOrDefault(x => x.Id == editorial.Id);
            if (editorialEntity != null)
            {
                editorialEntity.Name = editorial.Name;
                editorialEntity.Campus = editorial.Campus;

                _unitOfWork.EditorialRepository.Update(editorialEntity);

                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        #endregion

    }
}
