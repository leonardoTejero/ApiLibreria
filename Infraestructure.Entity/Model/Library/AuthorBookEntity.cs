using Infraestructure.Entity.Model.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Library
{
    [Table("AuthorBook", Schema = "Library")]
    public class AuthorBookEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AuthorEntity")]
        public int IdAuthor { get; set; }

        public AuthorEntity AuthorEntity { get; set; }

        [ForeignKey("BookEntity")]
        public int IdBook { get; set; }

        public BookEntity BookEntity { get; set; }
    }
}
