using ApiLibrary.Handlers;
using Common.Utils.Enums;
using Common.Utils.Helpers;
using Common.Utils.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Author;
using MyLibrary.Domain.Services;
using MyLibrary.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace ApiLibreriaNeoris.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class AuthorController : ControllerBase
    {
        #region Attributes
        private readonly IAuthorServices _authorServices;
        #endregion

        #region Builder
        // Obtener objeto inyectado
        public AuthorController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtener todos los autores
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpGet]
        [Route("GetAllAuthors")]
        [CustomPermissionFilter(Enums.Permission.ConsultarAutor)]
        public IActionResult GetAllAuthors()
        {
            List<AuthorDto> result = _authorServices.GetAllAuthors();

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = string.Empty
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtener un autor por id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>

        // Enrutamiento como segmento de ruta author/GetOneAuthor/2
        // [HttpGet("GetOneAuthor/{id}")]

        [HttpGet]
        [Route("GetOneAuthor")]
        [CustomPermissionFilter(Enums.Permission.ConsultarAutor)]
        public IActionResult GetOneAuthor(int id)
        {
            ResponseDto result = _authorServices.GetOneAuthor(id);

            return Ok(result);
        }


        /// <summary>
        /// Crear un nuevo autor
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpPost]
        [Route("InsertAuthor")]
        [CustomPermissionFilter(Enums.Permission.CrearAutor)]
        public async Task<IActionResult> InsertAuthor(InsertAuthorDto author)
        {
            IActionResult response; // bandera

            //obtener el Id del usuario
            string idUser = Utils.GetClaimValue(Request.Headers["Authorization"], TypeClaims.IdUser);
            bool result = await _authorServices.InsertAuthorAsync(author, Convert.ToInt32(idUser));

            ResponseDto responseDto = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            if (result)
                response = Ok(responseDto);
            else
                response = BadRequest(responseDto);

            return response;
        }


        /// <summary>
        /// Actualizar un autor
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpPut]
        [Route("UpdateAuthor")]
        [CustomPermissionFilter(Enums.Permission.ActualizarAutor)]
        public async Task<IActionResult> UpdateAuthor(AuthorDto author)
        {
            IActionResult response;

            bool result = await _authorServices.UpdateAuthorAsync(author);

            ResponseDto responseDto = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated
            };

            if (result)
                response = Ok(responseDto);
            else
                response = BadRequest(responseDto);

            return response;
        }

        /// <summary>
        /// Eliminar un autor
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpDelete]
        [Route("DeleteAuthor")]
        [CustomPermissionFilter(Enums.Permission.EliminarAutor)]
        public async Task<IActionResult> DeleteAuthor(int idAuthor)
        {
            IActionResult response;
            ResponseDto result = await _authorServices.DeleteAuthorAsync(idAuthor);

            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }
        #endregion
    }
}
