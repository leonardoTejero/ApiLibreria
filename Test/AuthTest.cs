using ApiLibreriaNeoris.Controllers;
using Common.Utils.Exceptions;
using Infraestructure.Core.Data;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Services;
using MyLibrary.Domain.Services.Interface;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using static Common.Utils.Constant.Const;

namespace Test
{
    public class AuthTest
    {
        #region Attributes
        private readonly AuthenticationController _controller;
        private readonly IUserServices _userServices;
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        #endregion

        #region Builder
        public AuthTest()
        {
            //Creacion de todos los objetos necesarios que se inyectan en cada servicio

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=LEOT\\SQLEXPRESS; Database=BdLibreriaNeoris; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=True;")
                .Options;


            _context = new DataContext(dbContextOptions);
            _unitOfWork = new UnitOfWork(_context);

            //_configuration = new ConfigurationBuilder().Build();

            var _configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\leona\\source\\repos\\ApiLibreriaNeoris\\Test") // Ruta base Directory.GetCurrentDirectory()
                .AddJsonFile("appsettings.test.json") // Nombre del archivo de configuración de pruebas
                .Build();

            _userServices = new UserServices(_unitOfWork, _configuration);
            _controller = new AuthenticationController(_userServices);
        }
        #endregion

        [Fact]
        public void Post_AuthenticationSucceeded()
        {
            LoginDto user = new()
            {
                UserName = "admin@gmail.com",
                Password = "admin"
            };

            var result = _controller.Login(user);

            Assert.IsType<OkObjectResult>(result);
            // Validar contenido de la respuesta como token etc..
        }

        [Fact]
        public void Post_AuthenticationFailed()
        {
            LoginDto user = new()
            {
                UserName = "invalid@gmail.com",
                Password = "invalid"
            };

            var exception = Assert.Throws<BusinessException>(() => _controller.Login(user));
            Assert.Equal("Credenciales Incorrectas.", exception.Message);
        }

        [Fact]
        public void Post_TokenExpiration()
        {
            LoginDto user = new()
            {
                UserName = "admin@gmail.com",
                Password = "admin"
            };

            var result = _controller.Login(user);

            Assert.IsType<OkObjectResult>(result);

            // Obtener el objeto resultante de la respuesta
            var okResult = (OkObjectResult)result;
            ResponseDto responseObject = (ResponseDto)okResult.Value;

            //Obtener el token del objeto resultante
            var objToken = (TokenDto)responseObject.Result;
            var token = objToken.Token;

            // Verifica que el token tenga la fecha de expiración esperada y que haya pasado el tiempo de expiración adecuado
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenData = tokenHandler.ReadJwtToken(token);
            var expirationDate = tokenData.ValidTo;

            Assert.True(DateTime.UtcNow < expirationDate);
            // También puedes verificar otros detalles del token, como los roles o los claims
        }

        [Fact]
        public void Post_RoleVerification()
        {
            // Configura el contexto de pruebas con un usuario que tenga roles específicos

            LoginDto user = new()
            {
                UserName = "estandar@gmail.com",
                Password = "estandar"
            };

            var response = _controller.Login(user);

            Assert.IsType<OkObjectResult>(response);

            // Obtener el objeto resultante de la respuesta
            var okResult = (OkObjectResult)response;
            ResponseDto responseObject = (ResponseDto)okResult.Value;

            //Obtener el token del objeto resultante
            var objToken = (TokenDto)responseObject.Result;
            var token = objToken.Token;

            // Verificar que el token contenga los roles esperados
            var tokenHandler = new JwtSecurityTokenHandler();
            //Lee el token JWT analiza y valida y lo deserializa en un objeto de tipo JwtSecurityToken
            var tokenData = tokenHandler.ReadJwtToken(token);
            var roles = tokenData.Claims.Where(c => c.Type == TypeClaims.IdRol).Select(c => c.Value).ToList();

            // que contenga rol admin "1" standar "2"
            //Assert.Contains("1", roles);

            Assert.Contains("2", roles);

        }

        //[Fact]
        //public void ObtenerId() 
        //{
        //    int id = 2;
        //    LoginDto user = new()
        //    {
        //        UserName = "estandar@gmail.com",
        //        Password = "estandar"
        //    };

        // Casting para verificar que la respuesta sea del tipo esperado 
        //    var result = (OkObjectResult)_controller.Login(user);
        //    Assert.IsType<ResponseDto>(result.Value);

        //}

    }
}