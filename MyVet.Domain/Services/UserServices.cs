using Common.Utils.Exceptions;
using Common.Utils.Helpers;
using Common.Utils.Resources;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.User;
using MyLibrary.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;
using static Common.Utils.Enums.Enums;

namespace MyLibrary.Domain.Services
{
    public class UserServices : IUserServices
    {
        #region Attribute
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        #endregion

        #region Builder
        public UserServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        #region authentication

        public TokenDto Login(LoginDto login)
        {
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(x => x.Email == login.UserName
                                                                        && x.Password == Utils.Encrypt(login.Password),
                                                                        r => r.RolUserEntities);
            if (user == null)
                throw new BusinessException(GeneralMessages.BadCredentials);

            //TOKEN
            return GenerateTokenJWT(user);
        }

        private TokenDto GenerateTokenJWT(UserEntity userEntity)
        {
            //Obtener la clave de cifrado el token, guardada en appSettings
            IConfigurationSection tokenAppSetting = _configuration.GetSection("Tokens");

            //Crear el encabezado del token
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAppSetting.GetSection("Key").Value));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(TypeClaims.IdUser, userEntity.IdUser.ToString()),
                new Claim(TypeClaims.UserName, userEntity.FullName),
                new Claim(TypeClaims.Email, userEntity.Email),
                new Claim(TypeClaims.IdRol, string.Join(",", userEntity.RolUserEntities.Select(x=>x.IdRol))),
            };

            var _payload = new JwtPayload(
                    issuer: tokenAppSetting.GetSection("Issuer").Value, // puede ser null
                    audience: tokenAppSetting.GetSection("Audience").Value, // null  
                    claims: _Claims,
                    notBefore: DateTime.UtcNow, //hora internacional
                    expires: DateTime.UtcNow.AddMinutes(60) //minutos que expira la sesion
                );

            var _token = new JwtSecurityToken(
                    _header,
                    _payload
                );

            TokenDto token = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(_token),
                Expiration = Utils.ConvertToUnixTimestamp(_token.ValidTo),
            };

            return token;
        }

        public async Task<ResponseDto> Register(UserDto data)
        {
            ResponseDto result = new ResponseDto();

            if (Utils.ValidateEmail(data.UserName))
            {
                if (_unitOfWork.UserRepository.FirstOrDefault(x => x.Email == data.UserName) == null)
                {

                    RolUserEntity rolUser = new RolUserEntity()
                    {
                        IdRol = RolUser.Estandar.GetHashCode(),
                        UserEntity = new UserEntity()
                        {
                            Email = data.UserName,
                            LastName = data.LastName,
                            Name = data.Name,
                            Password = Utils.Encrypt(data.Password)
                        }
                    };

                    _unitOfWork.RolUserRepository.Insert(rolUser);
                    result.IsSuccess = await _unitOfWork.Save() > 0;

                    // retornar un token si fue exitoso
                }
                else
                    result.Message = "Email ya se encuestra registrado, utiliza otro!";
            }
            else
                result.Message = "Usuario con Email Inválido";

            return result;
        }

        #endregion

        #region Methods Crud

        //public List<ConsultUserDto> GetAllUsers()
        //{
        //    var user = _unitOfWork.UserRepository.GetAll();

        //    List<ConsultUserDto> users = user.Select(x => new ConsultUserDto
        //    {
        //        Name = x.Name,
        //        LastName = x.LastName,
        //        Email = x.Email,
        //        //Rol ????

        //    }).ToList();

        //    return users;
        //}
        public List<ConsultUserDto> GetAllUsers()
        {
            var user = _unitOfWork.RolUserRepository.GetAll(x => x.UserEntity);

            // Hacer la relacion con RolUser para sacar el idRol, que no sale directamente de userEntity
            List<ConsultUserDto> users = user.Select(x => new ConsultUserDto
            {
                Id = x.Id,
                Name = x.UserEntity.Name,
                LastName = x.UserEntity.LastName,
                UserName = x.UserEntity.Email,
                IdRol = x.IdRol,

            }).ToList();

            return users;
        }

        // TO DO validaciones si no encuentra el usuario
        public UserEntity GetUser(int idUser)
        {
            return _unitOfWork.UserRepository.FirstOrDefault(x => x.IdUser == idUser);
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(x => x.Email == userDto.UserName);

            user.Name = userDto.Name;
            user.LastName = userDto.LastName;
            user.Password = Utils.Encrypt(userDto.Password);

            _unitOfWork.UserRepository.Update(user);

            return await _unitOfWork.Save() > 0;

        }

        public async Task<ResponseDto> DeleteUser(int idUser)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.UserRepository.Delete(idUser);

            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = "Usuario Eliminado";
            else
                response.Message = "Ocurrio un error al eliminar el Usuario, por favor vuelva a intentalo";

            return response;
        }

        public async Task<ResponseDto> CreateUser(UserEntity data)
        {
            ResponseDto result = new ResponseDto();

            if (Utils.ValidateEmail(data.Email))
            {
                if (_unitOfWork.UserRepository.FirstOrDefault(x => x.Email == data.Email) == null)
                {
                    int idRol = data.IdUser;
                    data.Password = "123456";
                    data.IdUser = 0;

                    RolUserEntity rolUser = new RolUserEntity()
                    {
                        IdRol = idRol,
                        UserEntity = data
                    };

                    _unitOfWork.RolUserRepository.Insert(rolUser);
                    result.IsSuccess = await _unitOfWork.Save() > 0;
                }
                else
                    result.Message = "Email ya se encuestra registrado, utilizar otro!";
            }
            else
                result.Message = "Usuario con Email Inválido";

            return result;
        }

        #endregion

    }
}
