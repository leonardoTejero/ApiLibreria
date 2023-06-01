using System.ComponentModel.DataAnnotations;

namespace MyLibrary.Domain.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(200)]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
