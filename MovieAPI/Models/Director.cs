using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DirectorId { get; set; }

        [Required]
        [SwaggerSchema(Description = "Nombre del director", Nullable = false)]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Nacionalidad del director")]
        public string? Nationality { get; set; }

        [SwaggerSchema(Description = "Edad del director")]
        public int Age { get; set; }

        [SwaggerSchema(Description = "¿Está activo?")]
        public bool Active { get; set; }
    }
    public class DirectorCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Nationality { get; set; }

        public int Age { get; set; }

        public bool Active { get; set; }
    }
}
