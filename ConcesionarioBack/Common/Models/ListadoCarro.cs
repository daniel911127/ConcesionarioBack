using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConcesionarioBack.Common.Models
{
    public class ListadoCarro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ListaCarroId { get; set; }
        public string Modelo { get; set; }
        public int Precio { get; set; }
    }
}
