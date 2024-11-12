using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConcesionarioBack.Common.Models
{
    public class Carro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarroId { get; set; }
        public string? Modelo { get; set; }
        public string? Color { get; set; }
        public string? Kilometraje { get; set; }
        public int Valor { get; set; }
        public string? Imagen { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool Nuevo { get; set; }
        public bool Activo { get; set; }
    }
}
