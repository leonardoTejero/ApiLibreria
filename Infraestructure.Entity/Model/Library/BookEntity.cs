using Infraestructure.Entity.Model.Library;
using Infraestructure.Entity.Model.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Library
{
    [Table("Book", Schema = "Library")]
    public class BookEntity
    {
        [Key]
        public int IdBook { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Synopsis { get; set; }
        public int NumberPages { get; set; }

        [ForeignKey("EditorialEntity")]
        public int IdEditorial { get; set; }
        public EditorialEntity EditorialEntity { get; set; }

        public IEnumerable<AuthorBookEntity> AuthorBookEntities { get; set; }

    }
}
