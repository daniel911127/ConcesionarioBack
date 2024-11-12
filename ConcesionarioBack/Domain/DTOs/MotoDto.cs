namespace ConcesionarioBack.Domain.DTOs
{
    public class MotoDto
    {
        public int MotoId { get; set; }
        public string? Modelo { get; set; }
        public string? Color { get; set; }
        public string? Kilometraje { get; set; }
        public int? Valor { get; set; }
        public string? Imagen { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool Nuevo { get; set; }
        public bool Activo { get; set; }
        public int? Cilindraje { get; set; }
        public string? Velocidades { get; set; }
        public bool EsActualizacion { get; set; }
    }
}
