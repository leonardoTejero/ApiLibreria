using ApiLibrary.Handlers;
using Common.Utils.Enums;
using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Domain.Dto.User;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System.Collections.Generic;

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
    }
}
