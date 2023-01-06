using ApiLibrary.Handlers;
using Common.Utils.Enums;
using Common.Utils.Helpers;
using Common.Utils.Resorces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Book;
using MyLibrary.Domain.Services.Interface;
using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace ApiLibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class BookController : ControllerBase
    {
        #region Attributes
        private readonly IBookServices _bookServices;
        #endregion

        #region Builder
        public BookController(IBookServices datesServices)
        {
            _bookServices = datesServices;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Obtener todos los libros
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpGet]
        [Route("GetAllBooks")]
        [CustomPermissionFilter(Enums.Permission.ConsultarLibros)]
        public IActionResult GetAllBooks()
        {
            List<ConsultBookDto> result = _bookServices.GetAllBooks();

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = string.Empty
            };
            return Ok(response);
        }


        /// <summary>
        /// Crear un nuevo libro
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpPost]
        [Route("InsertBook")]
        [CustomPermissionFilter(Enums.Permission.CrearLibro)]
        public async Task<IActionResult> InsertBook(InsertBookDto book)
        {
            IActionResult response; // bandera

            bool result = await _bookServices.InsertBookAsync(book);

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
        /// Actualizar un libro
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpPut]
        [Route("UpdateBook")]
        [CustomPermissionFilter(Enums.Permission.ActualizarLibros)]
        public async Task<IActionResult> UpdateBook(ConsultBookDto Book)
        {
            IActionResult response;

            bool result = await _bookServices.UpdateBookAsync(Book);

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
        /// Eliminar un libro
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK! </response>
        /// <response code="400">Business Exception</response>
        /// <response code="500">Oops! Can't process your request now</response>
        [HttpDelete]
        [Route("DeleteBook")]
        [CustomPermissionFilter(Enums.Permission.EliminarLibro)]
        public async Task<IActionResult> DeleteBook(int idBook)
        {
            IActionResult response;
            ResponseDto result = await _bookServices.DeleteBookAsync(idBook);

            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }

        #endregion
    }

}
