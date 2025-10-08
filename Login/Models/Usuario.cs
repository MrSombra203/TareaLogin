using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "El nombre completo es requerido")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder 100 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [Display(Name = "Rol")]
        public string Rol { get; set; } = "Usuario";

        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}