﻿using Infraestructure.Entity.Model.Library;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model
{
    [Table("User", Schema = "Security")]
    public class UserEntity
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "la contraseña es requerido")]
        [MaxLength(200)]
        public string Password { get; set; }

        public IEnumerable<RolUserEntity> RolUserEntities { get; set; }
        public IEnumerable<AuthorEntity> AuthorEntities { get; set; }

        //Concatena el nombre y el apellido, Not maped = no lo busca en base de datos
        [NotMapped]
        public string FullName { get { return $"{this.Name} {this.LastName}"; } }

    }
}
