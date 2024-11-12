using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConcesionarioBack.Common.Models
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VentaId { get; set; }
        public string NombreComprador { get; set; }
        public string TelefonoComprador { get; set; }
        public string CorreoComprador { get; set; }
        public int? CarroId { get; set; }
        public int? MotoId { get; set; }

        [ForeignKey("CarroId")]
        public virtual Carro Carro { get; set; }
        [ForeignKey("MotoId")]
        public virtual Moto Moto { get; set; }
    }
}
