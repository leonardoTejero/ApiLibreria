using Infraestructure.Entity.Model.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Library
{
    [Table("Editorial", Schema = "Library")]
    public class EditorialEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Campus { get; set; }

        public IEnumerable<BookEntity> BookEntities { get; set; }
    }
}
