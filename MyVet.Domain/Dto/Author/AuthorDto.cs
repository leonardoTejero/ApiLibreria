using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLibrary.Domain.Dto.Author
{
    public class AuthorDto : InsertAuthorDto
    {
        public int Id { get; set; }

    }
}
