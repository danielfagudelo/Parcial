using System.ComponentModel.DataAnnotations;

namespace Parcial.Shared.Entities
{
    public class Boleta
    {
        public int Id { get; set; }
        public DateTime? FechaUso { get; set; }
        public bool FueUsada { get; set; }
        public String? Porteria { get; set; }

    }

}
