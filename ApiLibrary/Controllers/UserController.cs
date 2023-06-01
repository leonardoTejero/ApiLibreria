using ApiLibrary.Handlers;
using Common.Utils.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.User;
using MyLibrary.Domain.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLibreriaNeoris.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion


        /// <summary>
        /// Obtener todos los Usuarios
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpGet]
        [Route("GetAllUsers")]
        [CustomPermissionFilter(Enums.Permission.ConsultarUsuarios)]
        public IActionResult GetAllUsers()
        {
            List<ConsultUserDto> result = _userServices.GetAllUsers();

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = string.Empty
            };
            return Ok(response);
        }

        /// <summary>
        /// Eliminar un usuario por Id
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpDelete]
        [Route("DeleteUser")]
        [CustomPermissionFilter(Enums.Permission.EliminarUsuarios)]
        public async Task<IActionResult> DeleteUser(int idUser)
        {
            IActionResult result;

            ResponseDto response = await _userServices.DeleteUser(idUser);
            if (response.IsSuccess)
                result = Ok(response);
            else
                result = BadRequest(response);

            return result;
        }


    }
}
