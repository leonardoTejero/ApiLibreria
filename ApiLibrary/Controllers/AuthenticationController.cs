using ApiLibrary.Handlers;
using Common.Utils.Resources;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Services.Interface;
using System;
using System.Threading.Tasks;

namespace ApiLibreriaNeoris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class AuthenticationController : ControllerBase
    {
        #region Attributes
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public AuthenticationController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Credenciales de acceso
        ///     {
        ///        "userName": "admin@gmail.com",
        ///        "password": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="login"></param>
        /// <returns> Token</returns>
        /// <response code="200">Token</response>
        /// <response code="400">Business Exception</response>
        /// <response code="401">User unauthorized</response>
        /// <response code="500">Oops! </response>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto login)
        {
            TokenDto result = _userServices.Login(login);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = string.Empty
            };

            return Ok(response);
        }

        /// <summary>
        /// Registro primera vez
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// /// <response code="200">Success</response>
        /// <response code="400">Business Exception</response>
        /// <response code="401">User unauthorized</response>
        /// <response code="500">Oops! </response>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            IActionResult response;

            var result = await _userServices.Register(user);

            ResponseDto responseDto = new ResponseDto()
            {
                IsSuccess = result.IsSuccess,
                Result = result.Result,
                Message = result.IsSuccess ? GeneralMessages.ItemInserted : result.Message //GeneralMessages.ItemNoInserted
            };

            if (result.IsSuccess)
                response = Ok(responseDto);
            else
                response = BadRequest(responseDto);

            return response;
        }
        #endregion

    }
}
