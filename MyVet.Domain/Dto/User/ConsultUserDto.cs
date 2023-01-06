using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Domain.Dto.User
{
    public class ConsultUserDto : UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
    }
}
