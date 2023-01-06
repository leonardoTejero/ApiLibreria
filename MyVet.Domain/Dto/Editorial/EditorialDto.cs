using Infraestructure.Entity.Model.Library;
using Microsoft.VisualBasic;
using MyLibrary.Domain.Dto.Book;
using MyLibrary.Domain.Dto.Editorial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLibrary.Domain.Dto
{
    public class EditorialDto : InsertEditorialDto
    {
        [Key]
        public int Id { get; set; }
        public IEnumerable<String> Books { get; set; }
    }
}
