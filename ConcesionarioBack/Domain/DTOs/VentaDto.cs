namespace ConcesionarioBack.Domain.DTOs
{
    public class VentaDto
    {
        public int VentaId { get; set; }
        public string NombreComprador { get; set; }
        public string TelefonoComprador { get; set; }
        public string CorreoComprador { get; set; }
        public int? CarroId { get; set; }
        public int? MotoId { get; set; }
    }
}
