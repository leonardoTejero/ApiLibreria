﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Domain.Dto.Book
{
    public class ConsultBookDto : InsertBookDto
    {
        public int Id { get; set; }
        public string Editorial { get; set; }
        public int IdAuthorBook { get; set; }
        public string Author { get; set; }

    }
}
