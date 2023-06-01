using Infraestructure.Entity.Model;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IUserServices
    {
        #region Auth
        TokenDto Login(LoginDto user);
        Task<ResponseDto> Register(UserDto data);
        #endregion

        #region Methods Crud
        List<ConsultUserDto> GetAllUsers();
        UserEntity GetUser(int idUser);
        Task<bool> UpdateUser(UserEntity user);
        Task<ResponseDto> DeleteUser(int idUser);
        Task<ResponseDto> CreateUser(UserEntity user);

        #endregion
    }
}
