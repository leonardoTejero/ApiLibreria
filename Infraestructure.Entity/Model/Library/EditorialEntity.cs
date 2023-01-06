using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        //Una editorial puede tener muchos libros (IEnumerable)
        public IEnumerable<BookEntity> BookEntities { get; set; }
    }
}
