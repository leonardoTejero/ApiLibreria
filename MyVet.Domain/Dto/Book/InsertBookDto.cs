﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLibrary.Domain.Dto.Book
{
    public class InsertBookDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "La sinopsis es requerida")]
        [MaxLength(500)]
        public string Synopsis { get; set; }

        public int NumberPages { get; set; }

        public int IdEditorial { get; set; }
        public int IdAuthor { get; set; }
    }
}
